using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using System;
using Blackbox.Engine;

namespace Blackbox
{
    public class LightSpot
    {
        public Point _point = new Point();
        public Visibility _visibility = Visibility.Collapsed;
    }

    public class Rays : ViewModelBase
    {
        public event EventHandler<EventArgs> AnimationCompleted;
        private LightBox _lightBox;
        private List<Line> _lineList = new List<Line>();
        private int _raysId;
        private bool _visible;
        private string _miniLanguage;
        private LightSpot _lightSpot;
        private DispatcherTimer timer;
        private int _animLineIndex;
        private int _animSpeed;
        private IRaysObserver _observer;

        public string MiniLanguage
        {
            get
            {
                return _miniLanguage;
            }
            set
            {
                if (_miniLanguage != (string)value)
                {
                    _miniLanguage = (string)value;
                    NotifyPropertyChanged("MiniLanguage");
                }
            }
        }

        public int RelatedState
        {
            get
            {
                return _lightBox.State;
            }
        }

        public List<Line> Lines
        {
            get
            {
                return _lineList;
            }
        }

        public int RaysId
        {
            get
            {
                return _raysId;
            }
            set
            {
                if (_raysId != (int)value)
                {
                    _raysId = (int)value;
                    NotifyPropertyChanged("RaysId");
                }
            }
        }

        public LightSpot LightSpot
        {
            get
            {
                return _lightSpot;
            }
        }

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (_visible != (bool)value)
                {
                    _visible = (bool)value;
                    NotifyPropertyChanged("Visible");
                }
            }
        }

        public void Start()
        {
            _animLineIndex = 0;
            _lightSpot._point = _lineList[_animLineIndex].From;
            _lightSpot._visibility = Visibility.Visible;
            this.timer.Start();

            if (_observer != null)
            {
                _observer.LightSpotUpdated(_lightSpot);
            }
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void SetObserver(IRaysObserver observer)
        {
            _observer = observer;
        }

        #region constructor
        public Rays(LightBox lightBox)
        {
            _visible = false;

            _lightBox = lightBox;

            BuildRays();

            _lightSpot = new LightSpot();

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(50);
            this.timer.Tick += new EventHandler(timer_Tick);
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (_lineList[_animLineIndex].Dir())
            {
                case Direction.DirTop:
                    _lightSpot._point.Y--;
                    break;
                case Direction.DirBottom:
                    _lightSpot._point.Y++;
                    break;
                case Direction.DirLeft:
                    _lightSpot._point.X--;
                    break;
                case Direction.DirRight:
                    _lightSpot._point.X++;
                    break;
                case Direction.DirTopLeft:
                    _lightSpot._point.X--;
                    _lightSpot._point.Y--;
                    break;
                case Direction.DirTopRight:
                    _lightSpot._point.X++;
                    _lightSpot._point.Y--;
                    break;
                case Direction.DirBottomLeft:
                    _lightSpot._point.X--;
                    _lightSpot._point.Y++;
                    break;
                case Direction.DirBottomRight:
                    _lightSpot._point.X++;
                    _lightSpot._point.Y++;
                    break;
                case Direction.DirCenter:
                    break;
                default:
                    break;
            }

            if ((int)_lightSpot._point.X == (int)_lineList[_animLineIndex].To.X
                && (int)_lightSpot._point.Y == (int)_lineList[_animLineIndex].To.Y)
            {
                _animLineIndex++;
                if (_animLineIndex < _lineList.Count)
                {
                    if (_animSpeed > 0)
                    {
                        --_animSpeed;
                        timer_Tick(null, EventArgs.Empty);
                    }
                    else
                    {
                        _animSpeed = 5;
                    }
                }
                else
                {
                    this.timer.Stop();
                    // notify rays'observer that anim is completed
                    _lightSpot._visibility = Visibility.Collapsed;
                    if (AnimationCompleted != null)
                    {
                        AnimationCompleted(this, EventArgs.Empty);
                    }
                }
            }
            else
            {
                if (_animSpeed > 0)
                {
                    --_animSpeed;
                    timer_Tick(null, EventArgs.Empty);
                }
                else
                {
                    _animSpeed = 5;
                }
            }


            NotifyPropertyChanged("LightSpot");
            if (_observer != null)
            {
                _observer.LightSpotUpdated(_lightSpot);
            }
        }

        private void BuildRays()
        {
            // Clears old lines
            _lineList.Clear();

            // Starts to build reays
            Direction direction;
            GetLightboxDirection(out direction);

            Position position = new Position(_lightBox.Position);
            Point start = SendOutPosition(direction, position);

            Line line = new Line();
            line.From = start;

            int state; // lightbox state

            while (true)
            {
                Forward(direction, ref position); // try to get the next position

                if (_lightBox.Helper.HasMirror(position.Row, position.Column))
                {
                    line.To = line.From;
                    _lineList.Add(new Line(line));
                    _lightBox.State = LightBox.ReflectionState;
                    break;
                }
                else if (NeighbourTest(direction, position))
                {
                    line.To = line.From;
                    _lineList.Add(new Line(line));
                    _lightBox.State = LightBox.ReflectionState;
                    break;
                }
                else if (FrontTest(direction, position))
                {
                    line.To = SendInPosition(direction, position); // ???
                    _lineList.Add(new Line(line));

                    if (_lineList.Count >= 2)
                    {
                        _lightBox.State = LightBox.ComplexReflectionState;
                    }
                    else
                    {
                        _lightBox.State = LightBox.ReflectionState;
                    }

                    break;
                }
                else if (SlopeTest(direction, position))
                {
                    line.To = SendInPosition(Direction.DirCenter, position);
                    _lineList.Add(new Line(line.From, line.To));

                    List<Position> array;
                    GetNearestMirror(out array, position);
                    if (array.Count > 1)
                    {
                        array.Clear();

                        if (_lineList.Count > 2)
                        {
                            _lightBox.State = LightBox.ComplexReflectionState;
                        }
                        else
                        {
                            _lightBox.State = LightBox.ReflectionState;
                        }

                        break;
                    }
                    else
                    {
                        direction = ReflectionDirection(line, array[0]);
                        array.Clear();
                        line.From = line.To;
                        continue;
                    }
                }
                else if (LightboxTest(position))
                {
                    line.To = SendInPosition(direction, position);
                    _lineList.Add(new Line(line));

                    if (_lineList.Count > 2)
                    {
                        state = LightBox.ComplexDeflectionState;
                    }
                    else
                    {
                        state = LightBox.DeflectionState;
                    }

                    if (position != _lightBox.Position)
                    {
                        _lightBox.State = state;
                        Box box = _lightBox.Helper.Box(position.Row, position.Column);
                        if (box!=null)
                        {
                            box.State = state + 2;
                            ((LightBox)box).Index = (_lightBox.Helper.RaysCount() + 1).ToString();
                            _lightBox.Index = (_lightBox.Helper.RaysCount() + 1).ToString();
                        }
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }

            BuildMiniLanguage();
        }

        private void BuildMiniLanguage()
        {
            string miniLanguage = string.Empty;
            bool IsStartPoint = true;
            foreach (Line line in _lineList)
            {
                if (IsStartPoint)
                {
                    miniLanguage = string.Format("M {0},{1} L {2},{3}", 
                        (int)line.From.X, (int)line.From.Y,
                        (int)line.To.X, (int)line.To.Y);

                    IsStartPoint = false;

                    continue;
                }
                else
                {
                    miniLanguage += string.Format(" L {0},{1}", (int)line.To.X, (int)line.To.Y);
                }
            }

            MiniLanguage = miniLanguage;
        }

        private void Forward( Direction dir, ref Position position )
        {
            switch ( dir )
                {
                case Direction.DirTop:
                    position.Column -= 1;
                    break;
                case Direction.DirBottom:
                    position.Column += 1;
                    break;
                case Direction.DirLeft:
                    position.Row -= 1;
                    break;
                case Direction.DirRight:
                    position.Row += 1;
                    break;
                case Direction.DirTopLeft:
                    position.Row -= 1;
                    position.Column -= 1;
                    break;
                case Direction.DirTopRight:
                    position.Row += 1;
                    position.Column -= 1;
                    break;
                case Direction.DirBottomLeft:
                    position.Row -= 1;
                    position.Column += 1;
                    break;
                case Direction.DirBottomRight:
                    position.Row += 1;
                    position.Column += 1;
                    break;
                case Direction.DirCenter:
                default:
                    break;
                }
        }

        bool SlopeTest( Direction dir, Position pos )
        {
            bool ret = false;

            switch (dir)
                {
                case Direction.DirTop:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column - 1 )
                        || _lightBox.Helper.HasMirror(pos.Row + 1, pos.Column - 1))
                        {
                            ret = true;
                        }
                    break;
                    }
                case Direction.DirBottom:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column + 1 )
                        || _lightBox.Helper.HasMirror( pos.Row + 1, pos.Column + 1 ) )
                        {
                            ret = true;
                        }
                    break;
                    }
                case Direction.DirLeft:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column - 1 )
                        || _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column + 1 ) )
                        {
                            ret = true;
                        }
                    break;
                    }
                case Direction.DirRight:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row + 1, pos.Column - 1 ) 
                        || _lightBox.Helper.HasMirror( pos.Row + 1, pos.Column + 1 ) )
                        {
                            ret = true;
                        }
                    break;
                    }
                case Direction.DirTopLeft:
                case Direction.DirTopRight:
                case Direction.DirBottomLeft:
                case Direction.DirBottomRight:
                case Direction.DirCenter: 
                default:
                    break;
                }
            return ret;
        }

        bool FrontTest( Direction dir, Position pos )
        {
            bool ret = false;
            switch ( dir )
                {
                case Direction.DirTop:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row, pos.Column - 1 ) )
                        {
                        ret = true;
                        }
                    break;
                    }
                case Direction.DirBottom:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row, pos.Column + 1 ) )
                        {
                        ret = true;
                        }
                    break;
                    }
                case Direction.DirLeft:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column ) )
                        {
                        ret = true;
                        }
                    break;
                    }
                case Direction.DirRight:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row + 1, pos.Column ) )
                        {
                        ret = true;
                        }
                    break;
                    }
                case Direction.DirTopLeft:
                case Direction.DirTopRight:
                case Direction.DirBottomLeft:
                case Direction.DirBottomRight:
                case Direction.DirCenter: 
                default:
                    break;
                }
            return ret;
        }

        bool NeighbourTest( Direction dir, Position pos )
        {
            bool ret = false;
            switch ( dir )
                {
                case Direction.DirTop:
                case Direction.DirBottom:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row - 1, pos.Column )
                        || _lightBox.Helper.HasMirror( pos.Row + 1, pos.Column ) )
                        {
                        ret = true;
                        }
                    break;
                    }

                case Direction.DirLeft:
                case Direction.DirRight:
                    {
                    if ( _lightBox.Helper.HasMirror( pos.Row, pos.Column + 1 )
                        || _lightBox.Helper.HasMirror( pos.Row, pos.Column - 1 ) )
                        {
                        ret = true;
                        }
                    break;
                    }

                case Direction.DirTopLeft:
                case Direction.DirTopRight:
                case Direction.DirBottomLeft:
                case Direction.DirBottomRight:
                case Direction.DirCenter: 
                default:
                    break;
                }
            return ret;
        }

        bool LightboxTest( Position pos )
        {
            if ( pos.Row == 0 
                || pos.Column == 0
                || pos.Row == BlackboxConfig.GameBoardRow - 1 
                || pos.Column == BlackboxConfig.GameBoardColumn - 1 )
                {
                return true;
                }
            return false;
        }

        void GetLightboxDirection( out Direction dir )
        {
            if ( _lightBox.Position.Row == 0 )
                {
                dir = Direction.DirRight;
                }
            else if ( _lightBox.Position.Row == BlackboxConfig.GameBoardRow - 1 )
                {
                dir = Direction.DirLeft;
                }
            else if ( _lightBox.Position.Column == 0 )
                {
                dir = Direction.DirBottom;
                }
            else if ( _lightBox.Position.Column == BlackboxConfig.GameBoardColumn - 1 )
                {
                dir = Direction.DirTop;
                }
            else
                {
                dir = Direction.DirCenter;
                }
        }

        void GetNearestMirror( out List<Position> mirrorList, Position pos )
        {
            mirrorList = new List<Position>();
            for ( int i = pos.Row - 1; i <= pos.Row + 1; i++ )
                for ( int j = pos.Column - 1; j <= pos.Column + 1; j++ )
                    {
                    if ( _lightBox.Helper.HasMirror( i, j ) )
                        {
                        mirrorList.Add( new Position( i, j ) );
                        }
                    }
        }

        Point SendOutPosition( Direction dir, Position position )
        {
            Point pos = new Point();
            Rect rect = _lightBox.Helper.BoxRect(position.Row, position.Column);
            switch (dir)
            {
                case Direction.DirTop:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y;
                    break;
                case Direction.DirBottom:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y + rect.Height;
                    break;
                case Direction.DirLeft:
                    pos.X = rect.X;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;
                case Direction.DirRight:
                    pos.X = rect.X + rect.Width;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;
                case Direction.DirCenter:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;

                case Direction.DirTopLeft:
                case Direction.DirTopRight:
                case Direction.DirBottomLeft:
                case Direction.DirBottomRight:
                default:
                    break;
            }
            return pos;
        }

        Point SendInPosition(Direction dir, Position position)
        {
            Point pos = new Point();
            Rect rect = _lightBox.Helper.BoxRect(position.Row, position.Column);
            switch (dir)
            {
                case Direction.DirBottom:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y;
                    break;
                case Direction.DirTop:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y + rect.Height;
                    break;
                case Direction.DirRight:
                    pos.X = rect.X;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;
                case Direction.DirLeft:
                    pos.X = rect.X + rect.Width;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;
                case Direction.DirCenter:
                    pos.X = rect.X + rect.Width / 2;
                    pos.Y = rect.Y + rect.Height / 2;
                    break;

                case Direction.DirTopLeft:
                case Direction.DirTopRight:
                case Direction.DirBottomLeft:
                case Direction.DirBottomRight:
                default:
                    break;
            }
            return pos;
        }

        Direction ReflectionDirection( Line line, Position mirrorPos )
        {
            Direction direction = line.Dir();
            Rect rect = _lightBox.Helper.BoxRect(mirrorPos.Row, mirrorPos.Column);
            Point mirrorPoint = new Point();
            mirrorPoint.X = rect.X + rect.Width / 2;
            mirrorPoint.Y = rect.Y + rect.Height / 2;

            if (line.To.X < mirrorPoint.X && line.To.Y > mirrorPoint.Y) // top right
            {
                if (direction == Direction.DirRight)
                {
                    direction = Direction.DirBottom;
                }
                if (direction == Direction.DirTop)
                {
                    direction = Direction.DirLeft;
                }
            }
            else if (line.To.X < mirrorPoint.X && line.To.Y < mirrorPoint.Y) // bottom right
            {
                if (direction == Direction.DirRight)
                {
                    direction = Direction.DirTop;
                }
                if (direction == Direction.DirBottom)
                {
                    direction = Direction.DirLeft;
                }
            }
            else if (line.To.X > mirrorPoint.X && line.To.Y > mirrorPoint.Y) // top left
            {
                if (direction == Direction.DirLeft)
                {
                    direction = Direction.DirBottom;
                }
                if (direction == Direction.DirTop)
                {
                    direction = Direction.DirRight;
                }
            }
            else if (line.To.X > mirrorPoint.X && line.To.Y < mirrorPoint.Y) // bottom left
            {
                if (direction == Direction.DirLeft)
                {
                    direction = Direction.DirTop;
                }
                if (direction == Direction.DirBottom)
                {
                    direction = Direction.DirRight;
                }
            }
            else
            {
                direction = Direction.DirCenter;
            }
            return direction;
        }
    }
}
