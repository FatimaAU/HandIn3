using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class TrackObject : ITrackObject
    {
        public string Tag { get; }
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public int Altitude { get; }
        public string Timestamp { get; }
        public int Course { get; }
        public int Velocity { get; set; }

        public TrackObject(string tag, IPosition pos, string time)
        {
            Tag = tag;
            XCoordinate = pos.XCoor;
            YCoordinate = pos.YCoor;
            Altitude = pos.Altitude;
            Timestamp = time;
            Course = 0;
            Velocity = 0;
        }

        public override string ToString()
        {
            return
                "Tag:\t\t" + Tag + "\n" +
                "X coordinate:\t" + XCoordinate + " meters \n" +
                "Y coordinate:\t" + YCoordinate + " meters\n" +
                "Altitide:\t" + Altitude + " meters\n" +
                "Timestamp:\t" + Timestamp + "\n" +
                "Velocity:\t" + Velocity + "\n" +
                "Course:\t\t" + Course + "\n";
        }
    }


}
