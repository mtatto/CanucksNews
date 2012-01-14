using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface ITwitterService
    {
        event LoadEventHandler ServiceLoaded;

        ObservableCollectionEx<TwitterStatusModel> GetFeed();
    }
}