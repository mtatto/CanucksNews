using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class TwitterViewModel : ViewModelBase
    {
        private readonly List<string> _errors;

        private string _twitterDataStatus;

        private ITwitterService _twitterService;

        public TwitterViewModel()
        {
            TwitterService.ServiceLoaded += ServiceTwitterLoaded;
            _errors = new List<string>();
            LoadData();
        }

        public ObservableCollectionEx<TwitterStatusModel> CanucksGame { get; private set; }

        public ObservableCollectionEx<TwitterStatusModel> CanucksPromo { get; private set; }

        public ObservableCollectionEx<TwitterStatusModel> CanucksStore { get; private set; }

        public ObservableCollectionEx<TwitterStatusModel> CanucksTickets { get; private set; }

        public ObservableCollectionEx<TwitterStatusModel> Twitter { get; private set; }

        public string TwitterDataStatus
        {
            get { return _twitterDataStatus; }
            set
            {
                if (_twitterDataStatus != value)
                {
                    _twitterDataStatus = value;
                    NotifyPropertyChanged("TwitterDataStatus");
                }
            }
        }

        private ITwitterService TwitterService
        {
            get
            {
                ITwitterService twitterService = _twitterService;
                ITwitterService twitterService1 = twitterService;
                if (twitterService == null)
                {
                    var twitterService2 = new TwitterService();
                    ITwitterService twitterService3 = twitterService2;
                    _twitterService = twitterService2;
                    twitterService1 = twitterService3;
                }
                return twitterService1;
            }
        }

        private void CheckForErrors()
        {
            if (_errors.Count != 0)
            {
                if (_errors.Count == 1)
                {
                    string str = string.Concat("We were unable to load: ", _errors.First());
                    var errorService = (new ErrorService(str, "")).ErrorDialog(true);
                    errorService.HandleError();
                }
                if (_errors.Count > 1)
                {
                    const string str1 = "We were unable to load: ";
                    List<string> strs = _errors;
                    string str2 = string.Concat(str1, strs.Aggregate((i, j) => string.Concat(i, " , ", j)));
                    var errorService1 = (new ErrorService(str2, "")).ErrorDialog(true);
                    errorService1.HandleError();
                }
            }
        }

        internal void GetCanucksGameFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (CanucksGame != null)
                {
                    CanucksGame.Clear();
                    CanucksGame = TwitterService.GetFeed(Settings.Canucksgame);
                }
                else
                {
                    CanucksGame = TwitterService.GetFeed(Settings.Canucksgame);
                }
                NotifyPropertyChanged("CanucksGame");
            }
            catch (Exception)
            {
                _errors.Add("canucks game feed");
            }
        }

        internal void GetCanucksPromoFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (CanucksPromo != null)
                {
                    CanucksPromo.Clear();
                    CanucksPromo = TwitterService.GetFeed(Settings.Canuckspromo);
                }
                else
                {
                    CanucksPromo = TwitterService.GetFeed(Settings.Canuckspromo);
                }
                NotifyPropertyChanged("CanucksPromo");
            }
            catch (Exception)
            {
                _errors.Add("canucks promo feed");
            }
        }

        internal void GetCanucksStoreFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (CanucksStore != null)
                {
                    CanucksStore.Clear();
                    CanucksStore = TwitterService.GetFeed(Settings.Canucksstore);
                }
                else
                {
                    CanucksStore = TwitterService.GetFeed(Settings.Canucksstore);
                }
                NotifyPropertyChanged("CanucksStore");
            }
            catch (Exception)
            {
                _errors.Add("canucks store feed");
            }
        }

        internal void GetCanucksticketsFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (CanucksTickets != null)
                {
                    CanucksTickets.Clear();
                    CanucksTickets = TwitterService.GetFeed(Settings.Canuckstickets);
                }
                else
                {
                    CanucksTickets = TwitterService.GetFeed(Settings.Canuckstickets);
                }
                NotifyPropertyChanged("CanucksTickets");
            }
            catch (Exception)
            {
                _errors.Add("canucks tickets feed");
            }
        }

        internal void GetTwitterFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (Twitter != null)
                {
                    Twitter.Clear();
                    Twitter = TwitterService.GetFeed(Settings.Vancanucks);
                }
                else
                {
                    Twitter = TwitterService.GetFeed(Settings.Vancanucks);
                }
                NotifyPropertyChanged("Twitter");
            }
            catch (Exception)
            {
                _errors.Add("twitter feed");
            }
        }

        public void LoadData()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                GetTwitterFeed();
                GetCanucksGameFeed();
                GetCanucksticketsFeed();
                GetCanucksStoreFeed();
                GetCanucksPromoFeed();
                CheckForErrors();
            }
            else
            {
                ErrorService errorService =
                    (new ErrorService("We need a network connection to make this app work", "")).ErrorDialog(true);
                errorService.HandleError();
            }
        }

        private static void ServiceTwitterLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }
    }
}