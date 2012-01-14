using System;

namespace Canucks.NewsReader.Common.Model
{
    public class ScheduleBase : ModelBase
    {
        private const string SrtStartDateSite = "StartDateSite";


        private const string StDateLocal = "StartDateLocal";


        private const string VTeam = "VisitingTeam";


        private const string HTeam = "HomeTeam";


        private const string STimeEst = "StartTimeEst";


        private const string STimeLocal = "StartTimeLocal";
        private string _homeTeam;
        private DateTime _startDateLocal;
        private DateTime _startDateSite;
        private string _startTimeEst;
        private string _startTimeLocal;
        private string _visitingTeam;

        public DateTime StartDateSite
        {
            get { return _startDateSite; }
            set
            {
                if (_startDateSite != value)
                {
                    _startDateSite = value;
                    NotifyPropertyChanged(SrtStartDateSite);
                }
            }
        }

        public DateTime StartDateLocal
        {
            get { return _startDateLocal; }
            set
            {
                if (_startDateLocal != value)
                {
                    _startDateLocal = value;
                    NotifyPropertyChanged(StDateLocal);
                }
            }
        }

        public string VisitingTeam
        {
            get { return _visitingTeam; }
            set
            {
                if (_visitingTeam != value)
                {
                    _visitingTeam = value;
                    NotifyPropertyChanged(VTeam);
                }
            }
        }

        public string HomeTeam
        {
            get { return _homeTeam; }
            set
            {
                if (_homeTeam != value)
                {
                    _homeTeam = value;
                    NotifyPropertyChanged(HTeam);
                }
            }
        }

        public string StartTimeEst
        {
            get { return _startTimeEst; }
            set
            {
                if (_startTimeEst != value)
                {
                    _startTimeEst = value;
                    NotifyPropertyChanged(STimeEst);
                }
            }
        }

        public string StartTimeLocal
        {
            get { return _startTimeLocal; }
            set
            {
                if (_startTimeLocal != value)
                {
                    _startTimeLocal = value;
                    NotifyPropertyChanged(STimeLocal);
                }
            }
        }
    }
}