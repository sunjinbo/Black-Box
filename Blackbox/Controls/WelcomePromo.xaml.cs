using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Blackbox
{
    public partial class WelcomePromo : UserControl
    {
        private Rectangle[,] rectangles = 
            new Rectangle[BlackboxConfig.PromoRow, BlackboxConfig.PromoColumn];
        private Random random = new Random();
        private DispatcherTimer timer;
        private Rectangle curRectangle;
        private PlaneProjection projection;

        // Default c++ constructor
        public WelcomePromo()
        {
            InitializeComponent();

            for (int row = 0; row < BlackboxConfig.PromoRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.PromoColumn; column++)
                {
                    rectangles[row, column] = new Rectangle();
                    rectangles[row, column].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    rectangles[row, column].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    rectangles[row, column].Width = BlackboxConfig.BoxWidth;
                    rectangles[row, column].Height = BlackboxConfig.BoxHeight;
                    rectangles[row, column].Margin = new Thickness(row * BlackboxConfig.BoxWidth, column * BlackboxConfig.BoxHeight, 0, 0);
                    rectangles[row, column].Fill = new SolidColorBrush(GetRandomColor());
                    rectangles[row, column].Stroke= new SolidColorBrush(Color.FromArgb(0, 255,255,255));
                    rectangles[row, column].Projection = new PlaneProjection();
                    this.LayoutRoot.Children.Add(rectangles[row, column]);
                }
            }
        }

        // Reset all rectangles
        private void Reset()
        {
            for (int row = 0; row < BlackboxConfig.PromoRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.PromoColumn; column++)
                {
                    PlaneProjection theProjection 
                        = (PlaneProjection)this.rectangles[row, column].Projection;
                    theProjection.RotationX = 0;
                }
            }
        }

        // Gets a random color
        private Color GetRandomColor()
        {
            byte a = 255; // alpha
            byte r = (byte)(random.Next(205) + 50); // red
            byte g = (byte)(random.Next(205) + 50); // green
            byte b = (byte)(random.Next(205) + 50); // blue
            return Color.FromArgb(a, r, g, b);
        }

        // Gets current rotated rectangle object
        private Rectangle GetCurrentRectangle()
        {
            int h = random.Next(BlackboxConfig.PromoRow); // rowontal
            int v = random.Next(BlackboxConfig.PromoColumn); // columnical
            return rectangles[h, v];
        }

        // Load WelcomePromo user control
        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.timer == null)
            {
                this.timer = new DispatcherTimer();
                this.timer.Interval = TimeSpan.FromMilliseconds(50);
                this.timer.Tick += new EventHandler(timer_Tick);
                this.timer.Start();
            }

            Reset(); // reset all rectangles

            this.curRectangle = GetCurrentRectangle();
            this.projection = (PlaneProjection)this.curRectangle.Projection;
        }

        // Timer tick callback
        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.projection.RotationX >= 360)
            {
                this.projection.RotationX = 0;
                this.curRectangle = GetCurrentRectangle();
                this.projection = (PlaneProjection)this.curRectangle.Projection;
            }
            this.projection.RotationX += 10;

            if (this.projection.RotationX == 90 || this.projection.RotationX == 270)
            {
                this.curRectangle.Fill = new SolidColorBrush(GetRandomColor());
            }
        }
    }
}
