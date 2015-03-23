using System.Windows.Controls;

namespace Blackbox.Controls
{
    public partial class LimitedRaysControl : UserControl
    {
        private int _currentRays;

        public bool LimitedRays
        {
            get;
            set;
        }

        public int CurrentRays
        {
            get
            {
                return _currentRays;
            }
            set
            {
                if (LimitedRays)
                {
                    if ((int)value <= BlackboxConfig.DefaultMaxRays)
                    {
                        _currentRays = (int)value;
                    }
                }
                else
                {
                    _currentRays = (int)value;
                }

                UpdateRaysText();
            }
        }

        public void Restart()
        {
            CurrentRays = 0;
        }

        public LimitedRaysControl()
        {
            InitializeComponent();
        }

        private void UpdateRaysText()
        {
            string text = string.Empty;

            text += _currentRays;
            text += @"/";
            text += (LimitedRays ? BlackboxConfig.DefaultMaxRays.ToString() : "∞");

            textBlock1.Text = text;
        }
    }
}
