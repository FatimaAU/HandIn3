using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes
{
    public class Track
    {
        //private IOutput myOutput;
        private string Tag;
        private string XCoordinate;
        private string YCoordinate;
        private string Altitude;
        private string TimeStamp;

        public Track(string tag, string x, string y, string al, string time)
        {
            Tag = strList[0];
            XCoordinate = strList[1];
            YCoordinate = strList[2];
            Altitude = strList[3];
            TimeStamp = strList[4];
        }
    }


}
