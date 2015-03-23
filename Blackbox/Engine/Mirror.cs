
namespace Blackbox
{
    public class Mirror : ViewModelBase
    {
        private Position _position;
        private bool _fullMirror;
        private bool _visible;
        private bool _guessed;

        public Position Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != (Position)value)
                {
                    _position = (Position)value;
                    NotifyPropertyChanged("Position");
                }
            }
        }

        public bool FullMirror
        {
            get
            {
                return _fullMirror;
            }
            set
            {
                if (_fullMirror != (bool)value)
                {
                    _fullMirror = (bool)value;
                    NotifyPropertyChanged("FullMirror");
                }
            }
        }

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (_visible != (bool)value)
                {
                    _visible = (bool)value;
                    NotifyPropertyChanged("Visible");
                }
            }
        }

        public bool Guessed
        {
            get
            {
                return _guessed;
            }
            set
            {
                if (_guessed != (bool)value)
                {
                    _guessed = (bool)value;
                    NotifyPropertyChanged("Guessed");
                }
            }
        }

        public Mirror(Position position)
        {
            Position = position;
            Visible = false;
            Guessed = false;
        }
    }
}
