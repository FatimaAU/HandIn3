using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface IFlightDataHandler
    {
        void Distribute(List<string> data, out string tag, ref IPosition pos, ref ITimestampFormatter formatter);
    }
}
