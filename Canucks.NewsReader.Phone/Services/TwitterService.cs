using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services.Contracts;
using Microsoft.Phone.Reactive;

namespace Canucks.NewsReader.Phone.Services
{
    public class TwitterService : ITwitterService
    {
        private static readonly Regex LinkRegEx =
            new Regex(
                "http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?",
                RegexOptions.IgnoreCase);

        private ObservableCollectionEx<TwitterStatusModel> _twitterFeed;

        #region ITwitterService Members

        public event LoadEventHandler ServiceLoaded;

        public ObservableCollectionEx<TwitterStatusModel> GetFeed()
        {
            _twitterFeed = new ObservableCollectionEx<TwitterStatusModel>();
            var wc = new WebClient();
            ErrorService service = new ErrorService("Unable to retrieve the twitter feed.", "").ErrorDialog(true);

            var loadedEventArgs = new LoadEventArgs();
            IObservable<IEvent<DownloadStringCompletedEventArgs>> o = Observable.FromEvent
                <DownloadStringCompletedEventArgs>(wc, "DownloadStringCompleted")
                .ObserveOn(Scheduler.ThreadPool)
                .ObserveOn(Scheduler.Dispatcher);

            o.Subscribe(s =>
                            {
                                if (s.EventArgs != null)
                                {
                                    try
                                    {
                                        string twitterResults = s.EventArgs.Result;
                                        if (!String.IsNullOrWhiteSpace(twitterResults))
                                        {
                                            XDocument doc = XDocument.Parse(twitterResults);
                                            ParseTwitterResults(doc);
                                        }
                                        else
                                        {
                                            service.HandleError();
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        service.HandleError();
                                    }
                                    finally
                                    {
                                        loadedEventArgs.IsLoaded = true;
                                        OnTwitterLoaded(loadedEventArgs);
                                    }
                                }
                                else
                                {
                                    service.HandleError();
                                    loadedEventArgs.IsLoaded = true;
                                    OnTwitterLoaded(loadedEventArgs);
                                }
                            }, e =>
                                   {
                                       loadedEventArgs.IsLoaded = true;
                                       loadedEventArgs.Message = e.Message.ToString(CultureInfo.InvariantCulture);
                                       OnTwitterLoaded(loadedEventArgs);
                                       service.HandleError();
                                   }
                );

            wc.DownloadStringAsync(new Uri(Settings.TwitterFeedUri));
            return _twitterFeed;
        }

        #endregion

        private void ParseTwitterResults(XDocument doc)
        {
            foreach (TwitterStatusModel model in doc.Descendants("status").Select(BuildTwitterStatusl))
            {
                _twitterFeed.Add(model);
            }
        }

        private TwitterStatusModel BuildTwitterStatusl(XElement item)
        {
            var model = new TwitterStatusModel {Date = ParseTweetDate(item), Text = ParseTweetText(item)};
            model.TwitterLink = CreateLink(model.Text);
            return model;
        }

        private static string ParseTweetText(XElement item)
        {
            XElement xElement = item.Element("text");
            return xElement != null ? xElement.Value : "";
        }

        private static string ParseTweetDate(XElement item)
        {
            if (item == null) throw new ArgumentNullException("item");
            XElement xElement = item.Element("created_at");
            return xElement != null
                       ? xElement.Value.Substring(0,
                                                  xElement
                                                      .Value.
                                                      IndexOf(
                                                          '+'))
                       : "";
        }

        protected string CreateLink(string txt)
        {
            string returnText = "";
            MatchCollection matches = LinkRegEx.Matches(txt);

            foreach (Match match in matches)
            {
                returnText = match.Value;
            }
            return returnText;
        }

        protected virtual void OnTwitterLoaded(LoadEventArgs e)
        {
            if (ServiceLoaded != null)
            {
                ServiceLoaded(this, e);
            }
        }
    }
}