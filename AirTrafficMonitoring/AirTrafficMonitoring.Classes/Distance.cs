using System;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class Distance : IDistance
    {
        public int Point(int first, int second)
        {
            return second - first;
        }

        public int DistanceOneDim(int first, int second)
        {
            return Math.Abs(second - first);
        }

        public double DistanceTwoDim(IPosition newTrack, IPosition oldTrack)
        {
            Int64 x = DistanceOneDim(oldTrack.XCoor, newTrack.XCoor);
            Int64 y = DistanceOneDim(oldTrack.YCoor, newTrack.YCoor);

            return Math.Round(
                Math.Sqrt((x * x) + (y * y)), 2);
        }

        //public double DistanceThreeDim(IPosition newTrack, IPosition oldTrack)
        //{
        //    Int64 x = DistanceOneDim(oldTrack.XCoor, newTrack.XCoor);
        //    Int64 y = DistanceOneDim(oldTrack.YCoor, newTrack.YCoor);
        //    Int64 z = DistanceOneDim(oldTrack.Altitude, newTrack.Altitude);

        //    return Math.Round(
        //        Math.Sqrt((x * x) + (y * y)), 2);
        //}



    }
}