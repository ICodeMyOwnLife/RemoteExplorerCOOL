using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using RemoteExplorerServerInfrastructure;


namespace RemoteExplorerWindowServer
{
    public abstract class SignalRServerViewModelBase<TSignalRService>: ViewModelBase
        where TSignalRService: SignalRServiceBase
    {
        #region Fields
        protected readonly TSignalRService _service;
        private ICommand _startServiceAsyncCommand;
        private ICommand _stopServiceCommand;
        #endregion


        #region  Constructors & Destructor
        protected SignalRServerViewModelBase(TSignalRService service)
        {
            _service = service;
        }
        #endregion


        #region  Properties & Indexers
        public bool CanStartService => _service.CanStart;
        public bool CanStopService => _service.CanStop;

        public ICommand StartServiceAsyncCommand
            => GetCommand(ref _startServiceAsyncCommand, async _ => await StartServiceAsync(), _ => CanStartService);

        public ICommand StopServiceCommand
            => GetCommand(ref _stopServiceCommand, _ => StopService(), _ => CanStopService);
        #endregion


        #region Methods
        public async Task StartServiceAsync()
        {
            State = "Connecting...";
            State = await _service.Start() ? $"Connected to {_service.Url}" : _service.Error;
        }

        public void StopService()
            => State = _service.Stop() ? "Disconnected" : _service.Error;
        #endregion
    }

    public class RemoteExplorerWindowServerViewModel: SignalRServerViewModelBase<RemoteExplorerService>
    {
        #region  Constructors & Destructor
        public RemoteExplorerWindowServerViewModel(): base(new RemoteExplorerService()) { }
        #endregion
    }
}