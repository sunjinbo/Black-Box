using System;
using System.Windows.Threading;
using Blackbox.Utils;

namespace Blackbox.Controls
{
    public class BlackBoxView : BoxView
    {
        private DispatcherTimer timer;
        private DispatcherTimer blinkTimer;
        private StarEffectPane _starEffectPane;
        private bool reverse = false;

        public override void UpdateStateValue(BoxData newState)
        {
            StopBlink();

            switch (newState._state)
            {
                case BlackBox.GuessNoneState:
                    this.image.Source = 
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessNone);
                    this.imageForeground.Source = null;
                    break;
                case BlackBox.GuessingState:
                    this.image.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessNone);
                    this.imageForeground.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessing);
                    this.imageForeground.Opacity = 0.0;
                    timer.Start();
                    break;
                case BlackBox.GuessFailedState:
                    this.image.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessNone);
                    this.imageForeground.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessFailed);
                    this.imageForeground.Opacity = 0.0;
                    timer.Start();
                    if (!this.LayoutRoot.Children.Contains(_starEffectPane))
                    {
                        this.LayoutRoot.Children.Add(_starEffectPane);
                    }
                    _starEffectPane.Start(StarEffectPane.StarEffectType.RedStar);
                    break;
                case BlackBox.GuessedState:
                    this.image.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessNone);
                    this.imageForeground.Source =
                        BlackboxImageUtils.Image(BlackboxImageType.BlackBoxGuessed);
                    this.imageForeground.Opacity = 0.0;
                    timer.Start();
                    if (!this.LayoutRoot.Children.Contains(_starEffectPane))
                    {
                        this.LayoutRoot.Children.Add(_starEffectPane);
                    }
                    _starEffectPane.Start(StarEffectPane.StarEffectType.YellowStar);
                    break;
                default:
                    break;
            }
        }

        public BlackBoxView()
            : base()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(50);
            this.timer.Tick += new EventHandler(timer_Tick);

            this.blinkTimer = new DispatcherTimer();
            this.blinkTimer.Tick += new EventHandler(blinkTimer_Tick);

            _starEffectPane = new StarEffectPane();
            _starEffectPane.StarEffectCompleted += 
                new StarEffectCompletedEventHandler(_starEffectPane_StarEffectCompleted);
        }

        void _starEffectPane_StarEffectCompleted()
        {
            if (this.LayoutRoot.Children.Contains(_starEffectPane))
            {
                this.LayoutRoot.Children.Remove(_starEffectPane);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (reverse)
            {
                this.imageForeground.Opacity -= 0.1;
                if (this.imageForeground.Opacity < 0.3)
                {
                    reverse = false;
                }
            }
            else
            {
                if (this.imageForeground.Opacity >= 1.0)
                {
                    this.timer.Stop();

                    int timeSpan = BlackboxUtils.Random.Next(1, 5);
                    this.blinkTimer.Interval = TimeSpan.FromMilliseconds(timeSpan * 1000);
                    this.blinkTimer.Start();
                }
                else
                {
                    this.imageForeground.Opacity += 0.1;
                }
            }
        }

        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            reverse = true;
            this.timer.Start();
            this.blinkTimer.Stop();
        }

        private void StopBlink()
        {
            reverse = false;
            this.blinkTimer.Stop();
        }
    }
}
