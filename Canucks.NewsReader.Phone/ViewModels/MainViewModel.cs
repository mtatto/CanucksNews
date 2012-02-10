using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<string> _errors;
        private IFinalScoreService _finalScoreService;
        private string _newsItems;
        private string _newsStreamItems;
        private IScheduleService _scheduleService;
        private INewsService _service;
        private string _twitterDataStatus;
        private ITwitterService _twitterService;

        public MainViewModel()
        {
            Service.NewsStreamLoaded += ServiceNewsStreamLoaded;
            Service.FeaturesLoaded += ServiceFeaturesLoaded;
            ScheduleService.ServiceLoaded += ServiceUpcomingLoaded;
            FinalScoreService.FinalScoresLoaded += ServiceFinalScoresLoaded;
            TwitterService.ServiceLoaded += ServiceTwitterLoaded;
            _errors = new List<string>();
            LoadData();
        }

        private ITwitterService TwitterService
        {
            get { return _twitterService ?? (_twitterService = new TwitterService()); }
        }

        private IScheduleService ScheduleService
        {
            get { return _scheduleService ?? (_scheduleService = new ScheduleService()); }
        }

        private IFinalScoreService FinalScoreService
        {
            get { return _finalScoreService ?? (_finalScoreService = new FinalScoreService()); }
        }


        public ObservableCollection<NewsFeatureItem> Features { get; private set; }
        public ObservableCollectionEx<TwitterStatusModel> Twitter { get; private set; }
        public ObservableCollection<NewsStreamItem> StreamItems { get; private set; }
        public ObservableCollection<CompletedViewSchedule> CompletedSchedule { get; private set; }
        public ObservableCollection<UpComingViewSchedule> UpComingSchedule { get; private set; }
        public NewsFeature NewsFeature { get; private set; }
        public List<FeedInfo> FeedInfo { get; private set; }

        public string TwitterDataStatus
        {
            get { return _twitterDataStatus; }
            set
            {
                if (_twitterDataStatus == value)
                    return;
                _twitterDataStatus = value;
                NotifyPropertyChanged("TwitterDataStatus");
            }
        }

        public string NewsStreamItems
        {
            get { return _newsStreamItems; }
            set
            {
                if (_newsStreamItems == value)
                    return;
                _newsStreamItems = value;
                NotifyPropertyChanged("NewsStreamItems");
            }
        }

        public string NewsItems
        {
            get { return _newsItems; }
            set
            {
                if (_newsItems == value)
                    return;
                _newsItems = value;
                NotifyPropertyChanged("NewsItems");
            }
        }

        public INewsService Service
        {
            get { return _service ?? (_service = new NewsService()); }

            set { _service = value; }
        }

        private static void ServiceFinalScoresLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }

        private static void ServiceUpcomingLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }

        private void ServiceNewsStreamLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
            NewsStreamItems = e.Message;
        }

        private static void ServiceTwitterLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
            
        }

        private static void ServiceFeaturesLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }

        internal void GetNewsStream(string start = "1", string pageSize ="15", bool refresh = false)
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (refresh)
                {
                    StreamItems.Clear();
                }
                StreamItems = Service.GetNewsStream("canucks", start, pageSize);

                NotifyPropertyChanged("StreamItems");
            }
            catch (Exception)
            {
                _errors.Add("news stream");
            }
        }

        internal void AddToNewsStream(string start, string pageSize ="15")
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {

                ObservableCollection<NewsStreamItem> newItems = Service.GetNewsStream("canucks", start, pageSize);

                newItems.CollectionChanged += (o, i) =>
                                                  {
                                                      IList t = i.NewItems;
                                                      StreamItems.Add(t.OfType<NewsStreamItem>().Last());
                                                  };
              
                NotifyPropertyChanged("StreamItems");
            }
            catch (Exception)
            {
                _errors.Add("news stream");
            }
        }
        private void GetFeatures()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                NewsFeature = Service.GetFeatures("canuckscom", "8");
                Features = NewsFeature.FeatureItems;
            }
            catch (Exception)
            {
                _errors.Add("news features");
            }
        }

        private void GetUpcomingItems()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (UpComingSchedule == null)
                {
                    UpComingSchedule = ScheduleService.GetUpcomingSchedule("1", "2");
                }
                else
                {
                    UpComingSchedule.Clear();
                    UpComingSchedule = ScheduleService.GetUpcomingSchedule("1", "2");
                }

                NotifyPropertyChanged("UpComingSchedule");
            }
            catch (Exception)
            {
                _errors.Add("upcoming games");
            }
        }

        private void GetFinalScores()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                CompletedSchedule = FinalScoreService.GetFinalScores("1", "2");

               // CompletedSchedule.CollectionChanged += CompletedSchedule_CollectionChanged;
            }
            catch (Exception)
            {
                _errors.Add("final scores");
            }
        }

      

        internal void LoadScoresAndSchedules()
        {
            GetFinalScores();
            GetUpcomingItems();
        }
        internal void GetTwitterFeed()
        {
            GlobalLoading.Instance.IsLoading = true;
            try
            {
                if (Twitter == null)
                {
                    Twitter = TwitterService.GetFeed(null);
                }
                else
                {
                    Twitter.Clear();
                    Twitter = TwitterService.GetFeed(null);
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
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                ErrorService errorService = new ErrorService("We need a network connection to make this app work", "")
                    .ErrorDialog(true);
                errorService.HandleError();
            }
            else
            {
                LoadScoresAndSchedules();
                GetNewsStream();
                GetFeatures();
                GetTwitterFeed();
                FeedInfo = new List<FeedInfo>
                               {
                                   new FeedInfo {Id = 1, Key = "twitter", FeedName = "twitter"},
                                   new FeedInfo {Id = 2, Key = "canuckscom", FeedName = "canucks.com"},
                                   new FeedInfo {Id = 3, Key = "thevancouversun", FeedName = "the vancouver sun"},
                                   new FeedInfo {Id = 4, Key = "theprovince", FeedName = "the province"},
                                   
                               };
                CheckForErrors();
            }
        }

        private void CheckForErrors()
        {
            if (_errors.Count == 0)
            {
                return;
            }
            if (_errors.Count == 1)
            {
                string errorMessage = "We were unable to load: " + _errors.First();
                ErrorService errorService = new ErrorService(errorMessage, "").ErrorDialog(true);
                errorService.HandleError();
            }
            if (_errors.Count > 1)
            {
                string errorMessage = "We were unable to load: " + _errors.Aggregate((i, j) => i + " , " + j);
                ErrorService errorService = new ErrorService(errorMessage, "").ErrorDialog(true);
                errorService.HandleError();
            }
        }
    }
}