using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Blackbox
{
    public class Star : Canvas
    {
        public double VelocityX = 1.0;
        public double VelocityY = 1.0;
        public double Gravity = 1.0;

        private PlaneProjection projection;
        private const int speed = 2;

        public Star(double size)
        {
            double op = GlobalValue.OPACITY;

            Rectangle e = new Rectangle();
            e.Width = size;
            e.Height = size;

            if (GlobalValue.IsRColor == null)
                e.Fill = new SolidColorBrush(Color.FromArgb(255, (byte)(128 + (128 * GlobalValue.random.NextDouble())),
                            (byte)(128 + (128 * GlobalValue.random.NextDouble())),
                            (byte)(128 + (128 * GlobalValue.random.NextDouble()))));
            else
                e.Fill = new SolidColorBrush(Color.FromArgb(255, GlobalValue.IsRColor.Color.R,
                    GlobalValue.IsRColor.Color.G, GlobalValue.IsRColor.Color.B));
            e.Opacity = op;
            e.SetValue(Canvas.LeftProperty, -e.Width / 2);
            e.SetValue(Canvas.TopProperty, -e.Height / 2);
            this.Children.Add(e);
        }

        public Star(ImageSource source, double size)
        {
            Image image = new Image();
            image.Source = source;
            image.Projection = new PlaneProjection();
            this.projection = (PlaneProjection)image.Projection;
            image.Opacity = GlobalValue.OPACITY;
            image.Width = size;
            image.Height = size;
            this.Children.Add(image);
        }

        public void RunLoop()
        {
            for (int i = 0; i < speed; ++i)
            {
                DoLoop();
            }
        }

        public double X
        {
            get { return Canvas.GetLeft(this); }
            set { Canvas.SetLeft(this, value); }
        }

        public double Y
        {
            get { return Canvas.GetTop(this); }
            set { Canvas.SetTop(this, value); }
        }

        public void DoLoop()
        {
            X = X + VelocityX;
            Y = Y + VelocityY;
            this.Opacity += GlobalValue.OpacityInc;
            VelocityY += Gravity;
            this.projection.RotationZ += (VelocityY * 5);
        }
    }
}
