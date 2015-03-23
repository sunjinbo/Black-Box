using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Blackbox
{
    public partial class MirrorView : UserControl
    {
        public static readonly DependencyProperty StateValueProperty =
            DependencyProperty.Register(
                "StateValueProperty",
                typeof(object),
                typeof(MirrorView),
                new PropertyMetadata(null, new PropertyChangedCallback(StateValueChanged)));

        private DispatcherTimer timer;
        private bool reverse = false;
        private bool guessed = false;
        private int currentImage = 0;
        private BitmapImage[] imageArray = null;
        private SolidColorBrush brush = null;
        private const double BoldStrokeThickness = 3.0;
        private const double NormalStrokeThickness = 2.0;
        private SolidColorBrush guessedBrush = new SolidColorBrush(Color.FromArgb(255, 255, 201, 14));
        private SolidColorBrush guessFailedBrush = new SolidColorBrush(Color.FromArgb(255, 237, 28, 36));

        public void SetGuessedState(bool guessed)
        {
            this.guessed = guessed;
            if (guessed)
            {
                imageArray = BlackboxImageUtils.Image(MirrorImageType.MirrorGuessed);
                brush = guessedBrush;
            }
            else
            {
                imageArray = BlackboxImageUtils.Image(MirrorImageType.MirrorGuessFailed);
                brush = guessFailedBrush;
            }
        }

        public MirrorView()
        {
            InitializeComponent();

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(500);
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (reverse)
            {
                this.path.StrokeThickness = BoldStrokeThickness;
                this.path.Stroke = brush;
                reverse = false;
            }
            else
            {
                this.path.StrokeThickness = NormalStrokeThickness;
                this.path.Stroke = brush;
                reverse = true;
            }

            this.image.Source = imageArray[currentImage];

            --currentImage;
            if (currentImage < 0)
            {
                currentImage = imageArray.Length - 1;
            }
        }

        static void StateValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var thisObj = obj as MirrorView;
            thisObj.SetGuessedState((bool)args.NewValue);
        }
    }
}
