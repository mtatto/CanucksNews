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
    public class ScheduleService : IScheduleService
    {
        private ObservableCollection<UpComingViewSchedule> _upcoming;

        public event LoadEventHandler ServiceLoaded;

        public ObservableCollection<UpComingViewSchedule> GetUpcomingSchedule(string pageStart, string pageSize)
        {
            _upcoming = new ObservableCollection<UpComingViewSchedule>();
            var loadedEventArgs = new LoadEventArgs();

            string queryString = String.Format("{0}?start={1}&pageSize={2}", Settings.Upcoming, pageStart ?? "",
                                               pageSize ?? "");
            var wb = new WebClient();
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
                                       _upcoming.Add(ConvertToView(upComingSchedule));
                                   }
                                   OnUpcomingScheduleLoaded(loadedEventArgs);
                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = true;
                                          //TODO: LOG Error
                                          ErrorService error = new ErrorService("Unable to retrieve any upcoming events", "")
                                                                    .ErrorDialog(true);
                                          error.HandleError();
                                          OnUpcomingScheduleLoaded(loadedEventArgs);
                                      }
                );
            wb.DownloadStringAsync(new Uri(queryString));
            return _upcoming;
        }

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

        protected virtual void OnUpcomingScheduleLoaded(LoadEventArgs e)
        {
            if (ServiceLoaded != null)
            {
                ServiceLoaded(this, e);
            }
        }
    }
}