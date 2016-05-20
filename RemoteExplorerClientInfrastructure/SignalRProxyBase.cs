using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerClientInfrastructure
{
    public abstract class SignalRProxyBase
    {
        #region Fields
        protected HubConnection _hubConnection;
        protected IHubProxy _hubProxy;
        #endregion


        #region  Constructors & Destructor
        protected SignalRProxyBase(string signalRUrl)
        {
            SignalRUrl = signalRUrl;
        }
        #endregion


        #region  Properties & Indexers
        public string SignalRUrl { get; }
        #endregion


        #region Events
        public event EventHandler<string> Error;
        #endregion


        #region Methods
        public virtual async Task ConnectAsync()
        {
            InitializeProxy();
            try
            {
                await _hubConnection.Start();
            }
            catch (Exception exception)
            {
                OnError(exception.Message);
            }
        }

        public virtual void Disconnect()
        {
            _hubConnection.Stop();
        }
        #endregion


        #region Implementation
        protected virtual void InitializeProxy()
        {
            _hubConnection = new HubConnection(SignalRUrl);
            _hubConnection.Error += exception => OnError(exception.Message);
            _hubProxy = _hubConnection.CreateHubProxy(RemoteExplorerConfig.GetHubName());
            _hubProxy.On<string>("error", OnError);
        }

        protected virtual void OnError(string e)
        {
            Error?.Invoke(this, e);
        }
        #endregion
    }
}


// TODO: CanConnect, CanDisconnect