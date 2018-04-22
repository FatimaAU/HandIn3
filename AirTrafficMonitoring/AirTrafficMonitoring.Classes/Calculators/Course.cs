using System;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Calculators
{
    public class Course : ICourse
    {
        public int CurrentCourse(IPosition oldTrack, IPosition newTrack, IDistance dist)
        {
            int x = dist.Point(oldTrack.XCoor, newTrack.XCoor);
            int y = dist.Point(oldTrack.YCoor, newTrack.YCoor);

            return (int) CalculateInDegrees(x,y);
        }

        public double CalculateInDegrees(int x, int y)
        {
            double angle = Math.Atan2(-y, x) * (180 / Math.PI) + 90;

            if (angle < 0)
                angle += 360;
            return Math.Round(angle, 2);
        }
    }
}
