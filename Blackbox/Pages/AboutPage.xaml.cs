using System;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Blackbox
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();

            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            this.SupportedOrientations = SupportedPageOrientation.Portrait;

            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.IsMenuEnabled = true;
            this.ApplicationBar.IsVisible = true;
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.BackgroundColor = Color.FromArgb(0xFF, 0x31, 0x99, 0xCC);

            ApplicationBarIconButton ok =
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.ok.png", UriKind.Relative));
            ok.Text = AppResources.OkApplicationBarText;
            ok.Click += new EventHandler(ok_Click);

            this.ApplicationBar.Buttons.Add(ok);

            this.Loaded += new System.Windows.RoutedEventHandler(AboutPage_Loaded);
        }

        void AboutPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Storyboard.Begin();
        }

        void ok_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.NavigationService.GoBack();
        }

        private void hyperlinkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wbTask = new WebBrowserTask();
            wbTask.Uri = new Uri("http://www.pandagame.com", UriKind.RelativeOrAbsolute);
            wbTask.Show();
        }
    }
}
