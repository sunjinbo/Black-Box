using System;
using System.Windows.Media;
using Blackbox.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Blackbox
{
    public partial class HelpPage : PhoneApplicationPage
    {
        HelpPageViewModel viewModel;
        ApplicationBarIconButton previous;
        ApplicationBarIconButton next;

        public HelpPage()
        {
            InitializeComponent();

            this.viewModel = new HelpPageViewModel();
            this.viewModel.AppbarStateUpdated += 
                new AppbarStateUpdatedEventHandler(viewModel_AppbarStateUpdated);
            this.DataContext = viewModel;

            this.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            this.SupportedOrientations = SupportedPageOrientation.Portrait;

            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.IsMenuEnabled = true;
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.BackgroundColor = Color.FromArgb(0xFF, 0x31, 0x99, 0xCC);

            previous =
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.previous.png", UriKind.Relative));
            previous.Text = AppResources.PreviousApplicationBarText;
            previous.Click += new EventHandler(previous_Click);

            next =
                new ApplicationBarIconButton(new Uri("/icons/Appbar/Appbar.next.png", UriKind.Relative));
            next.Text = AppResources.NextApplicationBarText;
            next.Click += new EventHandler(next_Click);

            this.ApplicationBar.Buttons.Add(previous);
            this.ApplicationBar.Buttons.Add(next);

            this.TitleTextBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 140, 138, 140));
            this.DetailsTextBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));

            this.AboutButton.Background = new SolidColorBrush(Color.FromArgb(255, 49, 153, 204));
            this.AboutButton.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 49, 153, 204));
            this.AboutButton.Foreground = new SolidColorBrush(Colors.White);

            this.viewModel.UpdateAppbarStates();
        }

        void viewModel_AppbarStateUpdated(AppbarState state)
        {
            switch (state)
            {
                case AppbarState.PreviousDisabled:
                    previous.IsEnabled = false;
                    break;
                case AppbarState.PreviousEnabled:
                    previous.IsEnabled = true;
                    break;
                case AppbarState.NextDisabled:
                    next.IsEnabled = false;
                    break;
                case AppbarState.NextEnabled:
                    next.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        void previous_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.viewModel.Previous();
        }

        void next_Click(object sender, EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AppbarClicked);
            this.viewModel.Next();
        }

        private void AboutButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.AboutShown);
            this.NavigationService.Navigate(
                new Uri("/Blackbox;component/Pages/AboutPage.xaml", UriKind.Relative));
        }
    }
}
