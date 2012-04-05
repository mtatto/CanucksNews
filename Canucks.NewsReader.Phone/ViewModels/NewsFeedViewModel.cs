using System.Collections.Generic;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class NewsFeedViewModel : ViewModelBase
    {
        private INewsService _service;
        public string NewsPageTitle { get; private set; }

        public NewsFeedModel NewsFeed { get; private set; }

        public ObservableCollectionEx<NewsItemModel> NewsItems { get; private set; }

        public INewsService Service
        {
            get { return _service ?? (_service = new NewsService()); }

            set { _service = value; }
        }

        public void LoadData(string feedName)
        {
            KeyValuePair<string, string> feedValue;

            bool feedKey = Settings.RssFeeds.TryGetValue(feedName.ToLower(), out feedValue);

            if (!feedKey)
            {
                NewsFeed = new NewsFeedModel();
            }

            NewsPageTitle = !string.IsNullOrWhiteSpace(feedValue.Key) ? feedValue.Key : "Recent Items";

            NewsFeed = Service.GetNews(feedName, "8");
            NewsItems = NewsFeed.Items;
        }
    }
}