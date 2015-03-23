using System;

namespace Blackbox
{
    public enum ModelState
    {
        NoRayExplored,
        MaxRaysExceed,
        MaxMirrorsExceed,
        LightBoxActivated,
        BlackBoxActivated,
        GuessAdded,
        GuessRemoved,
        GuessCompleted,
        GuessTransferred,
        GuessSuccess,
        GuessFailed,
        RaysCreated,
        GameOver
    }

    public class ModelStateEventArgs : EventArgs
    {
        #region model state attributes
        public ModelState State { get; set;}
        public string Message { get; set; }
        #endregion

        public ModelStateEventArgs(ModelState state)
        {
            State = state;
        }

        public ModelStateEventArgs(ModelState state, string message)
            : this(state)
        {
            Message = message;
        }
    }
}
