using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class CalculateVelocity : ICalculateVelocity
    {
        public int Velocity(ITrackObject newTrack, ITrackObject oldTrack)
        {
            TimeSpan diff = newTrack.InDateTime - oldTrack.InDateTime;

            if (diff.Seconds == 0)
            {
                return 0;
            }

            return 0;
            // return Distance(newTrack.Position, oldTrack) * 1000 / diff.Milliseconds ;
        }

        public int Distance(IPosition newTrack, IPosition oldTrack)
        {
            int x = Length(oldTrack.XCoor, newTrack.XCoor);
            int y = Length(oldTrack.YCoor, newTrack.YCoor);

            return (int)Math.Sqrt((x * x) + (y * y));
        }

        public int Length(int first, int second)
        {
            return Math.Abs(Math.Abs(second) - Math.Abs(first));
        }
    }
}
