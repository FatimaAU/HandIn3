namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface ICalculateVelocity
    {
        double Velocity(ITrackObject newTrack, ITrackObject oldTrack, IDistance dist);
    }
}
