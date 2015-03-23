using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Blackbox.Controls;

namespace Blackbox
{
    public class LightBoxView : BoxView
    {
        private enum LightBoxDirection
        {
            LightBoxLeft,
            LightBoxRight,
            LightBoxTop,
            LightBoxBottom
        }

        private DispatcherTimer timer;
        private PlaneProjection projection;
        private BoxData state;

        public LightBoxView()
            : base()
        {
            this.projection = new PlaneProjection();

            this.timer = new DispatcherTimer();
            this.timer.Tick += new EventHandler(OnTick);
            this.image.Projection = new PlaneProjection();
        }

        public override void UpdateStateValue(BoxData newState)
        {
            state = newState;
            if (state._state == LightBox.ConcealState)
            {
                this.image.Source = GetLightBoxImage(state);
                this.image.Stretch = Stretch.UniformToFill;
            }
            else
            {
                this.projection = (PlaneProjection)this.image.Projection;
                this.projection.RotationX = 0;
                this.timer.Interval = TimeSpan.FromMilliseconds(50);
                this.timer.Start();
            }
        }

        // Timer tick callback
        private void OnTick(object sender, EventArgs e)
        {
            if (this.projection.RotationX >= 180)
            {
                this.timer.Stop();
            }
            if (this.projection.RotationX == 90)
            {
                this.image.Source = GetLightBoxImage(state);
            }

            this.projection.RotationX += 15;
        }

        private BitmapImage GetLightBoxImage(BoxData newState)
        {
            BitmapImage bitmapImage = null;

            switch (newState._state)
            {
                case LightBox.ConcealState:
                    bitmapImage = 
                        BlackboxImageUtils.Image(BlackboxImageType.LightBoxConceal);
                    break;
                case LightBox.DeflectionState:
                    bitmapImage = DeflectionImageSource(newState._position);
                    break;
                case LightBox.ComplexDeflectionState:
                    bitmapImage = ComplexDeflectionImageSource(newState._position);
                    break;
                case LightBox.DeflectionOutState:
                    bitmapImage = DeflectionOutImageSource(newState._position);
                    break;
                case LightBox.ComplexDeflectionOutState:
                    bitmapImage = ComplexDeflectionOutImageSource(newState._position);
                    break;
                case LightBox.ReflectionState:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxReflection);
                    break;
                case LightBox.ComplexReflectionState:
                    bitmapImage = ComplexReflectionImageSource(newState._position);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }

        private LightBoxDirection Direction(Position position)
        {
            LightBoxDirection direction = LightBoxDirection.LightBoxLeft;

            if (position.Row == 0)
            {
                direction = LightBoxDirection.LightBoxLeft;
            }
            else if (position.Column == 0)
            {
                direction = LightBoxDirection.LightBoxTop;
            }
            else if (position.Row == BlackboxConfig.GameBoardRow - 1)
            {
                direction = LightBoxDirection.LightBoxRight;
            }
            else
            {
                direction = LightBoxDirection.LightBoxBottom;
            }
            return direction;
        }

        private BitmapImage DeflectionImageSource(Position position)
        {
            BitmapImage bitmapImage = null;

            switch (Direction(position))
            {
                case LightBoxDirection.LightBoxLeft:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionRight);
                    break;
                case LightBoxDirection.LightBoxRight:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionLeft);
                    break;
                case LightBoxDirection.LightBoxTop:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionTop);
                    break;
                case LightBoxDirection.LightBoxBottom:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionBottom);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }

        private BitmapImage ComplexDeflectionImageSource(Position position)
        {
            BitmapImage bitmapImage = null;

            switch (Direction(position))
            {
                case LightBoxDirection.LightBoxLeft:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionRight);
                    break;
                case LightBoxDirection.LightBoxRight:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionLeft);
                    break;
                case LightBoxDirection.LightBoxTop:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionTop);
                    break;
                case LightBoxDirection.LightBoxBottom:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionBottom);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }

        private BitmapImage DeflectionOutImageSource(Position position)
        {
            BitmapImage bitmapImage = null;

            switch (Direction(position))
            {
                case LightBoxDirection.LightBoxLeft:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionLeft);
                    break;
                case LightBoxDirection.LightBoxRight:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionRight);
                    break;
                case LightBoxDirection.LightBoxTop:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionBottom);
                    break;
                case LightBoxDirection.LightBoxBottom:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxDeflectionTop);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }

        private BitmapImage ComplexDeflectionOutImageSource(Position position)
        {
            BitmapImage bitmapImage = null;

            switch (Direction(position))
            {
                case LightBoxDirection.LightBoxLeft:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionLeft);
                    break;
                case LightBoxDirection.LightBoxRight:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionRight);
                    break;
                case LightBoxDirection.LightBoxTop:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionBottom);
                    break;
                case LightBoxDirection.LightBoxBottom:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexDeflectionTop);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }

        private BitmapImage ComplexReflectionImageSource(Position position)
        {
            BitmapImage bitmapImage = null;

            switch (Direction(position))
            {
                case LightBoxDirection.LightBoxLeft:
                case LightBoxDirection.LightBoxRight:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexReflectionHoriz);
                    break;
                case LightBoxDirection.LightBoxTop:
                case LightBoxDirection.LightBoxBottom:
                    bitmapImage = BlackboxImageUtils.Image(BlackboxImageType.LightBoxComplexReflectionVert);
                    break;
                default:
                    break;
            }

            return bitmapImage;
        }
    }
}
