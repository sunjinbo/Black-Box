
namespace Blackbox
{
    public class NookBox : Box
    {
        #region black-box state definition
        public const int NookState = -1;
        #endregion

        #region constructor
        public NookBox(IBoxHelper helper, Position pos)
            : base(helper, pos)
        {
        }

        public NookBox(IBoxHelper helper, int row, int column)
            : base(helper, row, column)
        {
        }
        #endregion

        public override void Reset()
        {
            State = NookState;
        }

        public override void Trigger()
        {
            // no need to implementation required
        }
    }
}
