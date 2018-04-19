namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface ICalculateVelocity
    {
        int Velocity(ITrackObject newTrack, ITrackObject oldTrack);
    }
}
