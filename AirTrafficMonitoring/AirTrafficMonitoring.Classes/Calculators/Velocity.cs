using System;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Calculators
{
    public class Velocity : IVelocity
    {
        public int CurrentVelocity(ITrackObject newTrack, ITrackObject oldTrack, IDistance dist)
        {
            TimeSpan diff = newTrack.InDateTime - oldTrack.InDateTime;

            if ((int) diff.TotalMilliseconds == 0)
            {
                return 0;
            }

            return (int) (dist.DistanceTwoDim(newTrack.Position, oldTrack.Position)
                          / diff.TotalMilliseconds * 1000);
        }
    }
}

