using System;
using System.Windows.Media;
using System.Windows.Threading;

namespace Blackbox.Controls
{
    public class NookBoxView : BoxView
    {
        private DispatcherTimer rotatedTimer;
        private DispatcherTimer acceleratedTimer;
        private PlaneProjection projection;
        private int speed;
        private const int NormalSpeed = 10;
        private const int HighSpeed = 60;
        private const double RotatedTimeSpane = 50;
        private const double AcceleratedTimeSpan = 1000;
        private const int OneCircleDegree = 360;

        public override void UpdateStateValue(BoxData newState)
        {
            // no state need to update
        }

        public void Accelerate()
        {
            this.acceleratedTimer.Start();
            this.speed = HighSpeed;
        }

        public NookBoxView()
            : base()
        {

            this.projection = new PlaneProjection();

            this.image.Source = 
                BlackboxImageUtils.Image(BlackboxImageType.NookBoxBackground);
            this.imageForeground.Source =
                BlackboxImageUtils.Image(BlackboxImageType.NookBoxForeground);
            this.imageForeground.Projection = new PlaneProjection();
            this.projection = (PlaneProjection)this.imageForeground.Projection;

            this.rotatedTimer = new DispatcherTimer();
            this.rotatedTimer.Interval = TimeSpan.FromMilliseconds(RotatedTimeSpane);
            this.rotatedTimer.Tick += new EventHandler(OnRotatedTimerTick);
            this.rotatedTimer.Start();
            this.speed = NormalSpeed;

            this.acceleratedTimer = new DispatcherTimer();
            this.acceleratedTimer.Interval = TimeSpan.FromMilliseconds(AcceleratedTimeSpan);
            this.acceleratedTimer.Tick += new EventHandler(OnAcceleratedTimerTick);
        }

        private void OnRotatedTimerTick(object sender, EventArgs e)
        {
            this.projection.RotationZ += speed;
            if (this.projection.RotationZ == OneCircleDegree)
            {
                int rotationZ = (int)this.projection.RotationZ % OneCircleDegree;
                this.projection.RotationZ = rotationZ;
            }
        }

        private void OnAcceleratedTimerTick(object sender, EventArgs e)
        {
            this.acceleratedTimer.Stop();
            this.speed = NormalSpeed;
        }
    }
}
