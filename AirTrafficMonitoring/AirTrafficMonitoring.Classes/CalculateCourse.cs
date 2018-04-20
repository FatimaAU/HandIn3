using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class CalculateCourse : ICalculateCourse
    {
        public int Course(IPosition oldTrack, IPosition newTrack, IDistance dist)
        {
            int x = dist.Point(oldTrack.XCoor, newTrack.XCoor);
            int y = dist.Point(oldTrack.YCoor, newTrack.YCoor);

            //double inRad = CalculateInRadians(x, y);

            return (int) CalculateInDegrees(x,y);
        }

        //public int Length(int first, int second)
        //{
        //    return second - first;
        //}

        public double CalculateInDegrees(int x, int y)
        {
            double angle = Math.Atan2(-y, x) * (180 / Math.PI) + 90;

            if (angle < 0)
                angle += 360;
            return Math.Round(angle, 2);
        }
    }
}
