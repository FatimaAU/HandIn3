using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface IFlightExtractor
    {
        string Tag { get; set; }
        IPosition Position { get; set; }
        int Altitude { get; set; }
        string RawTimestamp { get; set; }
        void Extract(List<string> data);
    }
}
