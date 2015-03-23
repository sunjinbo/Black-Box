using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Blackbox.Controls
{
    public delegate void StarEffectCompletedEventHandler();

    public class StarEffectPane : Canvas
    {
        public event StarEffectCompletedEventHandler StarEffectCompleted;

        public enum StarEffectType
        {
            YellowStar,
            RedStar,
            ColorfullPiece
        }

        private List<Star> _dotGroup = new List<Star>();
        private DispatcherTimer _timer;

        public StarEffectPane()
        {
            this.Background = new SolidColorBrush(Colors.Gray);
            this.Background.Opacity = 0.0;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(20);
            _timer.Tick += new EventHandler(loop_timer_Tick);
            _timer.Start();
        }

        void loop_timer_Tick(object sender, EventArgs e)
        {
            RunLoop();
        }

        void RunLoop()
        {
            for (int i = _dotGroup.Count - 1; i >= 0; i--)
            {
                Star star = _dotGroup[i];
                star.RunLoop();
                if (star.Opacity <= 0.1)
                {
                    this.Children.Remove(star);
                    _dotGroup.Remove(star);
                }
            }

            if (_dotGroup.Count == 0)
            {
                if (StarEffectCompleted!=null)
                {
                    StarEffectCompleted();
                }
            }
        }

        public virtual void Start(StarEffectType type)
        {
            int seed = (int)DateTime.Now.Ticks;

            for (int i = 0; i < GlobalValue.Dots_NUM; i++)
            {
                double size = GlobalValue.SIZE_MIN + (GlobalValue.SIZE_MAX - GlobalValue.SIZE_MIN) * GlobalValue.random.NextDouble();
                double xVelocity = GlobalValue.X_VELOCITY - 2 * GlobalValue.X_VELOCITY * GlobalValue.random.NextDouble();
                double yVelocity = -GlobalValue.Y_VELOCITY * GlobalValue.random.NextDouble();
                Star star = null;

                switch (type)
                {
                    case StarEffectType.YellowStar:
                        star = new Star(GlobalValue.YellowStarImage, size);
                        break;
                    case StarEffectType.RedStar:
                        star = new Star(GlobalValue.RedStarImage, size);
                        break;
                    case StarEffectType.ColorfullPiece:
                        star = new Star(size);
                        break;
                    default:
                        break;
                }

                star.Gravity = GlobalValue.GRAVITY;
                star.VelocityX = xVelocity;
                star.VelocityY = yVelocity;
                star.X = 22.5;
                star.Y = 22.5;
                _dotGroup.Add(star);
                Children.Insert(0, star);
                star.RunLoop();
            }
        }
    }
}
