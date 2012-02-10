using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

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
                FMessage.Text = "Something unexpected occurred.";
                StackTraceHeader.Visibility = Visibility.Collapsed;
                StackTrace.Visibility = Visibility.Collapsed;
                StackBorder.Visibility = Visibility.Collapsed;
            }

            base.OnNavigatedTo(e);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.To = "feedback@theTatto.com";
            email.Subject = "Canucks News auto-generated problem report";
            email.Body = App.CurrentError.Error.StackTrace;
            email.Show();
        }
    }
}