using System.Collections.Generic;

namespace Canucks.NewsReader.Common
{
    public class Settings
    {
        public const string NewsUrl = @"http://thetatto.com/NewsServices/Provider/getnews";
        public const string StreamUrl = @"http://thetatto.com/NewsServices/Provider/GetNewsStream";
        public const string FeaturesUrl = @"http://thetatto.com/NewsServices/Provider/GetFeatures";
        public const string Upcoming = @"http://thetatto.com/NewsServices/Provider/getupcomingschedule";
        public const string FinalScores = @"http://thetatto.com/NewsServices/Provider/GetCompletedSchedule";
        public const string ApplicationName = "Canucks News";

        public const string TwitterFeedUri =
            "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=VanCanucks";

        public const string EmailAddress = "theTatto@theTatto.com";
        public const string Subject = "please provide feedback";
        public const string DefaulImage = "/Images/defaultimage.png";


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
                                                                                              @"the provice",
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
    }
}