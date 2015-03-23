using System.Windows;

namespace Blackbox
{
    public interface IBoxHelper
    {
        Box Box(int row, int column);
        Rect BoxRect(int row, int column);
        bool HasMirror(int row, int column);
        int RaysCount();
        int MaxRays();
        GuessType GuessType();
        int MirrorNumber();
        bool UnlimitedRays();
        void RaysCreated(Rays rays);
        bool LayoutGuess();
    }
}
