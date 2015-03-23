using System;
using System.Windows.Data;

namespace Blackbox
{
    public class BoxStateToImageSourceConverter : IValueConverter
    {
        private enum LightBoxDirection
        {
            LightBoxLeft,
            LightBoxRight,
            LightBoxTop,
            LightBoxBottom
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imgSrc = string.Empty;

            BoxData boxData = (BoxData)value;
            Position position = boxData._position;

            switch (boxData._state)
            {
                case NookBox.NookState:
                    imgSrc = "/icons/NookBox.NookState.png";
                    break;
                case BlackBox.GuessNoneState:
                    imgSrc = "/icons/BlackBox.GuessNone.png";
                    break;
                case BlackBox.GuessingState:
                    imgSrc = "/icons/BlackBox.Guessing.png";
                    break;
                case BlackBox.GuessFailedState:
                    imgSrc = "/icons/BlackBox.GuessFailed.png";
                    break;
                case BlackBox.GuessedState:
                    imgSrc = "/icons/BlackBox.Guessed.png";
                    break;
                case LightBox.ConcealState:
                    imgSrc = "/icons/LightBox.Conceal.png";
                    break;
                case LightBox.DeflectionState:
                    imgSrc = DeflectionImageSource(position);
                    break;
                case LightBox.ComplexDeflectionState:
                    imgSrc = ComplexDeflectionImageSource(position);
                    break;
                case LightBox.DeflectionOutState:
                    imgSrc = DeflectionOutImageSource(position);
                    break;
                case LightBox.ComplexDeflectionOutState:
                    imgSrc = ComplexDeflectionOutImageSource(position);
                    break;
                case LightBox.ReflectionState:
                    imgSrc = "/icons/LightBox.Reflection.png";
                    break;
                case LightBox.ComplexReflectionState:
                    imgSrc = ComplexReflectionImageSource(position);
                    break;
                default:
                    break;
            }

            return imgSrc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
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

        private string DeflectionImageSource(Position position)
        {
            string imgSrc = string.Empty;
            LightBoxDirection direction = Direction(position);
            switch (direction)
            {
                case LightBoxDirection.LightBoxLeft:
                    imgSrc = "/icons/LightBox.Deflection.Right.png";
                    break;
                case LightBoxDirection.LightBoxRight:
                    imgSrc = "/icons/LightBox.Deflection.Left.png";
                    break;
                case LightBoxDirection.LightBoxTop:
                    imgSrc = "/icons/LightBox.Deflection.Bottom.png";
                    break;
                case LightBoxDirection.LightBoxBottom:
                    imgSrc = "/icons/LightBox.Deflection.Top.png";
                    break;
                default:
                    break;
            }
            return imgSrc;
        }

        private string ComplexDeflectionImageSource(Position position)
        {
            string imgSrc = string.Empty;
            LightBoxDirection direction = Direction(position);
            switch (direction)
            {
                case LightBoxDirection.LightBoxLeft:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Right.png";
                    break;
                case LightBoxDirection.LightBoxRight:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Left.png";
                    break;
                case LightBoxDirection.LightBoxTop:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Bottom.png";
                    break;
                case LightBoxDirection.LightBoxBottom:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Top.png";
                    break;
                default:
                    break;
            }
            return imgSrc;
        }

        private string DeflectionOutImageSource(Position position)
        {
            string imgSrc = string.Empty;
            LightBoxDirection direction = Direction(position);
            switch (direction)
            {
                case LightBoxDirection.LightBoxLeft:
                    imgSrc = "/icons/LightBox.Deflection.Left.png";
                    break;
                case LightBoxDirection.LightBoxRight:
                    imgSrc = "/icons/LightBox.Deflection.Right.png";
                    break;
                case LightBoxDirection.LightBoxTop:
                    imgSrc = "/icons/LightBox.Deflection.Top.png";
                    break;
                case LightBoxDirection.LightBoxBottom:
                    imgSrc = "/icons/LightBox.Deflection.Bottom.png";
                    break;
                default:
                    break;
            }
            return imgSrc;
        }

        private string ComplexDeflectionOutImageSource(Position position)
        {
            string imgSrc = string.Empty;
            LightBoxDirection direction = Direction(position);
            switch (direction)
            {
                case LightBoxDirection.LightBoxLeft:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Left.png";
                    break;
                case LightBoxDirection.LightBoxRight:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Right.png";
                    break;
                case LightBoxDirection.LightBoxTop:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Top.png";
                    break;
                case LightBoxDirection.LightBoxBottom:
                    imgSrc = "/icons/LightBox.ComplexDeflection.Bottom.png";
                    break;
                default:
                    break;
            }
            return imgSrc;
        }

        private string ComplexReflectionImageSource(Position position)
        {
            string imgSrc = string.Empty;
            LightBoxDirection direction = Direction(position);
            switch (direction)
            {
                case LightBoxDirection.LightBoxLeft:
                case LightBoxDirection.LightBoxRight:
                    imgSrc = "/icons/LightBox.ComplexReflection.Horiz.png";
                    break;
                case LightBoxDirection.LightBoxTop:
                case LightBoxDirection.LightBoxBottom:
                    imgSrc = "/icons/LightBox.ComplexReflection.Vert.png";
                    break;
                default:
                    break;
            }
            return imgSrc;
        }
    }
}
