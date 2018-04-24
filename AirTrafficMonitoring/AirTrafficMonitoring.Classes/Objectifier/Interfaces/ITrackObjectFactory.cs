using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface ITrackObjectFactory
    {
        ITrackObject CreateTrackObject(string tag, IPosition pos, string time, DateTime inDateTime);
    }
}
