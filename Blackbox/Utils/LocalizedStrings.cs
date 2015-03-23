
namespace Blackbox
{
    public class LocalizedStrings
    {
        #region internal data
        private static Blackbox.AppResources localizedResources 
            = new Blackbox.AppResources();
        #endregion

        #region property
        public Blackbox.AppResources LocalizedResources
        {
            get
            {
                return localizedResources;
            }
        }
        #endregion
    }
}
