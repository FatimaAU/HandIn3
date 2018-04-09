using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class TrackObject : ITrackObject
    {
        public string Tag { get; }
        public string XCoordinate { get; }
        public string YCoordinate { get; }
        public string Altitude { get; }
        public string TimeStamp { get; }

        public TrackObject(string tag, IPosition pos, string time)
        {
            Tag = tag;
            XCoordinate = pos.XCoor;
            YCoordinate = pos.YCoor;
            Altitude = pos.Altitude;
            TimeStamp = time;
        }
    }


}
