using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Canucks.NewsReader.Phone.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TombstoneHelper;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Canucks.NewsReader.Phone.Views
{
    public partial class Twitter : PhoneApplicationPage
    {
        private  TwitterViewModel _twitterViewModel;
        public TwitterViewModel TwitterViewModel
        {
            get { return _twitterViewModel ?? (_twitterViewModel = new TwitterViewModel()); }
        }
        public Twitter()
        {
            InitializeComponent();

            DataContext = TwitterViewModel;
            SystemTray.SetOpacity(this, 0.0);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RestoreState();
        }
        
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.TwitterViewModel.LoadData();
        }
    }
}