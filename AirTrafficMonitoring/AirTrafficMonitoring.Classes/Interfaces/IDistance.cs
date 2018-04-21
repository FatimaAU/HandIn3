using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public interface IDistance
    {
        double DistanceTwoDim(IPosition newTrack, IPosition oldTrack);
        int DistanceOneDim(int first, int second);
        int Point(int first, int second);
    }
}