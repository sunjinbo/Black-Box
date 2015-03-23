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
using Microsoft.Devices;

namespace Blackbox
{
    public class VibrateManager
    {
        public enum VibrateType
        {
            ShortVibrate,
            LongVibrate
        }

        private VibrateController vc;
        
        public bool IsVibrate{ get;set; }

        public void Vibrate(VibrateType type)
        {
            if (!IsVibrate) return;

            switch (type)
            {
                case VibrateType.ShortVibrate:
                    vc.Start(TimeSpan.FromMilliseconds(100));
                    break;
                case VibrateType.LongVibrate:
                    vc.Start(TimeSpan.FromMilliseconds(300));
                    break;
                default:
                    break;
            }
        }

        public VibrateManager()
        {
            IsVibrate = false;
            vc = VibrateController.Default;
        }
    }
}
