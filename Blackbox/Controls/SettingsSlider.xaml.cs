using System.Windows;
using System.Windows.Controls;

namespace Blackbox
{
    public partial class SettingsSlider : UserControl
    {
        public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register(
                           "SliderValueProperty",
                           typeof(int),
                           typeof(SettingsSlider),
                           new PropertyMetadata(4, new PropertyChangedCallback(OnSliderValueChanged)));

        public int SliderValue
        {
            get
            {
                return (int)GetValue(SliderValueProperty); ;
            }
            set
            {
                SetValue(SliderValueProperty, value); 
            }
        }
        
        public SettingsSlider()
        {
            InitializeComponent();

            this.slider.Value = App.Settings.MirrorNumber;

            this.slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);
        }

        static void OnSliderValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var thisObj = obj as SettingsSlider;

            thisObj.slider.Value = (int)args.NewValue;
        }

        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue >= 3 && e.NewValue <= 3.5)
            {
                SliderValue = 3;
            }
            else if (e.NewValue > 3.5 && e.NewValue <= 4.5)
            {
                SliderValue = 4;
            }
            else
            {
                SliderValue = 5;
            }
        }

        private void slider_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.slider.Value >= 3 && this.slider.Value <= 3.5)
            {
                this.slider.Value = 3;
            }
            else if (this.slider.Value > 3.5 && this.slider.Value <= 4.5)
            {
                this.slider.Value = 4;
            }
            else
            {
                this.slider.Value = 5;
            }
        }
    }
}
