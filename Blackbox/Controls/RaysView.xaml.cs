using System.Windows.Controls;
using System.Windows;
using Blackbox.Engine;
using System.Windows.Shapes;
using System.Windows.Media;
using Blackbox.Controls;

namespace Blackbox
{
    public partial class RaysView : UserControl, IRaysObserver
    {
        public static readonly DependencyProperty StateValueProperty =
            DependencyProperty.Register(
                "StateValueProperty",
                typeof(object),
                typeof(RaysView),
                new PropertyMetadata(null, new PropertyChangedCallback(StateValueChanged)));

        private RaysLightSpot lightSpot;

        public void UpdateLightSpot(LightSpot lightSpot)
        {
            this.lightSpot.Visibility = lightSpot._visibility;
            this.lightSpot.SetXY(lightSpot._point.X, lightSpot._point.Y);
            this.lightSpot.SetStroke(this.path.Stroke);
        }

        public RaysView()
        {
            InitializeComponent();
            lightSpot = new RaysLightSpot();
            this.LayoutRoot.Children.Add(lightSpot);
        }

        static void StateValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var thisObj = obj as RaysView;
            thisObj.UpdateLightSpot((LightSpot)args.NewValue);
        }

        public void LightSpotUpdated(LightSpot lightSpot)
        {
            UpdateLightSpot(lightSpot);
        }
    }
}
