using System.Windows;

namespace Canucks.NewsReader.Phone.Helpers
{
    public class ThemeSettings
    {
        public static ThemeSettings Instance = new ThemeSettings();

        private ThemeSettings()
        {
        }

        public Theme CurrentTheme
        {
            get
            {
                return (Visibility) Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible
                           ? Theme.Light
                           : Theme.Dark;
            }
        }
    }
}