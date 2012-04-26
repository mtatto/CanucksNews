using System.Collections.ObjectModel;
using Canucks.NewsReader.Common.Model;

namespace Canucks.NewsReader.Phone.Services.Contracts
{
    public interface IPlayOffScheduleService
    {
        ObservableCollection<UpComingViewSchedule> GetUpcomingPlayOffSchedule(string url, string pageStart, string pageSize);
    }
}