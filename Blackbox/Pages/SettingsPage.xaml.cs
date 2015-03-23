using System;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Blackbox
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = App.Settings;

            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            this.SupportedOrientations = SupportedPageOrientation.Portrait;

            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.IsMenuEnabled = true;
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.BackgroundColor = Color.FromArgb(0xFF, 0x31, 0x99, 0xCC);

            ApplicationBarIconButton ok =
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.ok.png", UriKind.Relative));
            ok.Text = AppResources.OkApplicationBarText;
            ok.Click += new EventHandler(ok_Click);

            this.ApplicationBar.Buttons.Add(ok);
        }

        void ok_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.NavigationService.GoBack();
        }

        private void soundToggleSwitch_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            App.SoundManager.Unmute();
        }

        private void soundToggleSwitch_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            App.SoundManager.Mute();
        }

        private void vibrateToggleSwitch_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            App.VibrateManager.IsVibrate = true;
        }

        private void vibrateToggleSwitch_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            App.VibrateManager.IsVibrate = false;
        }
    }
}
