using System;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;


namespace RemoteExplorerServerInfrastructure
{
    public abstract class SignalRServiceBase
    {
        #region Fields
        protected IDisposable _service;
        #endregion


        #region  Constructors & Destructor
        protected SignalRServiceBase(string signalRUrl)
        {
            Url = signalRUrl;
        }
        #endregion


        #region  Properties & Indexers
        public virtual bool CanStart => State == SignalRServiceState.Disconnected;
        public virtual bool CanStop => State == SignalRServiceState.Connected;
        public virtual string Error { get; private set; }
        public virtual SignalRServiceState State { get; private set; } = SignalRServiceState.Disconnected;

        public string Url { get; }
        #endregion


        #region Methods
        public virtual async Task<bool> Start()
        {
            if (State != SignalRServiceState.Disconnected)
            {
                SetStateError();
                return false;
            }

            try
            {
                State = SignalRServiceState.Connecting;
                _service = await Task.Run(() => WebApp.Start(Url));
                State = SignalRServiceState.Connected;
                Error = null;
                return true;
            }
            catch (Exception exception)
            {
                State = SignalRServiceState.Disconnected;
                Error = exception.Message;
                return false;
            }
        }

        public virtual bool Stop()
        {
            if (State != SignalRServiceState.Connected)
            {
                SetStateError();
                return false;
            }

            _service.Dispose();
            State = SignalRServiceState.Disconnected;
            Error = null;
            return true;
        }
        #endregion


        #region Implementation
        protected virtual void SetStateError()
        {
            Error = $"Service is {State.ToString().ToLower()}";
        }
        #endregion
    }
}


// TODO: Use 192.168.x.x