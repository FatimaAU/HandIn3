using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Calculators.Interfaces
{
    public interface IDistance
    {
        double DistanceTwoDim(IPosition newTrack, IPosition oldTrack);
        int DistanceOneDim(int first, int second);
        int Point(int first, int second);
    }
}