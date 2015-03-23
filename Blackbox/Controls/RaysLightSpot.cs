using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Blackbox.Controls
{
    public class RaysLightSpot : Canvas
    {
        private Thickness _thickness;
        private Ellipse _ellipse;
        private const int Diameter = 10;
        private const int Radius = 5;

        public RaysLightSpot()
        {
            this.Visibility = Visibility.Collapsed;

            _thickness = new Thickness();

            _ellipse = new Ellipse();
            _ellipse.Width = Diameter;
            _ellipse.Height = Diameter;
            _ellipse.Fill = new SolidColorBrush(Colors.White);

            this.Children.Add(_ellipse);
        }

        public void SetXY(double x, double y)
        {
            _thickness.Left = x - Radius;
            _thickness.Top = y - Radius;
            _ellipse.Margin = _thickness;
        }

        public void SetStroke(Brush brush)
        {
            _ellipse.Fill = brush;
        }
    }
}
