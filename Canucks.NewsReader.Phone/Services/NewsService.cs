using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services.Contracts;
using Microsoft.Phone.Reactive;

namespace Canucks.NewsReader.Phone.Services
{
    public class NewsService : INewsService
    {
        private static readonly Regex Regex = new Regex(@"\(([^)]*)\)", RegexOptions.Compiled);
        private NewsFeature _features;

        private NewsFeedModel _news;
        private ObservableCollection<NewsStreamItem> _newsStream;

        #region NewsStream

        public event LoadEventHandler NewsStreamLoaded;

        public ObservableCollection<NewsStreamItem> GetNewsStream(string team, string start, string pageSize)
        {
            _newsStream = new ObservableCollection<NewsStreamItem>();
            var loadedEventArgs = new LoadEventArgs();
            string queryString = string.Format("{0}?team={1}&start={2}&pageSize={3}", Settings.StreamUrl,
                                               team ?? "", start ?? "", pageSize ?? "");

            var wb = new SharpGIS.GZipWebClient();
            Observable.FromEvent<DownloadStringCompletedEventArgs>(wb, "DownloadStringCompleted")

                // Let's make sure that we’re on the thread pool
                .ObserveOn(Scheduler.ThreadPool)

                // When the event fires, just select the string and make
                // an IObservable<string> instead
                .Select(newString => ProcessNewsStreamItems(newString.EventArgs.Result))

                // Now go back to the UI Thread
                .ObserveOn(Scheduler.Dispatcher)

                // Subscribe to the observable
                .Subscribe(s =>
                               {
                                   loadedEventArgs.IsLoaded = true;
                                   loadedEventArgs.Message = "";
                                   for (int index = 0; index < s.Count; index++)
                                   {
                                       NewsStreamItem newsStreamItem = s[index];
                                       newsStreamItem.Title = Regex.Replace(newsStreamItem.Title, "");
                                       _newsStream.Add(newsStreamItem);
                                   }
                                   OnNewsStreamLoaded(loadedEventArgs);
                                   if (!(App.isoSettings.Contains("NewStream")))
                                   {
                                       ThreadPool.QueueUserWorkItem(o =>
                                                                        {
                                                                            var json= JsonHelpers.SerializeJson(_newsStream);
                                                                            App.isoSettings.Add("NewStream", json);
                                                                        });
                                   }
                                   else
                                   {
                                       ThreadPool.QueueUserWorkItem(o =>
                                       {
                                           var json = JsonHelpers.SerializeJson(_newsStream);
                                           App.isoSettings["NewStream"] = json;
                                       });
                                   }

                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = false;
                                          //TODO: LOG Error
                                          loadedEventArgs.Message =
                                              "We were unable to retrieve the news feed. Please refresh";
                                          OnNewsStreamLoaded(loadedEventArgs);
                                      }
                );

            wb.DownloadStringAsync(new Uri(queryString));
            return _newsStream;
        }

        private static List<NewsStreamItem> ProcessNewsStreamItems(string result)
        {
            var returnType = JsonHelpers.DeserializeJson<List<NewsStreamItem>>(result);
            return returnType;
        }

        #endregion

        #region News

        public event LoadEventHandler NewsLoaded;

        public NewsFeedModel GetNews(string newsFeed, string storyCount)
        {
            _news = new NewsFeedModel();
            var loadedEventArgs = new LoadEventArgs();
            string queryString = string.Format("{0}?feed={1}&pageSize={2}", Settings.NewsUrl, newsFeed ?? "",
                                               storyCount ?? "");
            var webClient = new SharpGIS.GZipWebClient();
            
            Observable.FromEvent<DownloadStringCompletedEventArgs>(webClient, "DownloadStringCompleted")
                .ObserveOn(Scheduler.ThreadPool)
                .Select(x => ProcessNews(x.EventArgs.Result))
                .ObserveOn(Scheduler.Dispatcher)
                .Subscribe(s =>
                               {
                                   BuildNewsFeedModel(s);
                                   loadedEventArgs.IsLoaded = true;
                                   loadedEventArgs.Message = "";
                                   OnNewsLoaded(loadedEventArgs);
                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = false;
                                          loadedEventArgs.Message = e.Message.ToString(CultureInfo.InvariantCulture);
                                          OnNewsLoaded(loadedEventArgs);
                                      });
            webClient.DownloadStringAsync(new Uri(queryString));
            return _news;
        }

        private static NewsFeedModel ProcessNews(string result)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(result);
            using (var ms = new MemoryStream(byteArray))
            {
                var ser = new DataContractJsonSerializer(typeof (NewsFeedModel));

                return (NewsFeedModel) ser.ReadObject(ms);
            }
        }

        #endregion

        #region Features

        public event LoadEventHandler FeaturesLoaded;

        public NewsFeature GetFeatures(string team, string pageSize)
        {
            _features = new NewsFeature();
            _features.FeatureItems.Clear();
            var loadedEventArgs = new LoadEventArgs();

            string qS = string.Format("{0}?feed={1}&pageSize={2}", Settings.FeaturesUrl, team ?? "",
                                      pageSize ?? "");

             
            var wb = new SharpGIS.GZipWebClient();
            Observable.FromEvent<DownloadStringCompletedEventArgs>(wb, "DownloadStringCompleted")
                .ObserveOn(Scheduler.ThreadPool)
                .Select(newString => ProcessFeatures(newString.EventArgs.Result))
                .Subscribe(s =>
                               {
                                   loadedEventArgs.IsLoaded = true;
                                   loadedEventArgs.Message = "";

                                   _features.Copyright = s.Copyright;
                                   _features.Description = s.Description;
                                   _features.Language = s.Language;
                                   _features.Link = s.Link;
                                   _features.Title = s.Title;

                                   for (int i = 0; i < s.FeatureItems.Count; i++)
                                   {
                                       NewsFeatureItem newsFeatureItem = s.FeatureItems[i];
                                       Deployment.Current.Dispatcher.BeginInvoke(() => _features.FeatureItems.Add(newsFeatureItem));
                                       
                                   }

                                  Deployment.Current.Dispatcher.BeginInvoke(() => OnFeaturesLoaded(loadedEventArgs));
                                   if (!(App.isoSettings.Contains("Features")))
                                   {
                                       ThreadPool.QueueUserWorkItem(o =>
                                       {
                                           var json = JsonHelpers.SerializeJson(_features);
                                           App.isoSettings.Add("Features", json);
                                       });
                                   }
                                   else
                                   {
                                       ThreadPool.QueueUserWorkItem(o =>
                                       {
                                           var json = JsonHelpers.SerializeJson(_features);
                                           App.isoSettings["Features"] = json;
                                       });
                                   }
                               }, e =>
                                      {
                                          loadedEventArgs.IsLoaded = false;
                                          loadedEventArgs.Message = e.Message.ToString(CultureInfo.InvariantCulture);
                                      }
                );
            wb.DownloadStringAsync(new Uri(qS));
            return _features;
        }

        private static NewsFeature ProcessFeatures(string result)
        {
            var returnType = JsonHelpers.DeserializeJson<NewsFeature>(result);
            return returnType;
        }

        #endregion
        
        #region INewsService Members

        #endregion

        public event LoadEventHandler RecentFeaturesLoaded;

        private void BuildNewsFeedModel(NewsFeedModel data)
        {
            _news.Title = data.Title;
            _news.Description = data.Description;
            _news.Copyright = data.Copyright;
            _news.Link = data.Link;
            _news.Language = data.Language;
            _news.ImageUrl = data.ImageUrl;

            foreach (NewsItemModel item in data.Items)
            {
                var newsItem = new NewsItemModel
                                   {
                                       Title = item.Title,
                                       ArticleLink = item.ArticleLink,
                                       Date = item.Date,
                                       Description = item.Description,
                                       ImageUrl = Coalesce(item.ImageUrl, Settings.DefaulImage).ToString()
                                   };
                _news.Items.Add(newsItem);
            }
        }

        protected virtual void OnNewsLoaded(LoadEventArgs e)
        {
            if (NewsLoaded != null)
            {
                NewsLoaded(this, e);
            }
        }

        protected virtual void OnNewsStreamLoaded(LoadEventArgs e)
        {
            if (NewsStreamLoaded != null)
            {
                NewsStreamLoaded(this, e);
            }
        }

        protected virtual void OnRecentFeaturesLoaded(LoadEventArgs e)
        {
            if (RecentFeaturesLoaded != null)
            {
                RecentFeaturesLoaded(this, e);
            }
        }

        protected virtual void OnFeaturesLoaded(LoadEventArgs e)
        {
            if (FeaturesLoaded != null)
            {
                FeaturesLoaded(this, e);
            }
        }

        /// <summary>
        /// Take the first non-null, non-empty value from the list
        /// </summary>
        /// <param name="parms">ParamArray for the items to coalesce</param>
        /// <returns>the first non-null, non-empty value, or the default</returns>
        private static object Coalesce(params object[] parms)
        {
            return parms.FirstOrDefault(p => p != null && p.ToString().Length > 0);
        }
    }
}