using System.Collections.ObjectModel;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.Services;
using Canucks.NewsReader.Phone.Services.Contracts;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class PlayOffScheduleViewModel : ViewModelBase
    {
        private IScheduleService _service;

        public PlayOffScheduleViewModel()
        {
            Refresh();
        }

        private IScheduleService ScheduleService
        {
            get { return _service ?? (_service = new ScheduleService()); }
        }

        public ObservableCollection<UpComingViewSchedule> UpComingSchedule { get; private set; }

        public void Refresh()
        {
            ScheduleService.ServiceLoaded += ServiceUpcomingLoaded;
            LoadData();
        }

        private static void ServiceUpcomingLoaded(object sender, LoadEventArgs e)
        {
            GlobalLoading.Instance.IsLoading = !e.IsLoaded;
        }

        private void GetUpcomingItems()
        {
            GlobalLoading.Instance.IsLoading = true;
            UpComingSchedule = ScheduleService.GetUpcomingSchedule(Settings.UpcomingPlayoff, "1", "120");
        }

        public void LoadData()
        {
            GetUpcomingItems();
        }
    }
}