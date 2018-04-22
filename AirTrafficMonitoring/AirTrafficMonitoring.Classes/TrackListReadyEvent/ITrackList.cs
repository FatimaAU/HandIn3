using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.TrackListReadyEvent
{
    public class TrackListUpdatedArgs : EventArgs
    {
        public TrackListUpdatedArgs(List<ITrackObject> tracks)
        {
            TrackList = tracks;
        }

        public List<ITrackObject> TrackList { get; }
    }

    public interface ITrackList
    {
        event EventHandler<TrackListUpdatedArgs> TrackListReady;
    }
}
