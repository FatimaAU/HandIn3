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
        public double Velocity(ITrackObject newTrack, ITrackObject oldTrack, IDistance dist)
        {
            TimeSpan diff = newTrack.InDateTime - oldTrack.InDateTime;

            if ((int) diff.TotalMilliseconds == 0)
            {
                return 0;
            }

            return Math.Round(
                dist.DistanceTwoDim(newTrack.Position, oldTrack.Position)
                / diff.TotalMilliseconds * 1000, 2);
        }

        //public double DistanceTwoDim(IPosition newTrack, IPosition oldTrack)
        //{
        //    Int64 x = DistanceOneDim(oldTrack.XCoor, newTrack.XCoor);
        //    Int64 y = DistanceOneDim(oldTrack.YCoor, newTrack.YCoor);

        //    return Math.Round(
        //        Math.Sqrt((x * x) + (y * y)), 2);
        //}

        //public int DistanceOneDim(int first, int second)
        //{
        //    return Math.Abs(second - first);
        //}
    }
}

