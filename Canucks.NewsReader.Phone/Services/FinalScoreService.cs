using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
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

        public ObservableCollection<CompletedViewSchedule> GetFinalScores(string pageStart, string pageSize)
        {
            _finalScores = new ObservableCollection<CompletedViewSchedule>();
            var loadedEventArgs = new LoadEventArgs();

            string queryString = string.Format("{0}?start={1}&pageSize={2}", Settings.FinalScores, pageStart ?? "",
                                               pageSize ?? "");
            var wb = new WebClient();

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
                                       _finalScores.Add(ConvertToFinalView(finalScores));
                                   }
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

        protected virtual void OnFinalScoresLoaded(LoadEventArgs e)
        {
            if (FinalScoresLoaded != null)
            {
                FinalScoresLoaded(this, e);
            }
        }
    }
}