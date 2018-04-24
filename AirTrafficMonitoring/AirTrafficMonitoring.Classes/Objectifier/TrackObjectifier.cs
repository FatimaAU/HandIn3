using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using TransponderReceiver;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class TrackObjectifier : ITrackList
    {

        private static IMonitoredArea _monitoredArea;
        private static IParseTrackInfo _parser;
        private static IFlightExtractor _flightHandler;
        private static ITimestampFormatter _formatter;
        private static IPositionFactory _positionFac;
        private static ITrackObjectFactory _trackObjFac;

        private List<ITrackObject> TrackList = new List<ITrackObject>();

        public event EventHandler<TrackListUpdatedArgs> TrackListReady;

        public TrackObjectifier(
            ITransponderReceiver rec,
            IMonitoredArea monitoredArea,
            IParseTrackInfo parser,
            IFlightExtractor flightHandler,
            ITimestampFormatter formatter,
            IPositionFactory positionFac,
            ITrackObjectFactory trackObjFac)
        {
            rec.TransponderDataReady += CreateTrack;

            _monitoredArea = monitoredArea;
            _parser = parser;
            _flightHandler = flightHandler;
            _formatter = formatter;
            _positionFac = positionFac;
            _trackObjFac = trackObjFac;

        }

        private void CreateTrack(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            TrackList.Clear();
            //Traverse all elements
            foreach (var data in rawTransponderDataEventArgs.TransponderData)
            {
                // Distribute data to relevant classes

                _flightHandler.Extract(_parser.Parse(data), _positionFac);

                // If inside the monitored area
                if (_monitoredArea.InsideMonitoredArea(_flightHandler.Position))
                {
                    // Format and return the date
                    _formatter.Unformatted = _flightHandler.RawTimestamp;
                    _formatter.FormatTimestamp();

                    var newPosition = _positionFac.CreatePosition
                    (_flightHandler.Position.XCoor, _flightHandler.Position.YCoor,
                        _flightHandler.Position.Altitude);

                    var newTrack = _trackObjFac.CreateTrackObject(
                        _flightHandler.Tag, newPosition, _formatter.InPretty, _formatter.InDateTime);

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
