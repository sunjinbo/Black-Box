
namespace Blackbox
{
    public class BoxData
    {
        #region data
        public int _state;
        public Position _position = new Position();

        public BoxData() { }
        public BoxData(int state, Position position)
        {
            _state = state;
            _position = position;
        }
        #endregion
    }
}
