using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Calculators.Interfaces
{
    public interface IVelocity
    {
        int CurrentVelocity(ITrackObject newTrack, ITrackObject oldTrack, IDistance dist);
    }
}
