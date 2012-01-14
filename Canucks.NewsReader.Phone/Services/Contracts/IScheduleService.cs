using System.Collections.ObjectModel;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface IScheduleService
    {
        event LoadEventHandler ServiceLoaded;
        ObservableCollection<UpComingViewSchedule> GetUpcomingSchedule(string pageStart, string pageSize);
    }
}