using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using TransponderReceiver;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class TrackObjectifier : ITrackList
    {

        // ------ UDVIND DENNE KLASSE NÅR DET HELE ER OPDELT KORREKT.DENNE SKAL JO IKKE HAVE ALT TILDELT SOM MAIN HAR LIGE NU ------

        private static IMonitoredArea _monitoredArea;
        private static IParseTrackInfo _parser;
        private static IFlightDataHandler _flightHandler;
        private static IPosition _position;
        private static ITimestampFormatter _formatter;

        private List<ITrackObject> TrackList = new List<ITrackObject>();

        public event EventHandler<TrackListUpdatedArgs> TrackListReady;

        public TrackObjectifier(
            ITransponderReceiver rec, 
            IMonitoredArea monitoredArea, 
            IParseTrackInfo parser, 
            IFlightDataHandler flightHandler,
            IPosition position,
            ITimestampFormatter formatter)
        {
            rec.TransponderDataReady += CreateTrack;

            _monitoredArea = monitoredArea;
            _parser = parser;
            _flightHandler = flightHandler;
            _position = position;
            _formatter = formatter;
        }

        private void CreateTrack(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            TrackList.Clear();
            //Traverse all elements
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                // Distribute data to relevant classes
                _flightHandler.Distribute(_parser.Parse(data), out var tag, ref _position, ref _formatter);

                // If inside the monitored area
                if (_monitoredArea.InsideMonitoredArea(_position))
                {
                    // Format and return the date
                    _formatter.FormatTimestamp();

                    var pos = new Position();
                    pos.SetPosition(_position.XCoor, _position.YCoor, _position.Altitude);

                    var newTrack = new TrackObject(tag, pos, _formatter.InPretty, _formatter.InDateTime);

                    TrackList.Add(newTrack);
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
