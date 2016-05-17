using System;
using RemoteExplorerServerInfrastructure;


namespace RemoteExplorerWindowServer
{
    public class RemoteExplorerWindowServerViewModel: SignalRServerViewModelBase<RemoteExplorerService>
    {
        #region Fields
        private string _state;
        #endregion


        #region  Constructors & Destructor
        public RemoteExplorerWindowServerViewModel(): base(new RemoteExplorerService()) { }
        #endregion


        #region  Properties & Indexers
        public string State
        {
            get { return _state; }
            private set { SetProperty(ref _state, value); }
        }
        #endregion


        #region Override
        public override void Log(string logContent)
            => State = string.IsNullOrEmpty(State) ? logContent : State + Environment.NewLine + logContent;
        #endregion
    }
}