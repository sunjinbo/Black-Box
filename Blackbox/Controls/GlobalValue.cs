using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Blackbox
{
    public class GlobalValue
    {
        public static double ViewWidth = 45;
        public static double ViewHeight = 45;
        /// <summary>
        /// 全局随机函数，从时间取得种子，保证每次都不一样
        /// </summary>
        public static Random random = new Random((int)DateTime.Now.Ticks);
        /// <summary>
        /// 起始透明度
        /// </summary>
        public static double OPACITY = 1;
        /// <summary>
        /// 每次添加多少个粒子
        /// </summary>
        public static double Dots_NUM = 20;
        /// <summary>
        /// 重力
        /// </summary>
        public static double GRAVITY = 0.3;
        /// <summary>
        /// 偏移X
        /// </summary>
        public static double X_VELOCITY = 5;
        /// <summary>
        /// 偏移Y
        /// </summary>
        public static double Y_VELOCITY = 5;
        /// <summary>
        /// 最小的半径
        /// </summary>
        public static double SIZE_MIN = 10;
        /// <summary>
        /// 最大的半径
        /// </summary>
        public static double SIZE_MAX = 25;
        /// <summary>
        /// 透明度衰减值
        /// </summary>
        public static double OpacityInc = -0.02;
        /// <summary>
        /// 自动释放的时间间隔
        /// </summary>
        public static double AutoIntervalTime = 50;
        /// <summary>
        /// 图片模式的资源取得
        /// </summary>
        public static ImageSource YellowStarImage =
            new BitmapImage(new Uri("/icons/Gameboard/Effects/Guessed.png", UriKind.Relative));
        public static ImageSource RedStarImage =
            new BitmapImage(new Uri("/icons/Gameboard/Effects/GuessFailed.png", UriKind.Relative));
        public static SolidColorBrush IsRColor = null;
    }
}
