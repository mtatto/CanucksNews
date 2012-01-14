﻿using System.ComponentModel;
using System.Windows.Navigation;
using Canucks.NewsReader.Common.Helpers;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Canucks.NewsReader.Phone.Helpers
{
    public class GlobalLoading : INotifyPropertyChanged
    {
        private static GlobalLoading _in;
        private int _loadingCount;
        private ProgressIndicator _mangoIndicator;

        private GlobalLoading()
        {
        }

        public static GlobalLoading Instance
        {
            get { return _in ?? (_in = new GlobalLoading()); }
        }

        public bool IsDataManagerLoading { get; set; }

        public bool ActualIsLoading
        {
            get { return IsLoading || IsDataManagerLoading; }
        }

        public bool IsLoading
        {
            get { return _loadingCount > 0; }
            set
            {
                bool loading = IsLoading;
                if (value)
                {
                    ++_loadingCount;
                }
                else
                {
                    --_loadingCount;
                }

                NotifyValueChanged();
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void Initialize(PhoneApplicationFrame frame)
        {
            // If using AgFx:
            // DataManager.Current.PropertyChanged += OnDataManagerPropertyChanged;

            _mangoIndicator = new ProgressIndicator();
            frame.Navigated += OnRootFrameNavigated;
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            // Use in Mango to share a single progress indicator instance.
            object ee = e.Content;
            var pp = ee as PhoneApplicationPage;
            if (pp != null)
            {
                pp.SetValue(SystemTray.ProgressIndicatorProperty, _mangoIndicator);
            }
        }

        private void NotifyValueChanged()
        {
            if (_mangoIndicator != null)
            {
                _mangoIndicator.IsIndeterminate = _loadingCount > 0 || IsDataManagerLoading;

                // for now, just make sure it's always visible.
                if (_mangoIndicator.IsVisible == false)
                {
                    _mangoIndicator.IsVisible = true;
                }
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                SmartDispatcher.BeginInvoke(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }
    }
}