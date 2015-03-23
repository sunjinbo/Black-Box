using System.Windows.Controls;
using System.Windows;

namespace Blackbox
{
    public abstract partial class BoxView : UserControl
    {
        public static readonly DependencyProperty StateValueProperty = 
            DependencyProperty.Register(
                   "StateValueProperty",
                   typeof(BoxData),
                   typeof(BoxView),
                   new PropertyMetadata(null, new PropertyChangedCallback(StateValueChanged)));

        public BoxView()
        {
            InitializeComponent();
        }

        public abstract void UpdateStateValue(BoxData newState);

        static void StateValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var thisObj = obj as BoxView;
            thisObj.UpdateStateValue((BoxData)args.NewValue);
        }
    }
}
