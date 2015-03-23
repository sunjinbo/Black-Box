
namespace Blackbox
{
    public class LightBox : Box
    {
        #region light-box state definition
        public const int ConcealState = 10;
        public const int DeflectionState = 11;
        public const int ComplexDeflectionState = 12;
        public const int DeflectionOutState = 13;
        public const int ComplexDeflectionOutState = 14;
        public const int ReflectionState = 15;
        public const int ComplexReflectionState = 16;
        #endregion

        private Rays _rays;
        private string _index = string.Empty;

        public string Index
        {
            get
            {
                return _index;
            }
            set
            {
                string newIndex = (string)value;
                if (newIndex != _index)
                {
                    _index = newIndex;
                    NotifyPropertyChanged("Index");
                }
            }
        }

        #region constructor
        public LightBox(IBoxHelper helper, Position pos)
            : base(helper, pos)
        {
        }

        public LightBox(IBoxHelper helper, int row, int column)
            : base(helper, row, column)
        {
        }
        #endregion

        public override void Reset()
        {
            State = ConcealState;
            Index = string.Empty;
            _rays = null;
        }

        public override void Trigger()
        {
            if ( State == ConcealState )
            {
                if ( !Helper.UnlimitedRays() )
                {
                    if ( Helper.RaysCount() >= Helper.MaxRays() )
                        {
                        UpdateState(ModelState.MaxRaysExceed);
                        return;
                        }
                }

                if ( _rays == null )
                {
                    UpdateState(ModelState.LightBoxActivated);

                    _rays = new Rays(this);
                    Helper.RaysCreated(_rays);
                    Index = Helper.RaysCount().ToString();
                }
            }
        }
    }
}
