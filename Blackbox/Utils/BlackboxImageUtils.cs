
using System.Windows.Media.Imaging;
using System;

namespace Blackbox
{
    public enum BlackboxImageType
    {
        BlackBoxGuessed,
        BlackBoxGuessFailed,
        BlackBoxGuessing,
        BlackBoxGuessNone,
        LightBoxComplexDeflectionBottom,
        LightBoxComplexDeflectionLeft,
        LightBoxComplexDeflectionRight,
        LightBoxComplexDeflectionTop,
        LightBoxComplexReflectionHoriz,
        LightBoxComplexReflectionVert,
        LightBoxConceal,
        LightBoxDeflectionBottom,
        LightBoxDeflectionLeft,
        LightBoxDeflectionRight,
        LightBoxDeflectionTop,
        LightBoxReflection,
        NookBoxBackground,
        NookBoxForeground,
        MessageCelebrate,
        MessageCriticism,
        MessagePrompt
    }

    public enum MirrorImageType
    {
        MirrorGuessed,
        MirrorGuessFailed
    }

    public static class BlackboxImageUtils
    {
        private static BitmapImage _blackBoxGuessedImage;
        private static BitmapImage _blackBoxGuessFailedImage;
        private static BitmapImage _blackBoxGuessingImage;
        private static BitmapImage _blackBoxGuessNoneImage;
        private static BitmapImage _lightBoxComplexDeflectionBottomImage;
        private static BitmapImage _lightBoxComplexDeflectionLeftImage;
        private static BitmapImage _lightBoxComplexDeflectionRightImage;
        private static BitmapImage _lightBoxComplexDeflectionTopImage;
        private static BitmapImage _lightBoxComplexReflectionHorizImage;
        private static BitmapImage _lightBoxComplexReflectionVertImage;
        private static BitmapImage _lightBoxConcealImage;
        private static BitmapImage _lightBoxDeflectionBottomImage;
        private static BitmapImage _lightBoxDeflectionLeftImage;
        private static BitmapImage _lightBoxDeflectionRightImage;
        private static BitmapImage _lightBoxDeflectionTopImage;
        private static BitmapImage _lightBoxReflectionImage;
        private static BitmapImage _nookBoxBackgroundImage;
        private static BitmapImage _nookBoxForegroundImage;
        private static BitmapImage _messageCelebrateImage;
        private static BitmapImage _messageCriticismImage;
        private static BitmapImage _messagePromptImage;
        private static BitmapImage[] _mirrorGuessedImageArray;
        private static BitmapImage[] _mirrorGuessFailedImageArray;

        static BlackboxImageUtils()
        {
            _blackBoxGuessedImage = new BitmapImage(new Uri("/icons/Gameboard/BlackBox.Guessed.png", UriKind.Relative));
            _blackBoxGuessFailedImage = new BitmapImage(new Uri("/icons/Gameboard/BlackBox.GuessFailed.png", UriKind.Relative));
            _blackBoxGuessingImage = new BitmapImage(new Uri("/icons/Gameboard/BlackBox.Guessing.png", UriKind.Relative));
            _blackBoxGuessNoneImage = new BitmapImage(new Uri("/icons/Gameboard/BlackBox.GuessNone.png", UriKind.Relative));
            _lightBoxComplexDeflectionBottomImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexDeflection.Bottom.png", UriKind.Relative));
            _lightBoxComplexDeflectionLeftImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexDeflection.Left.png", UriKind.Relative));
            _lightBoxComplexDeflectionRightImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexDeflection.Right.png", UriKind.Relative));
            _lightBoxComplexDeflectionTopImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexDeflection.Top.png", UriKind.Relative));
            _lightBoxComplexReflectionHorizImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexReflection.Horiz.png", UriKind.Relative));
            _lightBoxComplexReflectionVertImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.ComplexReflection.Vert.png", UriKind.Relative));
            _lightBoxConcealImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Conceal.png", UriKind.Relative));
            _lightBoxDeflectionBottomImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Deflection.Bottom.png", UriKind.Relative));
            _lightBoxDeflectionLeftImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Deflection.Left.png", UriKind.Relative));
            _lightBoxDeflectionRightImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Deflection.Right.png", UriKind.Relative));
            _lightBoxDeflectionTopImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Deflection.Top.png", UriKind.Relative));
            _lightBoxReflectionImage = new BitmapImage(new Uri("/icons/Gameboard/LightBox.Reflection.png", UriKind.Relative));
            _nookBoxBackgroundImage = new BitmapImage(new Uri("/icons/Gameboard/NookBox.Background.png", UriKind.Relative));
            _nookBoxForegroundImage = new BitmapImage(new Uri("/icons/Gameboard/NookBox.Foreground.png", UriKind.Relative));
            _messageCelebrateImage = new BitmapImage(new Uri("/icons/Message/Message.Celebrate.png", UriKind.Relative));
            _messageCriticismImage = new BitmapImage(new Uri("/icons/Message/Message.Criticism.png", UriKind.Relative));
            _messagePromptImage = new BitmapImage(new Uri("/icons/Message/Message.Prompt.png", UriKind.Relative));

            _mirrorGuessedImageArray = new BitmapImage[5];
            _mirrorGuessFailedImageArray = new BitmapImage[5];
            for (int i = 0; i < 5; i++)
            {
                _mirrorGuessedImageArray[i] = new BitmapImage(
                    new Uri("/icons/Gameboard/Mirror/Mirror.Guessed." + (i + 1) + ".png", UriKind.Relative));
                _mirrorGuessFailedImageArray[i] = new BitmapImage(
                    new Uri("/icons/Gameboard/Mirror/Mirror.GuessFailed." + (i + 1) + ".png", UriKind.Relative));
            }
        }

        public static BitmapImage Image(BlackboxImageType type)
        {
            BitmapImage image = null;
            
            switch (type)
            {
                case BlackboxImageType.BlackBoxGuessed:
                    image = _blackBoxGuessedImage;
                    break;
                case BlackboxImageType.BlackBoxGuessFailed:
                    image = _blackBoxGuessFailedImage;
                    break;
                case BlackboxImageType.BlackBoxGuessing:
                    image = _blackBoxGuessingImage;
                    break;
                case BlackboxImageType.BlackBoxGuessNone:
                    image = _blackBoxGuessNoneImage;
                    break;
                case BlackboxImageType.LightBoxComplexDeflectionBottom:
                    image = _lightBoxComplexDeflectionBottomImage;
                    break;
                case BlackboxImageType.LightBoxComplexDeflectionLeft:
                    image = _lightBoxComplexDeflectionLeftImage;
                    break;
                case BlackboxImageType.LightBoxComplexDeflectionRight:
                    image = _lightBoxComplexDeflectionRightImage;
                    break;
                case BlackboxImageType.LightBoxComplexDeflectionTop:
                    image = _lightBoxComplexDeflectionTopImage;
                    break;
                case BlackboxImageType.LightBoxComplexReflectionHoriz:
                    image = _lightBoxComplexReflectionHorizImage;
                    break;
                case BlackboxImageType.LightBoxComplexReflectionVert:
                    image = _lightBoxComplexReflectionVertImage;
                    break;
                case BlackboxImageType.LightBoxConceal:
                    image = _lightBoxConcealImage;
                    break;
                case BlackboxImageType.LightBoxDeflectionBottom:
                    image = _lightBoxDeflectionBottomImage;
                    break;
                case BlackboxImageType.LightBoxDeflectionLeft:
                    image = _lightBoxDeflectionLeftImage;
                    break;
                case BlackboxImageType.LightBoxDeflectionRight:
                    image = _lightBoxDeflectionRightImage;
                    break;
                case BlackboxImageType.LightBoxDeflectionTop:
                    image = _lightBoxDeflectionTopImage;
                    break;
                case BlackboxImageType.LightBoxReflection:
                    image = _lightBoxReflectionImage;
                    break;
                case BlackboxImageType.NookBoxBackground:
                    image = _nookBoxBackgroundImage;
                    break;
                case BlackboxImageType.NookBoxForeground:
                    image = _nookBoxForegroundImage;
                    break;
                case BlackboxImageType.MessageCelebrate:
                    image = _messageCelebrateImage;
                    break;
                case BlackboxImageType.MessageCriticism:
                    image = _messageCriticismImage;
                    break;
                case BlackboxImageType.MessagePrompt:
                    image = _messagePromptImage;
                    break;
                default:
                    break;
            }
            return image;
        }

        public static BitmapImage[] Image(MirrorImageType type)
        {
            BitmapImage[] imageArray = null;

            switch (type)
            {
                case MirrorImageType.MirrorGuessed:
                    imageArray = _mirrorGuessedImageArray;
                    break;
                case MirrorImageType.MirrorGuessFailed:
                    imageArray = _mirrorGuessFailedImageArray;
                    break;
                default:
                    break;
            }

            return imageArray;
        }
    }
}
