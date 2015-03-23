using System;
using System.ComponentModel;

namespace Blackbox
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ModelStateEventArgs> ModelStateUpdated;

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #region update state
        protected void UpdateState(ModelState state)
        {
            if (ModelStateUpdated != null)
            {
                ModelStateUpdated(this, new ModelStateEventArgs(state));
            }
        }

        protected void UpdateState(ModelState state, string message)
        {
            if (ModelStateUpdated != null)
            {
                ModelStateUpdated(this, new ModelStateEventArgs(state, message));
            }
        }
        #endregion
    }
}
