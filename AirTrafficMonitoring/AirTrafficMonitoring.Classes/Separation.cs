using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class Separation
    {
        int horizontalConflict = 5000;
        int verticalConflict = 300;

        public bool IsConflicting(ITrackObject trackOne, ITrackObject trackTwo, IDistance distance)
        {
            trackOne.Position.SetPosition(trackOne.Position.XCoor, trackOne.Position.YCoor, trackOne.Position.Altitude);
            trackTwo.Position.SetPosition(trackTwo.Position.XCoor, trackTwo.Position.YCoor, trackTwo.Position.Altitude);

            double horizontalDistance = distance.DistanceTwoDim(trackTwo.Position, trackOne.Position);
            int verticalDistance = distance.DistanceOneDim(trackTwo.Position.Altitude, trackOne.Position.Altitude);

            return horizontalDistance <= horizontalConflict && verticalDistance <= verticalConflict;
        }
    }
}
