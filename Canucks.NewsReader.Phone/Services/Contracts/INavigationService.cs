using System.Collections.Generic;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface INavigationService
    {
        void Navigate(string uri);

        void Navigate(string uri, IDictionary<string, string> parameters);
    }
}
