using System.Windows;
using Canucks.NewsReader.Common;
using Canucks.NewsReader.Common.Model;

namespace Canucks.NewsReader.Phone.ViewModels
{
    public class ViewModelBase : ModelBase
    {
        private static bool? _isInDesignMode;
        private string _dataStatus;
        private bool _isDataLoaded;
        private string _pageTitle;

        public string ApplicationName
        {
            get { return Settings.ApplicationName; }
        }

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                if (_pageTitle == value)
                    return;
                _pageTitle = value;
                NotifyPropertyChanged("PageTitle");
            }
        }

        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set
            {
                if (_isDataLoaded == value)
                    return;
                _isDataLoaded = value;
                NotifyPropertyChanged("IsDataLoaded");
            }
        }

        public string DataStatus
        {
            get { return _dataStatus; }
            set
            {
                if (_dataStatus == value)
                    return;
                _dataStatus = value;
                NotifyPropertyChanged("DataStatus");
            }
        }

        public static bool IsInDesignMode()
        {
            if (!_isInDesignMode.HasValue)
            {
                _isInDesignMode =
                    (null == Application.Current) ||
                    Application.Current.GetType() == typeof (Application);
            }
            return _isInDesignMode.Value;
        }
    }
}