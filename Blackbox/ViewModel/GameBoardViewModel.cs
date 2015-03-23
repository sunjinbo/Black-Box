using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;

namespace Blackbox
{
    public class GameBoardViewModel : ViewModelBase, IBoxHelper
    {
        public event EventHandler<RaysEventArgs> RaysCreated;
        private Box[,] _boxes = 
            new Box[BlackboxConfig.GameBoardRow, BlackboxConfig.GameBoardColumn];
        private List<Rays> _raysList = new List<Rays>();
        private List<Mirror> _mirrorList = new List<Mirror>();
        private Random _random = new Random();
        private bool _gameover;
        private bool _layoutGuess;
        private int _raysIndex;

        public Box[,] Boxes
        {
            get
            {
                return _boxes;
            }
            private set
            {
                _boxes = value;
            }
        }

        public List<Mirror> Mirrors
        {
            get
            {
                return _mirrorList;
            }
            private set
            {
                _mirrorList = value;
            }
        }

        public GameBoardViewModel()
        {
            GenerateBoxes();
            List<Position> posList = new List<Position>();
            GenerateMirrorsLayout(ref posList);
            foreach (Position pos in posList)
            {
                _mirrorList.Add(new Mirror(pos));
            }

            _gameover = false;
            _layoutGuess = false;
        }

        public void DoGuess()
        {
            _layoutGuess = false;

            int mirrorNumber = App.Settings.MirrorNumber;
            int guessedMirrorNumber = 0;

            for (int i = 1; i < BlackboxConfig.GameBoardRow - 1; i++)
                for (int j = 1; j < BlackboxConfig.GameBoardColumn - 1; j++)
                {
                    Box box = _boxes[i, j];

                    if (App.Settings.GuessType == GuessType.AllAtTime)
                    {
                        if (box.State == BlackBox.GuessingState)
                        {
                            Mirror mirror = Mirror(i, j);
                            if (mirror != null)
                            {
                                _layoutGuess = true;
                                mirror.Guessed = true;
                                box.State = BlackBox.GuessedState;
                                UpdateState(ModelState.GuessSuccess);
                            }
                            else
                            {
                                _layoutGuess = false;
                                box.State = BlackBox.GuessFailedState;
                                UpdateState(ModelState.GuessFailed);
                            }
                            //break;
                        }

                        if (box.State == BlackBox.GuessedState)
                        {
                            ++guessedMirrorNumber;
                            if (guessedMirrorNumber == App.Settings.MirrorNumber)
                            {
                                _layoutGuess = true;
                                UpdateState(ModelState.GameOver);
                            }
                        }
                    }
                    else // all at once
                    {
                        if (box.State == BlackBox.GuessingState)
                        {
                            if (Mirror(i, j) != null)
                            {
                                --mirrorNumber;
                            }
                        }
                    }
                }

            if (App.Settings.GuessType == GuessType.AllAtOnce)
            {
                if (mirrorNumber == 0)
                {
                    foreach (Mirror mirror in _mirrorList)
                    {
                        mirror.Guessed = true;
                    }

                    UpdateState(ModelState.GameOver);
                }
                else
                {
                    int guessedNum = App.Settings.MirrorNumber - mirrorNumber;
                    UpdateState(ModelState.GuessFailed, guessedNum.ToString());

                    //RemoveAllGuess();
                }
            }
        }

        public void Debunk()
        {
            _gameover = true;

            foreach (Rays rays in _raysList)
            {
                rays.Visible = true;
            }

            foreach (Mirror mirror in _mirrorList)
            {
                mirror.FullMirror = true;
                mirror.Visible = true;
            }

            StartRaysEffect();
        }

        public void Restart()
        {
            _gameover = false;
            _layoutGuess = false;

            foreach (Box box in _boxes)
            {
                box.Reset();
            }

            foreach (Rays ray in _raysList)
            {
                ray.Stop();
            }

            _raysList.Clear();

            List<Position> posList = new List<Position>();
            GenerateMirrorsLayout(ref posList);
            
            for(int i = 0; i < posList.Count; i++)
            {
                _mirrorList[i].Position = posList[i];
                _mirrorList[i].Visible = false;
                _mirrorList[i].Guessed = false;
                _mirrorList[i].FullMirror = false;
            }
        }

        Box IBoxHelper.Box(int row, int column)
        {
            Box box = null;

            if ((row >= 0) &&
                (row < BlackboxConfig.GameBoardRow) &&
                (column >= 0) &&
                (column < BlackboxConfig.GameBoardColumn))
            {
                box = _boxes[row, column];
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            return box;
        }

        Rect IBoxHelper.BoxRect(int row, int column)
        {
            Rect rect = new Rect();
            rect.X = row * BlackboxConfig.BoxWidth;
            rect.Y = column * BlackboxConfig.BoxHeight;
            rect.Width = BlackboxConfig.BoxWidth;
            rect.Height = BlackboxConfig.BoxHeight;
            return rect;
        }

        bool IBoxHelper.HasMirror(int row, int column)
        {
            return (Mirror(row, column) != null);
        }

        int IBoxHelper.RaysCount()
        {
            return _raysList.Count;
        }

        int IBoxHelper.MaxRays()
        {
            return BlackboxConfig.DefaultMaxRays;
        }

        GuessType IBoxHelper.GuessType()
        {
            return App.Settings.GuessType;
        }

        int IBoxHelper.MirrorNumber()
        {
            return App.Settings.MirrorNumber;
        }

        bool IBoxHelper.UnlimitedRays()
        {
            return App.Settings.UnlimitedRays;
        }

        void IBoxHelper.RaysCreated(Rays rays)
        {
            rays.AnimationCompleted += new EventHandler<EventArgs>(rays_AnimationCompleted);
            _raysList.Add(rays);

            // Once a ray was created, 
            // Player can layout a new guess then on.
            _layoutGuess = true;

            if (RaysCreated != null)
            {
                RaysCreated(this, new RaysEventArgs(rays));
            }
            UpdateState(ModelState.RaysCreated);
        }

        bool IBoxHelper.LayoutGuess()
        {
            return _layoutGuess;
        }

        void rays_AnimationCompleted(object sender, EventArgs e)
        {
            ++_raysIndex;
            if (_raysIndex >= _raysList.Count)
            {
                _raysIndex = 0;
            }

            if (_raysList.Count > 0)
            {
                _raysList[_raysIndex].Start();
            }
        }

        public void OnTap(object sender, GestureEventArgs e)
        {
            Box box = (Box)((Image)sender).DataContext;
            if (!_gameover && box != null)
            {
                box.Trigger();
            }
        }

        private Mirror Mirror(int row, int column)
        {
            foreach (Mirror mirror in _mirrorList)
            {
                if (mirror.Position.Row == row &&
                    mirror.Position.Column == column)
                {
                    return mirror;
                }
            }
            return null;
        }

        private void RemoveAllGuess()
        {
            for (int i = 1; i < BlackboxConfig.GameBoardRow - 1; i++)
                for (int j = 1; j < BlackboxConfig.GameBoardColumn - 1; j++)
                {
                    Box box = _boxes[i, j];
                    if (box.State == BlackBox.GuessingState)
                    {
                        box.State = BlackBox.GuessNoneState;
                    }
                }
        }

        private void GenerateBoxes()
        {
            for (int row = 0; row < BlackboxConfig.GameBoardRow; row++)
            {
                for (int column = 0; column < BlackboxConfig.GameBoardColumn; column++)
                {
                    if ((row == 0) ||
                        (column == 0) ||
                        (row == BlackboxConfig.GameBoardRow - 1) ||
                        (column == BlackboxConfig.GameBoardColumn - 1))
                    {
                        if ((row > 0) && (row < BlackboxConfig.GameBoardRow - 1) ||
                            (column > 0) && (column < BlackboxConfig.GameBoardColumn - 1))
                        {
                            _boxes[row, column] = new LightBox(this, row, column);
                            _boxes[row, column].State = LightBox.ConcealState;
                        }
                        else
                        {
                            _boxes[row, column] = new NookBox(this, row, column);
                            _boxes[row, column].State = NookBox.NookState;
                        }
                        continue;
                    }
                    _boxes[row, column] = new BlackBox(this, row, column);
                    _boxes[row, column].State = BlackBox.GuessNoneState;
                }
            }
        }

        private void StartRaysEffect()
        {
            _raysIndex = 0;
            if (_raysList.Count > 0)
                _raysList[_raysIndex].Start();
        }

        private void GenerateMirrorsLayout(ref List<Position> newPosList)
        {
            int mirrorNumber = 0;
            int puzzleEasyGap = BlackboxConfig.PuzzleEasyGap;
            int count = 0;

            newPosList.Clear();
            while( App.Settings.MirrorNumber > mirrorNumber )
            {
                int x = _random.Next(1, BlackboxConfig.GameBoardRow - 1);
                int y = _random.Next(1, BlackboxConfig.GameBoardColumn - 1);

                bool repeat = false;

                for (int i = 0; i < newPosList.Count; i++)
                {
                    int xGap = Math.Abs(newPosList[i].Row - x);
                    int yGap = Math.Abs(newPosList[i].Column - y);
                    int pGap = puzzleEasyGap;

                    if ( App.Settings.Difficulty == DifficultyType.Challenge )
                    {
                        pGap = BlackboxConfig.PuzzleChallengeGap;
                    }

                    if ( xGap <= pGap && yGap <= pGap )
                    {
                        repeat = true;
                        break;
                    }
                 }

                if ( !repeat )
                {
                    newPosList.Add(new Position(x, y));
                    Debug.WriteLine(string.Format("{0},{1}", x, y));
                    ++mirrorNumber;
                }

                ++count;
                if (count >= (BlackboxConfig.GameBoardRow - 2) * (BlackboxConfig.GameBoardColumn - 2))
                {
                    --puzzleEasyGap;
                    count = 0;
                }
            }
        }
    }
}
