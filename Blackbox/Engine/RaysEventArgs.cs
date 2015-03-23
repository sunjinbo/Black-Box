using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Blackbox
{
    public class RaysEventArgs : EventArgs
    {
        #region Rays attributes
        public Rays Rays
        {
            get;
            set;
        }
        #endregion

        public RaysEventArgs(Rays rays)
        {
            Rays = rays;
        }
    }
}
