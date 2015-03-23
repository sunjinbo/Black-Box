using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Blackbox
{
    public class MirrorStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color mirrorColor = Color.FromArgb(255, 255, 255, 255);
            if ((bool)value)
            {
                mirrorColor = Color.FromArgb(255, 255, 201, 14);
            }
            else
            {
                mirrorColor = Color.FromArgb(255, 237, 28, 36);
            }

            return new SolidColorBrush(mirrorColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
