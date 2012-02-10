using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Canucks.NewsReader.Phone.Views
{
    public partial class NewsFeed : PhoneApplicationPage
    {
        public NewsFeed()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string myParameterValue = NavigationContext.QueryString["feedName"];

            NewsFeedViewModel newsFeedViewModel = App.NewsFeedViewModel;
            newsFeedViewModel.LoadData(myParameterValue);
            DataContext = newsFeedViewModel;
            base.OnNavigatedTo(e);
        }

        private void FeedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = (sender as ListBox).SelectedItem as NewsItemModel;

            if (data != null && !string.IsNullOrWhiteSpace(data.ArticleLink))
            {
                var wbt = new WebBrowserTask {Uri = new Uri(data.ArticleLink, UriKind.Absolute)};
                wbt.Show();
            }
        }
    }
}