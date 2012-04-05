using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;
using JeffWilcox.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using TombstoneHelper;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Canucks.NewsReader.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static int _currentFeaturePage = 1;

        private IScheduleService _scheduleService;

        private IScheduleService ScheduleService
        {
            get { return _scheduleService ?? (_scheduleService = new ScheduleService()); }
        }

        // Constructor
        public MainPage()
        {
            IScheduler scheduler = Scheduler.Dispatcher;
            scheduler.Schedule(() =>
                                   {
                                       InitializeComponent();
                                       SystemTray.SetOpacity(this, 0.0);
                                       SetRefreshImage();
                                       if (!(App.isoSettings.Contains("UpComing")))
                                       {
                                           App.isoSettings.Add("UpComing", "");
                                       }
                                       if (!(App.isoSettings.Contains("FinalKey")))
                                       {
                                          App.isoSettings.Add("FinalKey", "");
                                       }

                                       RefreshTileTask();
                                   });
            DataContext = App.MainViewModel;

       

#if DEBUG
            ScheduledAction action = ScheduledActionService.Find("CanucksNewsScheduler");
            if (action != null)
            ScheduledActionService.LaunchForTest("CanucksNewsScheduler", TimeSpan.FromSeconds(1));
            #endif
            }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RestoreState(); 
            //var t = (PanoramaItem)MainPano.Items[0];
            //var rtr = t.Name;
            //if (App.MainViewModel.IsPlayoffs)
            //{


            //    nhlPlayoffs.Visibility = System.Windows.Visibility.Visible;
            //}
            //else
            //{
            //    nhlPlayoffs.Visibility = System.Windows.Visibility.Collapsed;
            //}

        }

        private void SetRefreshImage()
        {
            TwitterRefresh.Source = App.RefreshImage;
            ScheduleRefresh.Source = App.RefreshImage;
            RecentRefresh.Source = App.RefreshImage;
        }

        private void NewsFeedLink_Click(object sender, RoutedEventArgs e)
        {
            var hyperlinkButton = (HyperlinkButton) sender;
            if (Equals(hyperlinkButton.Tag.ToString(), "twitter"))
            {
                NavigationService.Navigate(new Uri("/Views/Twitter.xaml", UriKind.Relative));
            }
            NavigationService.Navigate(new Uri(string.Format("/Views/NewsFeed.xaml?feedName={0}", hyperlinkButton.Tag),
                                               UriKind.Relative));
        }

        private void lnkbtupcoming_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Schedule.xaml", UriKind.Relative));
        }

        private void lnkbtFinalScores_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/FinalScores.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.MainViewModel.LoadData();
        }

        private void Image_Tap(object sender, GestureEventArgs e)
        {
            App.MainViewModel.GetTwitterFeed();
        }

        private void ScheduleRefresh_Tap(object sender, GestureEventArgs e)
        {
            App.MainViewModel.LoadScoresAndSchedules();
        }

        private void RecentRefresh_Tap(object sender, GestureEventArgs e)
        {
            App.MainViewModel.GetNewsStream(refresh: true);
        }


        private void lnkbtgetMoreNews_Click(object sender, RoutedEventArgs e)
        {
            _currentFeaturePage++;
            App.MainViewModel.AddToNewsStream(_currentFeaturePage.ToString(CultureInfo.InvariantCulture));
        }

        private void RefreshTileTask()
        {
            string liveTile = null;
            if (App.isoSettings.Contains("LiveTile"))
            {
                liveTile = App.isoSettings["LiveTile"].ToString();
            }
            if (liveTile == "True")
            {
                GetValue();
            }
        }

        private static void GetValue()
        {
            var task = new PeriodicTask("CanucksNewsScheduler")
                           {Description = "Updates a Live Tile with schedule information"};


            try
            {
                ScheduledActionService.Remove("CanucksNewsScheduler");
            }
            catch (InvalidOperationException)
            {
                // ignore
            }

            try
            {
                ScheduledActionService.Add(task);
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var map = button.Content as StaticMap;
                if (map != null)
                {
                    map.NavigateToMapsApplication();
                }
            }
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SystemSettings.xaml", UriKind.Relative));
        }

        private void TextBlock_Tap(object sender, GestureEventArgs e)
        {
            var phoneNumber = ((TextBlock) sender).Tag;
            if (!string.IsNullOrWhiteSpace(phoneNumber.ToString()))
            {
                var phonecall = new PhoneCallTask();
                phonecall.PhoneNumber = phoneNumber.ToString();
                phonecall.Show();
            }
        }

        private void lnkbtupcomingplayoffs_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lnkbtFinalScoresPlayoffs_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}