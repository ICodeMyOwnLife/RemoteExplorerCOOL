using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using CB.Model.Prism;
using Microsoft.Practices.Prism.Commands;
using RemoteExplorerServerInfrastructure;


namespace RemoteExplorerWindowServer
{
    public abstract class SignalRServerViewModelBase<TSignalRService>: PrismViewModelBase, ILog
        where TSignalRService: SignalRServiceBase
    {
        #region Fields
        protected readonly TSignalRService _service;
        #endregion


        #region  Constructors & Destructor
        protected SignalRServerViewModelBase(TSignalRService service)
        {
            _service = service;
            StartServiceAsyncCommand = DelegateCommand.FromAsyncHandler(StartServiceAsync, () => CanStartService);
            StopServiceCommand = new DelegateCommand(StopService, () => CanStopService);
        }
        #endregion


        #region Abstract
        public abstract void Log(string logContent);
        #endregion


        #region  Properties & Indexers
        public bool CanStartService => _service.CanStart;
        public bool CanStopService => _service.CanStop;

        public ICommand StartServiceAsyncCommand { get; }

        public ICommand StopServiceCommand { get; }
        #endregion


        #region Methods
        public async Task StartServiceAsync()
        {
            Log("Connecting...");
            Log(await _service.Start() ? $"Connected to {_service.Url}" : _service.Error);
        }

        public void StopService()
            => Log(_service.Stop() ? "Disconnected" : _service.Error);
        #endregion
    }
}