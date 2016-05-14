using RemoteExplorerInfrastructure;


namespace RemoteExplorerServerInfrastructure
{
    public class RemoteExplorerService: SignalRServiceBase
    {
        #region  Constructors & Destructor
        public RemoteExplorerService(): base(RemoteExplorerConfig.GetSignalRUrl()) { }
        #endregion
    }
}