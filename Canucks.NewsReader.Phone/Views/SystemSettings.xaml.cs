using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using TombstoneHelper;

namespace Canucks.NewsReader.Phone.Views
{
    public partial class SystemSettings : PhoneApplicationPage
    {
        public SystemSettings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string liveTile = null;
            if (App.isoSettings.Contains("LiveTile"))
            {
                liveTile = App.isoSettings["LiveTile"].ToString();
            }
            if (liveTile == "True")
            {
                chkLiveTile.IsChecked = true;
            }
            else
            {
                chkLiveTile.IsChecked = false;
            }

            if ((App.isoSettings.Contains("Theme")))
            {
                var theme = App.isoSettings["Theme"].ToString();
                if (theme == "Light")
                {
                    rb_LightTheme.IsChecked = true;
                }
                else
                {
                    rb_DarkTheme.IsChecked = true;
                }
            }

            this.RestoreState();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(App.isoSettings.Contains("LiveTile")))
            {
                App.isoSettings.Add("LiveTile", "True");
            }
            else
            {
                App.isoSettings["LiveTile"] = "True";
            }

            // Application Tile is always the first Tile, even if it is not pinned to Start.
            var tileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (tileToFind != null)
            {
                // Set the properties to update for the Application Tile.
                // Empty strings for the text values and URIs will result in the property being cleared.
                var newTileData = new StandardTileData
                {
                    Title = "Canucks News",
                    BackTitle = "Schedule",
                    BackContent = "Upcoming" + System.Environment.NewLine + "Final"

                };

                // Update the Application Tile
                tileToFind.Update(newTileData);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!(App.isoSettings.Contains("LiveTile")))
            {
                App.isoSettings.Add("LiveTile", "False");
            }
            else
            {
                App.isoSettings["LiveTile"] = "False";
                try
                {
                    ScheduledActionService.Remove("CanucksNewsScheduler");
                }
                catch (InvalidOperationException)
                {
                    // ignore
                }
                
            }
        }

        private void rb_LightTheme_Checked(object sender, RoutedEventArgs e)
        {
            if (!(App.isoSettings.Contains("Theme")))
            {
                App.isoSettings.Add("Theme", "Light");
            }
            else
            {
                App.isoSettings["Theme"] = "Light";
            }

        }

        private void rb_DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            if (!(App.isoSettings.Contains("Theme")))
            {
                App.isoSettings.Add("Theme", "Dark");
            }
            else
            {
                App.isoSettings["Theme"] = "Dark";
            }
        }
    }
}