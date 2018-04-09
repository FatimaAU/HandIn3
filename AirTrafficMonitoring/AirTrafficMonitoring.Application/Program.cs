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
        static ExtractPosition extractPos = new ExtractPosition();
        static ExtractTimestamp extractTime = new ExtractTimestamp();
        static TimestampFormatter timestampFormatter = new TimestampFormatter();


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

                extractPos.Position(parsedData, out var xPos, out var yPos, out var Alt);
                string timeStamp = extractTime.Timestamp(parsedData);


                //var parsedFlightList = ParseFlightInfo.Parse(data);

                // If inside the monitored area
                if (monitoredArea.InsideMonitoredArea(xPos, yPos, Alt))
                {
                    // Format and return the date
                    string formattedTimeStamp = timestampFormatter.FormatTimestamp(timeStamp);

                    //var date = FormatDate.FormatDate(parsedFlightList);

                    // Create track object and print info
                    Track myTrack = new Track(parsedData[0], xPos, yPos, Alt, formattedTimeStamp);
                    Output myOutput = new Output();
                    myOutput.Print(myTrack);
                }
            }
        } 
    }
}
