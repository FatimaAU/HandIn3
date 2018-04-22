using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces
{
    public interface ISeparation
    {
        bool IsConflicting(ITrackObject trackOne, ITrackObject trackTwo, IDistance distance);
    }
}
