using System;
using System.Windows.Data;

namespace Blackbox
{
    public class MirrorStateToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imageSource = string.Empty;

            if ((bool)value)
            {
                imageSource = "/icons/Gameboard/Mirror.png";
            }
            else
            {
                imageSource = "/icons/Gameboard/Mirror.Failed.png";
            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
