using System;
using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.TrackListReadyEvent
{
    public class TrackListUpdatedArgs : EventArgs
    {
        public TrackListUpdatedArgs(List<TrackObject> tracks)
        {
            TrackList = tracks;
        }

        public List<TrackObject> TrackList { get; }
    }

    public interface ITrackList
    {
        event EventHandler<TrackListUpdatedArgs> TrackListReady;
    }
}
