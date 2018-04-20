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
    }
}

