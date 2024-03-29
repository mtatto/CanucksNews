﻿using System;
using System.Collections.Generic;

namespace Canucks.NewsReader.Common
{
    public class Settings
    {
        #region FeedInfo

        public const string NewsUrl = @"http://thetatto.com/CanucksNewsServices/Provider/getnews";
        public const string StreamUrl = @"http://thetatto.com/CanucksNewsServices/Provider/GetNewsStream";
        public const string FeaturesUrl = @"http://thetatto.com/CanucksNewsServices/Provider/GetFeatures";
        public const string Upcoming = @"http://thetatto.com/CanucksNewsServices/Provider/getupcomingschedule";
        public const string FinalScores = @"http://thetatto.com/CanucksNewsServices/Provider/GetCompletedSchedule";
        public const string UpcomingPlayoff = @"http://thetatto.com/CanucksNewsServices/Provider/GetUpComingPlayoffSchedule";
        public const string FinalScoresPlayoff = @"http://thetatto.com/CanucksNewsServices/Provider/GetCompletedPlayoffSchedule";
        
        public static DateTime PlayOffStart = new DateTime(2012,4,5);
        public static DateTime PlayOffEnd = new DateTime(2012,10,01);

        public static Dictionary<string, KeyValuePair<string, string>> RssFeeds = new Dictionary
            <string, KeyValuePair<string, string>>
                                                                                      {
                                                                                          {
                                                                                              "canuckscom",
                                                                                              new KeyValuePair
                                                                                              <string, string>(
                                                                                              @"canucks.com",
                                                                                              @"http://canucks.nhl.com/rss/news.xml")
                                                                                              },
                                                                                          {
                                                                                              "theprovince",
                                                                                              new KeyValuePair
                                                                                              <string, string>(
                                                                                              @"the province",
                                                                                              @"http://rss.canada.com/get/?F6949")
                                                                                              },
                                                                                          {
                                                                                              "thevancouversun",
                                                                                              new KeyValuePair
                                                                                              <string, string>(
                                                                                              @"the vancouver sun",
                                                                                              @"http://rss.canada.com/get/?F6961")
                                                                                              }
                                                                                      };

        #endregion

        public const string TwitterFeedUri =
            "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=VanCanucks";

        public static readonly Uri Vancanucks = new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=VanCanucks");
        public static readonly Uri Canucksgame = new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=canucksgame");
        public static readonly Uri Canuckstickets = new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=canuckstickets");
        public static readonly Uri Canucksstore = new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=canucksstore");
        public static readonly Uri Canuckspromo = new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=canuckspromo");

      

        #region appInfo

        public const string ApplicationName = "Canucks News";
        public const string EmailAddress = "theTatto@theTatto.com";
        public const string Subject = "please provide feedback";
        public const string DefaulImage = "/Images/defaultimage.png";

        #endregion
    }
}