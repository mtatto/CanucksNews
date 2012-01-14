namespace Canucks.NewsReader.Common.Model
{
    public class UpComingViewSchedule : ScheduleViewBase
    {
        private const string Tv = "TvInfo";
        private string _tvInfo;

        public string TvInfo
        {
            get { return _tvInfo; }
            set
            {
                if (_tvInfo != value)
                {
                    _tvInfo = value;
                    NotifyPropertyChanged(Tv);
                }
            }
        }
    }
}