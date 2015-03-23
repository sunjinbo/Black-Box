using System;

namespace Blackbox
{
    public abstract class Box : ViewModelBase, ITriggerBehavior, IComparable, IComparable<Box>
    {
        private BoxData _boxData = new BoxData();

        #region box attributes
        public IBoxHelper Helper
        {
            get;
            private set;
        }

        public BoxData BaseData
        {
            get
            {
                return _boxData;
            }
            private set
            {
                BoxData newData = (BoxData)value;
                if (newData != _boxData)
                {
                    _boxData = newData;
                    NotifyPropertyChanged("BaseData");
                }
            }
        }

        public Position Position
        {
            get
            {
                return BaseData._position;
            }
            private set
            {
                Position newPosition = (Position)value;
                if (newPosition != _boxData._position)
                {
                    BaseData = new BoxData(BaseData._state, newPosition);
                    NotifyPropertyChanged("Position");
                }
            }
        }

        public int State
        {
            get
            {
                return BaseData._state;
            }
            set
            {
                int newState = (int)value;
                if (newState != _boxData._state)
                {
                    BaseData = new BoxData(newState, BaseData._position);
                    NotifyPropertyChanged("State");
                }
            }
        }
        #endregion

        #region ITriggerBehavior implementation
        public virtual void Trigger()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IComparable implementation
        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual int CompareTo(Box other)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region reset attributes
        public abstract void Reset();
        #endregion

        #region constructor
        public Box(IBoxHelper helper, Position pos)
        {
            Helper = helper;
            Position = new Position(pos);
        }

        public Box(IBoxHelper helper, int row, int column)
        {
            Helper = helper;
            Position = new Position(row, column);
        }
        #endregion
    }
}
