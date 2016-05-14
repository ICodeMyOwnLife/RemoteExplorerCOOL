using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using RemoteExplorerInfrastructure;


namespace RemoteExplorerClientInfrastructure
{
    public class RemoteExplorerConnection
    {
        #region Fields
        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;
        #endregion


        #region  Properties & Indexers
        public string[] Entries { get; set; }
        public string Error { get; set; }
        #endregion


        #region Methods
        public async Task Connect()
        {
            InitializeConnect();
            try
            {
                await _hubConnection.Start();
            }
            catch (Exception exception)
            {
                Error = exception.Message;
            }
        }

        public async Task EnumerateFolder(string folderPath)
        {
            await _hubProxy.Invoke("EnumerateFolder", folderPath);
        }
        #endregion


        #region Implementation
        private void InitializeConnect()
        {
            _hubConnection = new HubConnection(RemoteExplorerConfig.GetSignalRUrl());

            _hubProxy = _hubConnection.CreateHubProxy(RemoteExplorerConfig.GetHubName());
            _hubProxy.On<string[]>("enumerateFolder", entries => Entries = entries);
        }
        #endregion
    }
}