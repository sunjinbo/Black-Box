using System;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Blackbox
{
    public partial class GamePage : PhoneApplicationPage
    {
        private ApplicationBarIconButton guess;
        private ApplicationBarIconButton debunk;
        private ApplicationBarIconButton restart;

        public GamePage()
        {
            InitializeComponent();

            this.ApplicationBar = new ApplicationBar();
            this.ApplicationBar.IsMenuEnabled = true;
            this.ApplicationBar.IsVisible = true;
            this.ApplicationBar.ForegroundColor = Colors.White;
            this.ApplicationBar.BackgroundColor = Color.FromArgb(0xFF, 0x31, 0x99, 0xCC);

            guess = new ApplicationBarIconButton();
            guess.Click += new EventHandler(OnGuessClick);
            guess.IconUri = new Uri("/icons/Appbar/Appbar.guess.png", UriKind.Relative);
            guess.Text = AppResources.GuessApplicationBarText;
            guess.IsEnabled = false;

            debunk = new ApplicationBarIconButton();
            debunk.Click += new EventHandler(OnDebunkClick);
            debunk.IconUri = new Uri("/icons/Appbar/Appbar.debunk.png", UriKind.Relative);
            debunk.Text = AppResources.DebunkApplicationBarText;

            restart = new ApplicationBarIconButton();
            restart.Click += new EventHandler(OnRestartClick);
            restart.IconUri = new Uri("/icons/Appbar/Appbar.restart.png", UriKind.Relative);
            restart.Text = AppResources.RestartApplicationBarText;

            this.ApplicationBar.Buttons.Add(guess);
            this.ApplicationBar.Buttons.Add(debunk);
            this.ApplicationBar.Buttons.Add(restart);

            gameBoardView.SetObserver(this);

            limitedRaysControl1.LimitedRays = !App.Settings.UnlimitedRays;
            limitedRaysControl1.CurrentRays = 0;
        }

        public void StateChanged(object sender, ModelStateEventArgs args)
        {
            switch (args.State)
            {
                case ModelState.NoRayExplored:
                    {
                        flyMessageControl1.AddMessage(
                            "您需要先发射一束激光！", FlyMessageType.Prompt);

                        gameBoardView.Accelerate();

                    }
                    break;

                case ModelState.MaxRaysExceed:
                    {
                    }
                    break;

                case ModelState.LightBoxActivated:
                    {
                        App.SoundManager.Play(SoundType.LightboxActivated);
                    }
                    break;

                case ModelState.MaxMirrorsExceed: // fall through
                case ModelState.BlackBoxActivated: // fall through
                    break;

                case ModelState.GuessAdded:
                    {
                        App.SoundManager.Play(SoundType.GuessAdded);
                    }
                    break;

                case ModelState.GuessRemoved:
                    {
                        App.SoundManager.Play(SoundType.GuessRemoved);
                        guess.IsEnabled = false;
                    }
                    break;

                case ModelState.GuessCompleted:
                    {
                        guess.IsEnabled = true;
                    }
                    break;

                case ModelState.GuessTransferred:
                    {
                        App.SoundManager.Play(SoundType.GuessTransferred);
                    }
                    break;

                case ModelState.GuessSuccess:
                    {
                        App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
                        App.SoundManager.Play(SoundType.GuessSuccess);
                    }
                    break;

                case ModelState.GuessFailed:
                    {
                        App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
                        App.SoundManager.Play(SoundType.GuessFailed);
                        if (App.Settings.GuessType == GuessType.AllAtOnce)
                        {
                            flyMessageControl1.AddMessage(
                                string.Format("You guessed {0} mirrors.", args.Message), 
                                FlyMessageType.Prompt
                                );
                        }
                    }
                    break;

                case ModelState.RaysCreated:
                    {
                        limitedRaysControl1.CurrentRays += 1;
                    }
                    break;

                case ModelState.GameOver:
                    {
                        App.VibrateManager.Vibrate(VibrateManager.VibrateType.LongVibrate);
                        App.SoundManager.Play(SoundType.GameOver);
                        gameBoardView.Debunk();
                        debunk.IsEnabled = false;
                    }
                    break;

                default:
                    break;
            }
        }

        private void OnGuessClick(object sender, System.EventArgs e)
        {
            gameBoardView.DoGuess();
            guess.IsEnabled = false;
        }

        private void OnDebunkClick(object sender, System.EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.Debunk);
            gameBoardView.Debunk();
            debunk.IsEnabled = false;
        }

        private void OnRestartClick(object sender, System.EventArgs e)
        {
            App.VibrateManager.Vibrate(VibrateManager.VibrateType.ShortVibrate);
            App.SoundManager.Play(SoundType.Restarted);
            guess.IsEnabled = false;
            debunk.IsEnabled = true;
            gameBoardView.Restart();
            limitedRaysControl1.Restart();
        }
    }
}
