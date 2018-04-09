using System;
using System.Collections.Generic;
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
        public static IFlightData ExtractedFlight = new FlightData();


        static void Main(string[] args)
        {
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            receiver.TransponderDataReady += receiver_TransponderDataReady;
            while (true) { }
        }

        public static void receiver_TransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Console.Clear();
            //Traverse all elements
            foreach (var data in e.TransponderData)
            {
                // Return list of parsed flight info
                List<string> parsedData = ParseTrack.Parse(data);

                ExtractedFlight.ExtractFlight(parsedData, out var tag, ref Position, ref Timestamp);

                //extractPos.Position(parsedData, out var xPos, out var yPos, out var Alt);
                //string timeStamp = extractTime.Timestamp(parsedData);


                //var parsedFlightList = ParseFlightInfo.Parse(data);

                // If inside the monitored area
                if (MonitoredArea.InsideMonitoredArea(Position))
                {
                    // Format and return the date
                    string formattedTimeStamp = TimestampFormatter.FormatTimestamp(Timestamp.UnformattedTimestamp);

                    //var date = FormatDate.FormatDate(parsedFlightList);

                    // Create track object and print info
                    ITrackObject myTrack = new TrackObject(tag, Position, formattedTimeStamp);
                    IOutput myOutput = new Output();
                    myOutput.Print(myTrack);
                }
            }
        } 
    }
}
