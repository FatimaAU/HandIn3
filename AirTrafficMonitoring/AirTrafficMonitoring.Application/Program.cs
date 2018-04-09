using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using TransponderReceiver;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        static MonitoredArea monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
        static ParseTrackInfo parseTrack = new ParseTrackInfo();
        static Position extractPos = new Position();
        static Timestamp extractTime = new Timestamp();
        static TimestampFormatter timestampFormatter = new TimestampFormatter();
        static Flight extractFlight = new Flight();


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
                List<string> parsedData = parseTrack.Parse(data);

                extractFlight.ExtractFlight(parsedData, out var Tag, ref extractPos, ref extractTime);

                //extractPos.Position(parsedData, out var xPos, out var yPos, out var Alt);
                //string timeStamp = extractTime.Timestamp(parsedData);


                //var parsedFlightList = ParseFlightInfo.Parse(data);

                // If inside the monitored area
                if (monitoredArea.InsideMonitoredArea(extractPos))
                {
                    // Format and return the date
                    string formattedTimeStamp = timestampFormatter.FormatTimestamp(extractTime.UnformattedTimestamp);

                    //var date = FormatDate.FormatDate(parsedFlightList);

                    // Create track object and print info
                    Track myTrack = new Track(Tag, extractPos, formattedTimeStamp);
                    Output myOutput = new Output();
                    myOutput.Print(myTrack);
                }
            }
        } 
    }
}
