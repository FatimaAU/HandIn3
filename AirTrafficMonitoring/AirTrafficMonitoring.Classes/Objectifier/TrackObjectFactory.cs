using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class TrackObjectFactory
    {
        ITrackObject CreateTrackObject(string tag, IPosition pos, string time, DateTime inDateTime)
        {
            return new TrackObject(tag, pos, time, inDateTime);
        }
    }
}
