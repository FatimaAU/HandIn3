using System;
using System.Collections.Generic;
using System.Security.Policy;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        public static readonly IMonitoredArea MonitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
        public static readonly IParseTrackInfo ParseTrack = new ParseTrackInfo();
        public static IPosition Position = new Position();
        public static ITimestamp Timestamp = new Timestamp();
        public static ITimestampFormatter TimestampFormatter = new TimestampFormatter();
        public static IFlightDataExtractor ExtractedFlight = new FlightDataExtractor();
        public static IOutput Output = new Output();

        static void Main(string[] args)
        {
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            receiver.TransponderDataReady += receiver_TransponderDataReady;

            //TrackObjectSystem.CreateTrackObjectSystem(receiver, MonitoredArea, ParseTrack, Position, Timestamp, TimestampFormatter, ExtractedFlight, Output);

            while (true) { }
        }

        public static void receiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Console.Clear();
            //Traverse all elements
            foreach (var data in e.TransponderData)
            {

                //NÆSTE STEP:
                //CreateTrackObjekt som wrapper alting:
                //parse en liste af data.
                //denne skal extractes:
                //position og formateret timestamp findes
                //tjek på om flyet er inde i MonitoredArea
                //hvis ja, create trackobject
                //herefter kan lave TrackObjectSystem som netop er denne klasse?

                // Return list of parsed flight info
                List<string> parsedData = ParseTrack.Parse(data);

                ExtractedFlight.ExtractFlight(parsedData, out var tag, ref Position, ref Timestamp);

                // If inside the monitored area
                if (MonitoredArea.InsideMonitoredArea(Position))
                {
                    // Format and return the date
                    TimestampFormatter.FormatTimestamp(Timestamp.UnformattedTimestamp);

                    // Create track object and print info
                    ITrackObject trackObj = new TrackObject(tag, Position, TimestampFormatter.InFormatted);
                    Output.Print(trackObj);
                }
            }
        }
    }
}
