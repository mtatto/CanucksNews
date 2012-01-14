using System.Collections.ObjectModel;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface INewsService
    {
        NewsFeedModel GetNews(string newsFeed, string storyCount);

        NewsFeature GetFeatures(string team, string storyCount);

        ObservableCollection<NewsStreamItem> GetNewsStream(string team, string start, string pageSize);

        event LoadEventHandler NewsLoaded;

        event LoadEventHandler NewsStreamLoaded;

        event LoadEventHandler FeaturesLoaded;
    }
}