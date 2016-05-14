using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using RemoteExplorerClientInfrastructure;


namespace RemoteExplorerWindowClient
{
    public class SignalRClientViewModelBase<TSignalRProxy>: ViewModelBase where TSignalRProxy: SignalRProxyBase
    {
        #region Fields
        protected ICommand _connectAsyncCommand;
        private ICommand _disconnectCommand;
        protected readonly TSignalRProxy _proxy;
        #endregion


        #region  Constructors & Destructor
        public SignalRClientViewModelBase(TSignalRProxy proxy)
        {
            _proxy = proxy;
            _proxy.Error += Proxy_Error;
        }
        #endregion


        #region  Properties & Indexers
        public ICommand ConnectAsyncCommand
            => GetCommand(ref _connectAsyncCommand, async _ => await ConnectAsync() /*, _ => CanConnect*/);

        public ICommand DisconnectCommand
            => GetCommand(ref _disconnectCommand, _ => Disconnect() /*, _ => CanDisconnect*/);
        #endregion


        #region Methods
        public async Task ConnectAsync() => await _proxy.ConnectAsync();
        public void Disconnect() => _proxy.Disconnect();
        #endregion


        #region Event Handlers
        private void Proxy_Error(object sender, string e)
        {
            State = e;
        }
        #endregion
    }
}