using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Canucks.NewsReader.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool _onNavigatedToCalled = true;

        private static int _currentFeaturePage = 1;

        // Constructor
        public MainPage()
        {
            LayoutUpdated += MainPageLayoutUpdated;

            InitializeComponent();

            DataContext = App.MainViewModel;
            SystemTray.SetOpacity(this, 0.0);
            SetRefreshImage();
        }

        private void MainPageLayoutUpdated(object sender, EventArgs e)
        {
            if (_onNavigatedToCalled)
            {
                _onNavigatedToCalled = false;
                Dispatcher.BeginInvoke(() =>
                                           {
                                               if (!NetworkInterface.GetIsNetworkAvailable())
                                               {
                                                   MessageBox.Show(
                                                       "Sorry...We need a network connection to make this app work :(");
                                                   NavigationService.Navigate(new Uri("/Error.xaml", UriKind.Relative));
                                               }
                                           }
                    );
            }
        }

        private void SetRefreshImage()
        {
            TwitterRefresh.Source = App.RefreshImage;
            ScheduleRefresh.Source = App.RefreshImage;
            RecentRefresh.Source = App.RefreshImage;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            var wbt = new WebBrowserTask();
            //            wbt.Uri = new Uri(c.ArticleLink, UriKind.Absolute);
        }

        private void NewsFeedLink_Click(object sender, RoutedEventArgs e)
        {
            var hyperlinkButton = (HyperlinkButton) sender;
            NavigationService.Navigate(new Uri(string.Format("/NewsFeed.xaml?feedName={0}", hyperlinkButton.Tag),
                                               UriKind.Relative));
        }

        private void lnkbtupcoming_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Schedule.xaml", UriKind.Relative));
        }

        private void lnkbtFinalScores_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FinalScores.xaml", UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
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
            App.MainViewModel.GetNewsStream();
        }


        private void lnkbtgetMoreNews_Click(object sender, RoutedEventArgs e)
        {
            _currentFeaturePage++;
            App.MainViewModel.AddToNewsStream(_currentFeaturePage.ToString(CultureInfo.InvariantCulture));
        } 
    }
}