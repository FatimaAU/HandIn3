using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using TransponderReceiver;

namespace AirTrafficMonitoring.Classes
{
    public class TrackObjectifier : ITrackList
    {

        // ------ UDVIND DENNE KLASSE NÅR DET HELE ER OPDELT KORREKT.DENNE SKAL JO IKKE HAVE ALT TILDELT SOM MAIN HAR LIGE NU ------

        private static IMonitoredArea _monitoredArea;
        private static IParseTrackInfo _parser;
        private static IPosition _position;
        private static ITimestampFormatter _timestampFormatter;
        private static IFlightDataHandler _flightHandler;

        private List<ITrackObject> TrackList = new List<ITrackObject>();

        public event EventHandler<TrackListUpdatedArgs> TrackListReady;

        public TrackObjectifier(
            ITransponderReceiver rec, 
            IMonitoredArea monitoredArea, 
            IParseTrackInfo parser, 
            IPosition pos,
            ITimestampFormatter formatter, 
            IFlightDataHandler flightHandler)
        {
            rec.TransponderDataReady += CreateTrack;

            _monitoredArea = monitoredArea;
            _parser = parser;
            _position = pos;
            _timestampFormatter = formatter;
            _flightHandler = flightHandler;
        }

        private void CreateTrack(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            TrackList.Clear();
            //Traverse all elements
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                // Distribute data to relevant classes
                _flightHandler.Distribute(_parser.Parse(data), out var tag);

                // If inside the monitored area
                if (_monitoredArea.InsideMonitoredArea(_position))
                {
                    // Format and return the date
                    _timestampFormatter.FormatTimestamp();

                    TrackList.Add(new TrackObject(tag, _position, _timestampFormatter.InPretty));
                }
            }

            if (TrackList.Count != 0)
            {
                OnTrackListReady(new TrackListUpdatedArgs(TrackList));
            }
        }

        protected virtual void OnTrackListReady(TrackListUpdatedArgs e)
        {
            var handler = TrackListReady;
            handler?.Invoke(this, e);
        }
    }
}
