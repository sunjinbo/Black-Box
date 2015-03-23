
namespace Blackbox
{
    public static class BlackboxConfig
    {
        #region game board specification
        public const int GameBoardRow = 10;
        public const int GameBoardColumn = 10;
        #endregion

        #region welcome promo
        public const int PromoRow = 10;
        public const int PromoColumn = 4;
        #endregion

        #region box width and height
        public const int BoxWidth = 45;
        public const int BoxHeight = 45;
        #endregion

        #region puzzle gap
        public const int PuzzleEasyGap = 3;
        public const int PuzzleChallengeGap = 0;
        #endregion

        #region default settings
        public const GuessType DefaultGuessType = GuessType.AllAtTime;
        public const int DefaultMirrorNumber = 4;
        public const int DefaultMaxRays = 8;
        public const bool DefaultUnlimitedRays = false;
        public const DifficultyType DefalutDifficulty = DifficultyType.Easy;
        public const bool DefaultSound = true;
        public const bool DefaultVibrate = true;
        #endregion
    }
}
