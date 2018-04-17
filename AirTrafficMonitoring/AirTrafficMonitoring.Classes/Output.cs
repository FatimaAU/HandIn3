using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class Output : IOutput
    {
        public void Print(ITrackObject track)
        {
            Console.WriteLine("Tag:\t\t" + track.Tag);
            Console.WriteLine("X coordinate:\t" + track.XCoordinate + " meters");
            Console.WriteLine("Y coordinate:\t" + track.YCoordinate + " meters");
            Console.WriteLine("Altitide:\t" + track.Altitude + " meters");
            Console.WriteLine("Timestamp:\t" + track.TimeStamp);
            Console.WriteLine();

        }

        
    }
}
