using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Calculators.Interfaces
{
    public interface ICourse
    {
        int CurrentCourse(IPosition oldTrack, IPosition newTrack, IDistance dist);
    }
}