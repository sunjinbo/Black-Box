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

namespace Blackbox.Utils
{
    public static class BlackboxUtils
    {
        private static Random random = new Random();
        public static Random Random
        {
            get
            {
                return random;
            }
        }
    }
}
