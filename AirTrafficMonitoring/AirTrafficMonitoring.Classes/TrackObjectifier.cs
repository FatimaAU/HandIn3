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
        private static IOutput _output;

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
            _output = output;
        }

        private static void OnTransponderDataReceived(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            List<TrackObject> TrackList = new List<TrackObject>();
            
            Console.Clear();
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
                    string formattedTimeStamp = _timestampFormatter.FormatTimestamp(_timestamp.UnformattedTimestamp);

                    TrackList.Add(new TrackObject(tag, _position, formattedTimeStamp));
                    // Create track object and print info
                    //ITrackObject TrackObj = new TrackObject(tag, _position, formattedTimeStamp);
                    
                    //On


                    //_output.Print(TrackObj);
                }
            }
        }

        

        protected virtual void OnTrackListReady(TrackListUpdatedArgs e)
        {
            TrackListReady?.Invoke(this, e);
        }
    }
}
