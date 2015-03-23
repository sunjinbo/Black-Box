using System.Windows.Data;
using System.Windows.Media;

namespace Blackbox
{
    public class RelatedStateToRaysColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Color raysColor = Color.FromArgb(255, 255, 255, 255);
            switch((int)value)
            {
                case LightBox.DeflectionState:
                case LightBox.DeflectionOutState:
                    raysColor = Color.FromArgb(255, 34, 177, 76);
                    break;
                case LightBox.ComplexDeflectionState:
                case LightBox.ComplexDeflectionOutState:
                    raysColor = Color.FromArgb(255, 63, 72, 204);
                    break;
                case LightBox.ReflectionState:
                    raysColor = Color.FromArgb(255, 255, 201, 14);
                    break;
                case LightBox.ComplexReflectionState:
                    raysColor = Color.FromArgb(255, 237, 28, 36);
                    break;
                default:
                    break;
            }

            return new SolidColorBrush(raysColor);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
