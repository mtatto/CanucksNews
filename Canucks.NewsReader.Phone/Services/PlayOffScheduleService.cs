using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services.Contracts;
using Microsoft.Phone.Reactive;
using SharpGIS;

namespace Canucks.NewsReader.Phone.Services
{
    public class PlayOffScheduleService : IPlayOffScheduleService
    {
        private ObservableCollection<UpComingViewSchedule> _upcoming;

        #region IScheduleService Members

        public event LoadEventHandler ServiceLoaded;

        public ObservableCollection<UpComingViewSchedule> GetUpcomingPlayOffSchedule(string url, string pageStart, string pageSize)
        {
            _upcoming = new ObservableCollection<UpComingViewSchedule>();
            var upcoming2 = new List<UpComingSchedule>();
            var loadedEventArgs = new LoadEventArgs();

            string queryString = String.Format("{0}?start={1}&pageSize={2}", url, pageStart ?? "", pageSize ?? "");
            var wb = new GZipWebClient();
            Observable.FromEvent<DownloadStringCompletedEventArgs>(wb, "DownloadStringCompleted")
                .ObserveOn(Scheduler.ThreadPool)
                .Select(x => ProcessUpcomingItems(x.EventArgs.Result))
                .ObserveOn(Scheduler.Dispatcher)
                .Subscribe(s =>
                               {
                                   loadedEventArgs.IsLoaded = true;
                                   loadedEventArgs.Message = "";
                                   foreach (UpComingSchedule upComingSchedule in s)
                                   {
                                       upcoming2.Add(upComingSchedule);
                                       _upcoming.Add(ConvertToView(upComingSchedule));
                                   }

                                   ThreadPool.QueueUserWorkItem(o =>
                                                                    {
                                                                        InsertIntoIS(upcoming2);
                                                                        CacheUpComingSchedule(_upcoming);
                                                                    });

                                   OnUpcomingScheduleLoaded(loadedEventArgs);
                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = true;
                                          //TODO: LOG Error
                                          ErrorService error = new ErrorService(
                                              "Unable to retrieve any upcoming events", "")
                                              .ErrorDialog(true);
                                          error.HandleError();
                                          OnUpcomingScheduleLoaded(loadedEventArgs);
                                      }
                );
            wb.DownloadStringAsync(new Uri(queryString));

            //TODO: Add this to the service
            //if (!_upcoming.Any())
            //{
            //    _upcoming.Add(new UpComingViewSchedule{Teams = "Regular season complete", GameDateTime = "Enjoy the playoffs"});
            //}
            return _upcoming;
        }

        public bool IsPlayoffs()
        {
            //var wb = new WebClient();
            //bool returnValue = false;
            //wb.DownloadStringAsync(new Uri(Settings.IsPlayoffs));
            //Observable.FromEvent<DownloadStringCompletedEventArgs>(wb, "DownloadStringCompleted")
            //    .ObserveOn(Scheduler.ThreadPool)
            //    .ObserveOn(Scheduler.Dispatcher)
            //    .Subscribe(s => bool.TryParse(s.EventArgs.Result, out returnValue), e => { returnValue = false; }
            //    );
            //return returnValue;
            throw new NotImplementedException();
        }

        #endregion

        private static UpComingViewSchedule ConvertToView(UpComingSchedule input)
        {
            if (input == null) throw new ArgumentNullException("input");
            var output = new UpComingViewSchedule
                             {
                                 Teams = String.Format("{0} vs {1}", input.HomeTeam, input.VisitingTeam),
                                 GameDateTime =
                                     String.Format("{0} {1}", input.StartDateLocal.ToShortDateString(),
                                                   input.StartTimeLocal),
                                 TvInfo = input.TvInfo
                             };

            return output;
        }

        private static List<UpComingSchedule> ProcessUpcomingItems(string result)
        {
            List<UpComingSchedule> returnType;
            try
            {
                returnType = JsonHelpers.DeserializeJson<List<UpComingSchedule>>(result);
            }
            catch (Exception)
            {
                returnType = new List<UpComingSchedule>();
                ErrorService error = new ErrorService("Unable to retrieve any upcoming events", "")
                    .ErrorDialog(true);
                error.HandleError();
            }

            return returnType;
        }

        private void InsertIntoIS(List<UpComingSchedule> upcoming2)
        {
            if (App.isoSettings.Contains("UpComingPlayOff"))
            {
                string upcoming = App.isoSettings["UpComingPlayOff"].ToString();
                if (string.IsNullOrWhiteSpace(upcoming))
                {
                    if (upcoming2.Any())
                    {
                        UpComingSchedule schedule = upcoming2.Last();
                        if (!string.IsNullOrWhiteSpace(schedule.HomeTeam) &&
                            (!string.IsNullOrWhiteSpace(schedule.VisitingTeam)))
                        {
                            UpComingViewSchedule input = ConvertToView(schedule);
                            App.isoSettings["UpComingPlayOff"] = input.Teams;
                        }
                    }
                }
            }
        }

        private static void CacheUpComingSchedule(ObservableCollection<UpComingViewSchedule> upComingSchedule)
        {
            if (!(App.isoSettings.Contains("UpComingPlayOffSchedule")))
            {
                string json = JsonHelpers.SerializeJson(upComingSchedule);
                if (!string.IsNullOrEmpty(json) && json != "[]")
                {
                    App.isoSettings.Add("UpComingPlayOffSchedule", json);
                }
            }
            else
            {
                string json = JsonHelpers.SerializeJson(upComingSchedule);
                if (!string.IsNullOrEmpty(json) && json != "[]")
                {
                    App.isoSettings["UpComingPlayOffSchedule"] = json;
                }
            }
        }

        protected virtual void OnUpcomingScheduleLoaded(LoadEventArgs e)
        {
            if (ServiceLoaded != null)
            {
                ServiceLoaded(this, e);
            }
        }
    }
}