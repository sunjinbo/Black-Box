using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Blackbox
{
    public enum SoundType
    {
        Started,
        LightboxActivated,
        GuessAdded,
        GuessRemoved,
        GuessTransferred,
        GuessWrongPlaced,
        GuessSuccess,
        GuessFailed,
        Won,
        Debunk,
        Restarted,
        AppbarClicked,
        AboutShown,
        GameOver
    }

    public class SoundManager
    {
        private SoundEffect startedSound;
        private SoundEffect lightboxActivatedSound;
        private SoundEffect guessAddedSound;
        private SoundEffect guessRemovedSound;
        private SoundEffect guessTransferredSound;
        private SoundEffect guessWrongPlacedSound;
        private SoundEffect guessSuccessSound;
        private SoundEffect guessFailedSound;
        private SoundEffect wonSound;
        private SoundEffect debunkSound;
        private SoundEffect restartedSound;
        private SoundEffect appbarClickedSound;
        private SoundEffect aboutShownSound;
        private SoundEffect gameOverSound;
        private SoundEffect backgroundMusic;
        private SoundEffectInstance backgroundMusicInstance;
        private bool mute = true;

        public void Play(SoundType type)
        {
            if (mute) return;

            try
            {
                switch (type)
                {
                    case SoundType.Started:
                        startedSound.Play();
                        break;
                    case SoundType.LightboxActivated:
                        lightboxActivatedSound.Play();
                        break;
                    case SoundType.GuessAdded:
                        guessAddedSound.Play();
                        break;
                    case SoundType.GuessRemoved:
                        guessRemovedSound.Play();
                        break;
                    case SoundType.GuessTransferred:
                        guessTransferredSound.Play();
                        break;
                    case SoundType.GuessWrongPlaced:
                        guessWrongPlacedSound.Play();
                        break;
                    case SoundType.GuessSuccess:
                        guessSuccessSound.Play();
                        break;
                    case SoundType.GuessFailed:
                        guessFailedSound.Play();
                        break;
                    case SoundType.Won:
                        wonSound.Play();
                        break;
                    case SoundType.Debunk:
                        debunkSound.Play();
                        break;
                    case SoundType.Restarted:
                        restartedSound.Play();
                        break;
                    case SoundType.AppbarClicked:
                        appbarClickedSound.Play();
                        break;
                    case SoundType.AboutShown:
                        aboutShownSound.Play();
                        break;
                    case SoundType.GameOver:
                        gameOverSound.Play();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Unmute()
        {
            try
            {
                mute = false;

                if (backgroundMusicInstance.State == SoundState.Paused)
                {
                    backgroundMusicInstance.Resume();
                }
                else
                {
                    backgroundMusicInstance.Play();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Mute()
        {
            try
            {
                mute = true;

                if (backgroundMusicInstance.State == SoundState.Playing)
                {
                    backgroundMusicInstance.Pause();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public SoundManager()
        {
            LoadSound("Sounds/started.wav", out startedSound);
            LoadSound("Sounds/lightbox.activated.wav", out lightboxActivatedSound);
            LoadSound("Sounds/guess.added.wav", out guessAddedSound);
            LoadSound("Sounds/guess.removed.wav", out guessRemovedSound);
            LoadSound("Sounds/guess.transferred.wav", out guessTransferredSound);
            LoadSound("Sounds/guess.wrong.placed.wav", out guessWrongPlacedSound);
            LoadSound("Sounds/guess.success.wav", out guessSuccessSound);
            LoadSound("Sounds/guess.failed.wav", out guessFailedSound);
            LoadSound("Sounds/won.wav", out wonSound);
            LoadSound("Sounds/debunk.wav", out debunkSound);
            LoadSound("Sounds/restarted.wav", out restartedSound);
            LoadSound("Sounds/appbar.clicked.wav", out appbarClickedSound);
            LoadSound("Sounds/about.shown.wav", out aboutShownSound);
            LoadSound("Sounds/game.over.wav", out gameOverSound);
            LoadSoundInstance("Sounds/background.music.wav", out backgroundMusic, out backgroundMusicInstance);

            // Set the volume a little lower than full so it becomes the background.
            backgroundMusicInstance.Volume = 0.7f;

            // Turn on looping so it runs continually in the background.
            backgroundMusicInstance.IsLooped = true;

            // Timer to simulate the XNA game loop (SoundEffect classes are from the XNA Framework)
            DispatcherTimer XnaDispatchTimer = new DispatcherTimer();
            XnaDispatchTimer.Interval = TimeSpan.FromMilliseconds(50);

            // Call FrameworkDispatcher.Update to update the XNA Framework internals.
            XnaDispatchTimer.Tick += delegate { try { FrameworkDispatcher.Update(); } catch { } };

            // Start the DispatchTimer running.
            XnaDispatchTimer.Start();
        }

        /// <summary>
        /// Loads a wav file into an XNA Framework SoundEffect.
        /// </summary>
        /// <param name="SoundFilePath">Relative path to the wav file.</param>
        /// <param name="Sound">The SoundEffect to load the audio into.</param>
        private void LoadSound(String SoundFilePath, out SoundEffect Sound)
        {
            // For error checking, assume we'll fail to load the file.
            Sound = null;

            try
            {
                // Holds informations about a file stream.
                StreamResourceInfo SoundFileInfo = 
                    App.GetResourceStream(new Uri(SoundFilePath, UriKind.Relative));

                // Create the SoundEffect from the Stream
                Sound = SoundEffect.FromStream(SoundFileInfo.Stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Loads a wav file into an XNA Framework SoundEffect.
        /// Then, creates a SoundEffectInstance from the SoundEffect.
        /// </summary>
        /// <param name="SoundFilePath">Relative path to the wav file.</param>
        /// <param name="Sound">The SoundEffect to load the audio into.</param>
        /// <param name="SoundInstance">The SoundEffectInstance to create from Sound.</param>
        private void LoadSoundInstance(
            String SoundFilePath, out SoundEffect Sound, out SoundEffectInstance SoundInstance)
        {
            // For error checking, assume we'll fail to load the file.
            Sound = null;
            SoundInstance = null;

            try
            {
                LoadSound(SoundFilePath, out Sound);
                SoundInstance = Sound.CreateInstance();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
