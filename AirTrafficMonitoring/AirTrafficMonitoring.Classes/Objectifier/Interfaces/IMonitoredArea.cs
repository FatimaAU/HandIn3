namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface IMonitoredArea
    {
        bool InsideMonitoredArea(IPosition position);
    }
}
