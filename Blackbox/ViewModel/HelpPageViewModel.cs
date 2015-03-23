
using System.Windows;
namespace Blackbox.ViewModel
{
    public enum AppbarState
    {
        PreviousEnabled,
        PreviousDisabled,
        NextEnabled,
        NextDisabled
    }
    public delegate void AppbarStateUpdatedEventHandler(AppbarState state);

    public class HelpPageViewModel : ViewModelBase
    {
        public event AppbarStateUpdatedEventHandler AppbarStateUpdated;

        private string _index;
        private string _title;
        private string _illustration;
        private string _details;
        private Visibility _hasAbout;
        private HelpDataItemCollection _collection;

        public string Index 
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                NotifyPropertyChanged("Index");
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string Illustration
        {
            get
            {
                return _illustration;
            }
            set
            {
                _illustration = value;
                NotifyPropertyChanged("Illustration");
            }
        }

        public string Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
                NotifyPropertyChanged("Details");
            }
        }

        public Visibility HasAbout
        {
            get
            {
                return _hasAbout;
            }
            set
            {
                _hasAbout = value;
                NotifyPropertyChanged("HasAbout");
            }
        }
        public void UpdateAppbarStates()
        {
            if (_collection.Count() > _collection.Current())
            {
                NotifyAppbarStateUpdated(AppbarState.NextEnabled);
            }

            if (1 == _collection.Current())
            {
                NotifyAppbarStateUpdated(AppbarState.PreviousDisabled);
            }

            if (_collection.Count() == _collection.Current())
            {
                NotifyAppbarStateUpdated(AppbarState.NextDisabled);
            }

            if (1 < _collection.Current())
            {
                NotifyAppbarStateUpdated(AppbarState.PreviousEnabled);
            }
        }

        public void Previous()
        {
            if (_collection.MovePrevious())
            {
                Fill();
            }

            UpdateAppbarStates();
        }

        public void Next()
        {
            if (_collection.MoveNext())
            {
                Fill();
            }

            UpdateAppbarStates();
        }

        public HelpPageViewModel()
        {
            _collection = new HelpDataItemCollection();
            _collection.Add(new HelpDataItem(
                AppResources.IntroductionHelpTitleText,
                string.Empty,
                AppResources.IntroductionHelpDetailsText
                ));

            _collection.Add(new HelpDataItem(
                AppResources.TargetHelpTitleText,
                string.Empty,
                AppResources.TargetHelpDetailsText
                ));

            _collection.Add(new HelpDataItem(
                AppResources.MissHelpItemTitleText,
                "/Icons/Help/Help.Miss.png",
                AppResources.MissHelpItemDetailsText
                ));

            _collection.Add(new HelpDataItem(
                AppResources.HitHelpItemTitleText,
                "/Icons/Help/Help.Hit.png",
                AppResources.HitHelpItemDetailsText
                ));

            _collection.Add(new HelpDataItem(
                AppResources.DeflectionHelpItemTitleText,
                "/Icons/Help/Help.Deflection.png",
                AppResources.DeflectionHelpItemDetailsText
                ));

            // Double deflection
            // Reflection
            // Detour

            Fill();
        }

        private void Fill()
        {
            HelpDataItem dataItem = _collection.DataItem();
            Index = _collection.Current() + @"/" + _collection.Count();
            Title = dataItem.Title;
            Illustration = dataItem.Illustration;
            Details = dataItem.Details;

            // Only the first page has the About button.
            if (_collection.Current() == 1)
            {
                HasAbout = Visibility.Visible;
            }
            else
            {
                HasAbout = Visibility.Collapsed;
            }
        }

        private void NotifyAppbarStateUpdated(AppbarState state)
        {
            if (AppbarStateUpdated != null)
            {
                AppbarStateUpdated.Invoke(state);
            }
        }
    }
}
