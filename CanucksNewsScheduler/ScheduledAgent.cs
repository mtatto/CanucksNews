using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace CanucksNewsScheduler
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        private readonly IsolatedStorageSettings _isolated = IsolatedStorageSettings.ApplicationSettings;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                                                              {
                                                                  Application.Current.UnhandledException +=
                                                                      ScheduledAgent_UnhandledException;
                                                              });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            ProcessNotification();
        }

        private void ProcessNotification()
        {
            if (DateTime.Now > new DateTime(2012, 4, 5) || DateTime.Now < new DateTime(2012, 10, 01))
            {
                GetUpcomingPlayoffSchedule();
            }
            else
            {
                if (GetUpdateSelection())
                {
                    GetUpcomingSchedule();
                }
                else
                {
                    GetFinalScore();
                } 
            }
            
        }

        public bool GetUpdateSelection()
        {
            return new Random().Next()%2 == 0;
        }

        private static void UpdateTile(string backTitle, string backContent)
        {
            ShellTile tileToFind = ShellTile.ActiveTiles.First();
            var newTileData = new StandardTileData
                                  {
                                      Title = "Canucks News",
                                      BackgroundImage = new Uri("Images/CanucksTile.png", UriKind.Relative),
                                      BackTitle = backTitle,
                                      BackContent = backContent
                                  };

            tileToFind.Update(newTileData);
        }

        #region final

        public void GetFinalScore()
        {
            string queryString = string.Format("{0}?start={1}&pageSize={2}", Settings.FinalScores, "1", "1");
            var wb = new WebClient();
            wb.DownloadStringCompleted += (s, ev) => UpdateFinalScore(ProcessFinalItems(ev.Result));
            wb.DownloadStringAsync(new Uri(queryString));
        }

        private CompletedViewSchedule UpdateFinalScore(CompletedSchedule input)
        {
            var output = new CompletedViewSchedule
                             {
                                 Teams = String.Format("{0} vs {1}", input.HomeTeam, input.VisitingTeam),
                                 GameDateTime =
                                     string.Format("{0}", input.StartDateLocal.ToShortDateString()),
                                 FinalScores = input.FinalScores
                             };

            if (!string.IsNullOrWhiteSpace(output.FinalScores))
            {
                _isolated["FinalKey"] = output.FinalScores;
            }

            string fs = null;
            if (_isolated.Contains("FinalKey"))
            {
                fs = _isolated["FinalKey"].ToString();
            }

            string backContent = fs;
            UpdateTile("Final Score", backContent);
            NotifyComplete();
            return output;
        }

        private CompletedSchedule ProcessFinalItems(string result)
        {
            CompletedSchedule returnType;
            try
            {
                returnType = JsonHelpers.DeserializeJson<List<CompletedSchedule>>(result).First();
            }
            catch (Exception)
            {
                returnType = new CompletedSchedule();
            }

            return returnType;
        }

        #endregion

        private void GetUpcomingPlayoffSchedule()
        {
            string queryString = String.Format("{0}?start={1}&pageSize={2}", Settings.UpcomingPlayoff, "1", "1");
            var wb = new WebClient();
            wb.DownloadStringCompleted += (s, ev) => UpdateUpComing(ProcessUpcomingItems(ev.Result));
            wb.DownloadStringAsync(new Uri(queryString));
        }

        #region Upcoming

        private void GetUpcomingSchedule()
        {
            string queryString = String.Format("{0}?start={1}&pageSize={2}", Settings.Upcoming, "1", "1");
            var wb = new WebClient();
            wb.DownloadStringCompleted += (s, ev) => UpdateUpComing(ProcessUpcomingItems(ev.Result));
            wb.DownloadStringAsync(new Uri(queryString));
        }


        private void UpdateUpComing(UpComingSchedule input)
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

            if (!string.IsNullOrWhiteSpace(output.Teams))
            {
                _isolated["UpComing"] = output.Teams;
            }

            string upcoming = null;
            if (_isolated.Contains("UpComing"))
            {
                upcoming = _isolated["UpComing"].ToString();
            }

            string backContent = upcoming;
            UpdateTile("Schedule", backContent);
            NotifyComplete();
        }

        private static UpComingSchedule ProcessUpcomingItems(string result)
        {
            UpComingSchedule returnType;
            try
            {
                returnType = JsonHelpers.DeserializeJson<List<UpComingSchedule>>(result).First();
            }
            catch (Exception)
            {
                returnType = new UpComingSchedule();
            }

            return returnType;
        }

        #endregion
    }
}