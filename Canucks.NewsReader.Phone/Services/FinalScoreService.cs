using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services.Contracts;
using Microsoft.Phone.Reactive;

namespace Canucks.NewsReader.Phone.Services
{
    public class FinalScoreService : IFinalScoreService
    {
        private ObservableCollection<CompletedViewSchedule> _finalScores;

        #region IFinalScoreService Members

        public event LoadEventHandler FinalScoresLoaded;

        public ObservableCollection<CompletedViewSchedule> GetFinalScores(string url, string pageStart, string pageSize)
        {
            _finalScores = new ObservableCollection<CompletedViewSchedule>();
            var finalScores2 = new List<CompletedSchedule>();
            var loadedEventArgs = new LoadEventArgs();

            string queryString = string.Format("{0}?start={1}&pageSize={2}", url, pageStart ?? "",
                                               pageSize ?? "");
            var wb = new SharpGIS.GZipWebClient();

            Observable.FromEvent<DownloadStringCompletedEventArgs>(wb, "DownloadStringCompleted")
                .ObserveOn(Scheduler.ThreadPool)
                .Select(x => ProcessFinalItems(x.EventArgs.Result))
                .ObserveOn(Scheduler.Dispatcher)
                .Subscribe(s =>
                               {
                                   loadedEventArgs.IsLoaded = true;
                                   loadedEventArgs.Message = "";
                                   foreach (CompletedSchedule finalScores in s)
                                   {
                                       finalScores2.Add(finalScores);
                                       _finalScores.Add(ConvertToFinalView(finalScores));
                                   }

                                   ThreadPool.QueueUserWorkItem(o =>
                                                                    {
                                                                        InsertIntoIS(finalScores2);
                                                                        CacheFinalScores(_finalScores);
                                                                    });

                                   OnFinalScoresLoaded(loadedEventArgs);
                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = true;
                                          //TODO: LOG Error
                                          ErrorService error = new ErrorService(
                                              "Unable to retrieve any upcoming events", "")
                                              .ErrorDialog(true);
                                          error.HandleError();
                                          OnFinalScoresLoaded(loadedEventArgs);
                                      }
                );
            wb.DownloadStringAsync(new Uri(queryString));
            return _finalScores;
        }

        #endregion

        private static CompletedViewSchedule ConvertToFinalView(CompletedSchedule input)
        {
            var output = new CompletedViewSchedule
                             {
                                 Teams = String.Format("{0} vs {1}", input.HomeTeam, input.VisitingTeam),
                                 GameDateTime =
                                     string.Format("{0}", input.StartDateLocal.ToShortDateString()),
                                 FinalScores = input.FinalScores
                             };

            return output;
        }

        private static List<CompletedSchedule> ProcessFinalItems(string result)
        {
            List<CompletedSchedule> returnType;
            try
            {
                returnType = JsonHelpers.DeserializeJson<List<CompletedSchedule>>(result);
            }
            catch (Exception)
            {
                returnType = new List<CompletedSchedule>();
                ErrorService error = new ErrorService("Unable to retrieve any final scores", "")
                    .ErrorDialog(true);
                error.HandleError();
            }

            return returnType;
        }

        private void InsertIntoIS(List<CompletedSchedule> finalScore)
        {
            if (App.isoSettings.Contains("FinalKey"))
            {
                var fs = App.isoSettings["FinalKey"].ToString();
                if (string.IsNullOrWhiteSpace(fs))
                {
                    if (finalScore.Any())
                    {
                        var fscores = finalScore.Last();
                        if (!string.IsNullOrWhiteSpace(fscores.FinalScores))
                        {
                            App.isoSettings["UpComing"] = fscores.FinalScores;
                        }
                    }

                }
            }
        }

        private static void CacheFinalScores(ObservableCollection<CompletedViewSchedule> finalScores)
        {
            if (!(App.isoSettings.Contains("FinalScores")))
            {
                string json = JsonHelpers.SerializeJson(finalScores);
                if (!string.IsNullOrWhiteSpace(json) || json != "[]")
                {
                    try
                    {
                        App.isoSettings.Add("FinalScores", json);
                    }
                    catch (Exception)
                    {
                        
                       //best effort
                    }
                    
                }
            }
            else
            {
                string json = JsonHelpers.SerializeJson(finalScores);
                if (!string.IsNullOrEmpty(json) || json != "[]")
                {
                    App.isoSettings["FinalScores"] = json;
                }
            }
        }

        protected virtual void OnFinalScoresLoaded(LoadEventArgs e)
        {
            if (FinalScoresLoaded != null)
            {
                FinalScoresLoaded(this, e);
            }
        }
    }
}