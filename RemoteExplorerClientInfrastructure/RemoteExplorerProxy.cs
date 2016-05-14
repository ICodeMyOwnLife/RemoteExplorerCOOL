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
        public virtual string Error { get; set; }

        public string SignalRUrl { get; }
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
                Error = exception.Message;
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
            _hubProxy = _hubConnection.CreateHubProxy(RemoteExplorerConfig.GetHubName());
        }
        #endregion
    }

    public class RemoteExplorerProxy: SignalRProxyBase
    {
        #region  Constructors & Destructor
        public RemoteExplorerProxy(): base(RemoteExplorerConfig.GetSignalRUrl()) { }
        #endregion


        #region  Properties & Indexers
        public string[] Entries { get; set; }
        #endregion


        #region Methods
        public async Task EnumerateFolder(string folderPath)
        {
            await _hubProxy.Invoke("EnumerateFolder", folderPath);
        }
        #endregion


        #region Override
        protected override void InitializeProxy()
        {
            base.InitializeProxy();
            _hubProxy.On<string[]>("enumerateFolder", entries => Entries = entries);
        }
        #endregion
    }
}