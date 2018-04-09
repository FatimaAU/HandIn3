using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes
{
    public class Track
    {
        //private IOutput myOutput;
        public string Tag { get; }
        public string XCoordinate { get; }
        public string YCoordinate { get; }
        public string Altitude { get; }
        public string TimeStamp { get; }

        public Track(string tag, ExtractPosition pos, string time)
        {
            Tag = tag;
            XCoordinate = pos.XCoor;
            YCoordinate = pos.YCoor;
            Altitude = pos.Altitude;
            TimeStamp = time;
        }
    }


}
