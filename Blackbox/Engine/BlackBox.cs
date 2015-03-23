
namespace Blackbox
{
    public class BlackBox : Box
    {
        #region black-box state definition
        public const int GuessNoneState = 0;
        public const int GuessingState = 1;
        public const int GuessFailedState = 2;
        public const int GuessedState = 3;
        #endregion

        #region constructor
        public BlackBox(IBoxHelper helper, Position pos)
            : base(helper, pos)
        {
        }

        public BlackBox(IBoxHelper helper, int row, int column)
            : base(helper, row, column)
        {
        }
        #endregion

        public override void Reset()
        {
            State = GuessNoneState;
        }

        public override void Trigger()
        {
            if (!Helper.LayoutGuess())
            {
                UpdateState(ModelState.NoRayExplored);
                return;
            }

            if (State == GuessNoneState || State == GuessingState)
            {
                if (State == GuessingState)
                {
                    State = GuessNoneState;
                    UpdateState(ModelState.GuessRemoved);
                }
                else
                {
                    if (Helper.GuessType() == GuessType.AllAtTime)
                    {
                        int x = 0, y = 0;
                        for (int i = 1; i < BlackboxConfig.GameBoardRow - 1; i++)
                            for (int j = 1; j < BlackboxConfig.GameBoardColumn - 1; j++)
                            {
                                Box box = Helper.Box(i, j);
                                if (box != null && box.State == GuessingState)
                                {
                                    x = i;
                                    y = j;
                                    break;
                                }
                            }

                        if (x != 0 && y != 0)
                        {
                            Helper.Box(x, y).State = GuessNoneState;
                            UpdateState(ModelState.GuessTransferred);
                        }
                        else
                        {
                            UpdateState(ModelState.GuessAdded);
                            UpdateState(ModelState.GuessCompleted);
                        }
                        State = GuessingState;
                    }
                    else // all at once
                    {
                        int mirrorNumber = 0;
                        for (int i = 1; i < BlackboxConfig.GameBoardRow - 1; i++)
                            for (int j = 1; j < BlackboxConfig.GameBoardColumn - 1; j++)
                            {
                                Box box = Helper.Box(i, j);
                                if (box != null && box.State == GuessingState)
                                {
                                    ++mirrorNumber;
                                }
                            }

                        if (Helper.MirrorNumber() > mirrorNumber)
                        {
                            State = GuessingState;

                            ++mirrorNumber;
                            if (Helper.MirrorNumber() == mirrorNumber)
                            {
                                UpdateState(ModelState.GuessCompleted);
                            }

                            UpdateState(ModelState.GuessAdded);
                        }
                        else
                        {
                            UpdateState(ModelState.MaxMirrorsExceed);
                        }
                    }
                }
            }
        }
    }
}
