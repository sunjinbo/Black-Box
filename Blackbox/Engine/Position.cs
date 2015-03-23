
namespace Blackbox
{
    public class Position
    {
        public int Row
        {
            get;
            set;
        }

        public int Column
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return (this.Row == ((Position)obj).Row && this.Column == ((Position)obj).Column);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Row, Column);
        }

        public Position() 
        { 
        }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position(Position pos)
        {
            Row = pos.Row;
            Column = pos.Column;
        }
    }
}
