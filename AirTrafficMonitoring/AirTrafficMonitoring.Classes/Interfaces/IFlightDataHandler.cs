using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface IFlightDataHandler
    {
        void Distribute(List<string> data, out string tag);
    }
}
