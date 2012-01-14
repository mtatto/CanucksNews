using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Canucks.NewsReader.Phone
{
    public partial class Error : PhoneApplicationPage
    {
        public Error()
        {
            InitializeComponent();
        }

        private void btnRetry_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.CurrentError != null)
            {
                mTitle.Text = App.CurrentError.Title;
                FMessage.Text = App.CurrentError.FriendlyError;
                StackTrace.Text = App.CurrentError.Error.StackTrace;
                StackTraceHeader.Visibility = Visibility.Visible;
                StackTrace.Visibility = Visibility.Visible;
                StackBorder.Visibility = Visibility.Visible;
            }
            else
            {
                mTitle.Text = "Sorry about this";
                FMessage.Text = "Something bad happened. :(";
                StackTraceHeader.Visibility = Visibility.Collapsed;
                StackTrace.Visibility = Visibility.Collapsed;
                StackBorder.Visibility = Visibility.Collapsed;
            }

            base.OnNavigatedTo(e);
        }
    }
}