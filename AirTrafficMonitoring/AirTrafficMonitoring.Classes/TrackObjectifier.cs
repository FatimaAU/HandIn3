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
        private static IParseTrackInfo _parseTrack;
        private static IPosition _position;
        private static ITimestamp _timestamp;
        private static ITimestampFormatter _timestampFormatter;
        private static IFlightDataExtractor _extractedFlight;

        private List<TrackObject> TrackList = new List<TrackObject>();

        public event EventHandler<TrackListUpdatedArgs> TrackListReady;

        public void CreateTrackObject(ITransponderReceiver rec, IMonitoredArea monitoredArea, IParseTrackInfo parser, IPosition pos,
            ITimestamp timestamp, ITimestampFormatter formatter, IFlightDataExtractor flightExtractor, IOutput output)
        {
            rec.TransponderDataReady += OnTransponderDataReceived;

            _monitoredArea = monitoredArea;
            _parseTrack = parser;
            _position = pos;
            _timestamp = timestamp;
            _timestampFormatter = formatter;
            _extractedFlight = flightExtractor;
        }

        private void OnTransponderDataReceived(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            TrackList.Clear();
            //Traverse all elements
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                // Return list of parsed flight info
                List<string> parsedData = _parseTrack.Parse(data);

                _extractedFlight.ExtractFlight(parsedData, out var tag, ref _position, ref _timestamp);

                // If inside the monitored area
                if (_monitoredArea.InsideMonitoredArea(_position))
                {
                    // Format and return the date
                    _timestampFormatter.FormatTimestamp(_timestamp.UnformattedTimestamp);

                    TrackList.Add(new TrackObject(tag, _position, _timestampFormatter.InFormatted));
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
