using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Blackbox
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            this.SupportedOrientations = SupportedPageOrientation.Portrait;
            this.Projection = new PlaneProjection();

            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.IsMenuEnabled = true;
            this.ApplicationBar.IsVisible = true;
            this.ApplicationBar.Opacity = 1.0;
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.BackgroundColor = Color.FromArgb(0xFF, 0x31, 0x99, 0xCC);

            ApplicationBarIconButton settings = 
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.settings.png", UriKind.Relative));
            settings.Text = AppResources.SettingsApplicationBarText;
            settings.Click += new EventHandler(settings_Click);

            ApplicationBarIconButton help = 
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.help.png", UriKind.Relative));
            help.Text = AppResources.HelpApplicationBarText;
            help.Click += new EventHandler(help_Click);

            this.ApplicationBar.Buttons.Add(settings);
            this.ApplicationBar.Buttons.Add(help);

            this.startButton.Background = new SolidColorBrush(Color.FromArgb(255, 49, 153, 204));
            this.startButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 49, 153, 204));
        }

        void settings_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.NavigationService.Navigate(
                new Uri("/Blackbox;component/Pages/SettingsPage.xaml", UriKind.Relative));
        }

        void help_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.NavigationService.Navigate(
                new Uri("/Blackbox;component/Pages/HelpPage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Opacity = 1.0;
            PlaneProjection theProjection
                = (PlaneProjection)this.Projection;
            theProjection.RotationX = 0;
            this.ApplicationBar.IsVisible = true;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.LongVibrate);
            App.SoundManager.Play(SoundType.Started);
            WelcomePageSwitching.Completed += new EventHandler(WelcomePageSwitching_Completed);
            this.ApplicationBar.IsVisible = false;
            WelcomePageSwitching.Begin();
        }

        void WelcomePageSwitching_Completed(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(
                new Uri("/Blackbox;component/Pages/GamePage.xaml", UriKind.Relative));
        }
    }
}
