using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Canucks.NewsReader.Common.Helpers;
using Canucks.NewsReader.Common.Model;
using Canucks.NewsReader.Phone.Helpers;
using Canucks.NewsReader.Phone.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Canucks.NewsReader.Phone
{
    public partial class App : Application
    {
        private static MainViewModel _mainViewModel;


        private static NewsFeedViewModel _newsFeedViewModel;

        private static ScheduleViewModel _scheduleViewModel;

        private static PlayOffScheduleViewModel _playOffScheduleViewModel;

        private static FinalScoresViewModel _finalScoresViewModel;

        private static PlayOffFinalScoresViewModel _playOffFinalScoresViewModel;

        private static TwitterViewModel _twitterViewModel;

        public static Canucks.NewsReader.Phone.Helpers.Theme CurrentTheme;

        public static IsolatedStorageSettings isoSettings;

        public static BitmapImage RefreshImage;

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            SmartDispatcher.Initialize(Deployment.Current.Dispatcher);

            CurrentTheme = ThemeSettings.Instance.CurrentTheme;

            isoSettings = IsolatedStorageSettings.ApplicationSettings;
            SetRefreshImageSource();

            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            SetTheme();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Current.Host.Settings.EnableFrameRateCounter = true;

               // MetroGridHelper.IsVisible = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        public static ErrorMessage CurrentError { get; set; }

        public static MainViewModel MainViewModel
        {
            get
            {
                // Delay creation of the view model until necessary.
                return _mainViewModel ?? (_mainViewModel = new MainViewModel());
            }
        }

        public static NewsFeedViewModel NewsFeedViewModel
        {
            get
            {
                // Delay creation of the view model until necessary.
                return _newsFeedViewModel ?? (_newsFeedViewModel = new NewsFeedViewModel());
            }
        }

        public static ScheduleViewModel ScheduleViewModel
        {
            get { return _scheduleViewModel ?? (_scheduleViewModel = new ScheduleViewModel()); }
        }

        public static PlayOffScheduleViewModel PlayOffScheduleViewModel
        {
            get { return _playOffScheduleViewModel ?? (_playOffScheduleViewModel = new PlayOffScheduleViewModel()); }
        }

        public static FinalScoresViewModel FinalScoresViewModel
        {
            get { return _finalScoresViewModel ?? (_finalScoresViewModel = new FinalScoresViewModel()); }
        }

        public static PlayOffFinalScoresViewModel PlayOffFinalScoresViewModel
        {
            get { return _playOffFinalScoresViewModel ?? (_playOffFinalScoresViewModel = new PlayOffFinalScoresViewModel()); }
        }

        public static TwitterViewModel TwitterViewModel
        {
            get { return _twitterViewModel ?? (_twitterViewModel = new TwitterViewModel()); }
        }
        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                //    // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }

            else
            {
                var rootSource = RootVisual as TransitionFrame;

                if (rootSource != null)
                {
                    SetAppErrorMessage(e.ExceptionObject);
                    rootSource.Source = new Uri("/Error.xaml", UriKind.Relative);
                }
                else
                {
                    SetAppErrorMessage(e.ExceptionObject);
                    RootFrame.Navigate(new Uri("/Error.xaml", UriKind.Relative));
                }

                GlobalLoading.Instance.IsLoading = false;
                e.Handled = true;
            }
        }

        private static void SetAppErrorMessage(Exception exception)
        {
            if (exception != null && !String.IsNullOrEmpty(exception.Message))
            {
                CurrentError = new ErrorMessage
                                   {
                                       Error = exception,
                                       FriendlyError = "Something bad happened.",
                                       Title = "Sorry about this"
                                   };
            }
            else
            {
                CurrentError = new ErrorMessage
                                   {
                                       Error = null,
                                       FriendlyError = "Something bad happened...but we don't know what.",
                                       Title = "Sorry about this"
                                   };
            }
        }

        private static void SetTheme()
        {
            if ((isoSettings.Contains("Theme")))
            {
                var theme = isoSettings["Theme"].ToString();
                if (theme == "Light")
                {
                    ThemeManager.ToLightTheme();
                }
                else
                {
                    ThemeManager.ToDarkTheme();
                }
            }
            else
            {
                ThemeManager.ToLightTheme();
                App.isoSettings["Theme"] = "Light";
            }
        }

        private static void SetRefreshImageSource()
        {
            //RefreshImage = CurrentTheme == Theme.Dark
            //                   ? new BitmapImage(new Uri("Images/Dark/appbar.refresh.rest.png", UriKind.Relative))
            //                   : new BitmapImage(new Uri("Images/Light/appbar.refresh.rest.png", UriKind.Relative));
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            //RootFrame = new PhoneApplicationFrame();
            RootFrame = new TransitionFrame();

            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;


            GlobalLoading.Instance.Initialize(RootFrame);
        }


        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}