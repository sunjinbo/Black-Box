using System;
using System.Windows.Data;

namespace Blackbox
{
    public class GuessTypeToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int guessTypeInteger = -1;

            if (value is GuessType)
            {
                guessTypeInteger = (int)value;
            }

            return guessTypeInteger;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            GuessType guessType = GuessType.AllAtOnce;

            if (value is int)
            {
                guessType = (GuessType)value;
            }

            return guessType;
        }
    }
}
