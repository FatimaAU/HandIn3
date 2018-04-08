using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes
{
    public class Track
    {
        //private IOutput myOutput;
        public string Tag;
        public string XCoordinate;
        public string YCoordinate;
        public string Altitude;
        public string TimeStamp;

        public Track(List<string> strList)
        {
            Tag = strList[0];
            XCoordinate = strList[1];
            YCoordinate = strList[2];
            Altitude = strList[3];
            TimeStamp = strList[4];
        }
    }


}
