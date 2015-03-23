using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace Blackbox
{
    public enum GuessType
    {
        AllAtOnce,
        AllAtTime
    }

    public enum DifficultyType
    {
        Easy,
        Challenge
    }

    public class BlackboxSettings : INotifyPropertyChanged
    {
        #region member data
        private const string NotFirstTimeKey = "NotFirstTime";
        private const string GuessTypeKey = "GuessType";
        private const string MirrorNumberKey = "MirrorNumber";
        private const string UnlimitedRaysKey = "UnlimitedRays";
        private const string DifficultyKey = "Difficulty";
        private const string SoundKey = "Sound";
        private const string VibrateKey = "Vibrate";
        private ObservableCollection<string> _guessTypeList = null;
        #endregion

        #region constructor
        public BlackboxSettings()
        {
            _guessTypeList = new ObservableCollection<string>();
            _guessTypeList.Add(AppResources.AllAtOnceText);
            _guessTypeList.Add(AppResources.AllAtTimeText);

            try
            {
                if (!NotFirstTime)
                {
                    NotFirstTime = true;

                    // Sets to default value
                    GuessType = BlackboxConfig.DefaultGuessType;
                    MirrorNumber = BlackboxConfig.DefaultMirrorNumber;
                    UnlimitedRays = BlackboxConfig.DefaultUnlimitedRays;
                    Difficulty = BlackboxConfig.DefalutDifficulty;
                    Sound = BlackboxConfig.DefaultSound;
                    Vibrate = BlackboxConfig.DefaultVibrate;
                }
            }
            catch (IsolatedStorageException ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        #region retrieve setting generic method
        private T RetrieveSetting<T>(string settingKey)
        {
            object settingValue;
            try
            {
                if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(settingKey, out settingValue))
                {
                    return (T)settingValue;
                }
            }
            catch (IsolatedStorageException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return default(T);
        }
        #endregion

        #region not first time
        public bool NotFirstTime
        {
            get
            {
                return RetrieveSetting<bool>(NotFirstTimeKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[NotFirstTimeKey] = value;
                RaisePropertyChanged("NotFirstTime");
            }
        }
        #endregion

        #region Guess type list
        public ObservableCollection<string> GuessTypeList
        {
            get 
            { 
                return _guessTypeList; 
            }
            private set
            {
            }
        }
        #endregion

        #region Guess type
        public GuessType GuessType
        {
            get
            {
                return RetrieveSetting<GuessType>(GuessTypeKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[GuessTypeKey] = value;
                RaisePropertyChanged("GuessType");
            }
        }
        #endregion

        #region Number of mirrors
        public int MirrorNumber
        {
            get
            {
                return RetrieveSetting<int>(MirrorNumberKey);
            }
            set
            {
                int mirrorNumber = (int)value;

                if (mirrorNumber >= 3 && mirrorNumber <= 5)
                {
                    IsolatedStorageSettings.ApplicationSettings[MirrorNumberKey] = value;
                    RaisePropertyChanged("MirrorNumber");
                }
            }
        }
        #endregion

        #region Unlimited rays
        public bool UnlimitedRays
        {
            get
            {
                return RetrieveSetting<bool>(UnlimitedRaysKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[UnlimitedRaysKey] = value;
                RaisePropertyChanged("UnlimitedRays");
            }
        }
        #endregion

        #region Difficulty
        public DifficultyType Difficulty
        {
            get
            {
                return RetrieveSetting<DifficultyType>(DifficultyKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[DifficultyKey] = value;
                RaisePropertyChanged("Difficulty");
            }
        }
        #endregion

        #region Sound
        public bool Sound
        {
            get
            {
                return RetrieveSetting<bool>(SoundKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[SoundKey] = value;
                RaisePropertyChanged("Sound");
            }
        }
        #endregion

        #region Vibrate
        public bool Vibrate
        {
            get
            {
                return RetrieveSetting<bool>(VibrateKey);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings[VibrateKey] = value;
                RaisePropertyChanged("Vibrate");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
