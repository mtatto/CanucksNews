using System;
using System.Globalization;
using System.Windows.Data;

namespace Canucks.NewsReader.Phone.Converters
{
    public class BackGroundConverter : IValueConverter
    {
        #region IValueConverter

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            return (string) value == string.Empty ? "" : "Blue";
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}