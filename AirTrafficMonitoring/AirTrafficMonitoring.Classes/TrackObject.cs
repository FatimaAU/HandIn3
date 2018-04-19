using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class TrackObject : ITrackObject
    {
        public string Tag { get; }
        public IPosition Position { get; set; }
        public string Timestamp { get; }
        public int Course { get; set; }
        public int Velocity { get; set; }
        public DateTime InDateTime { get; set; }

        public TrackObject(string tag, IPosition pos, string time, DateTime inDateTime)
        {
            Tag = tag;
            Position = pos;
            Timestamp = time;
            InDateTime = inDateTime;
            Course = 0;
            Velocity = 0;
        }

        public override string ToString()
        {
            return
                "Tag:\t\t" + Tag + "\n" +
                "X coordinate:\t" + Position.XCoor + " meters \n" +
                "Y coordinate:\t" + Position.YCoor + " meters\n" +
                "Altitide:\t" + Position.Altitude + " meters\n" +
                "Timestamp:\t" + Timestamp + "\n" +
                "Velocity:\t" + Velocity + "\n" +
                "Course:\t\t" + Course + "\n";
        }
    }


}
