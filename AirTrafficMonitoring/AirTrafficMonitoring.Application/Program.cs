using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Policy;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using TransponderReceiver;

namespace AirTrafficMonitoring.Application
{
    class Program
    {
        private static ITransponderReceiver receiver;
        public static readonly IMonitoredArea MonitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
        public static readonly IParseTrackInfo Parser = new ParseTrackInfo();
        public static IPosition Position = new Position();
        public static ITimestampFormatter Formatter = new TimestampFormatter();
        public static IFlightDataHandler Handler = new FlightDataHandler(Position, Formatter);
        public static ICalculateCourse CourseCalc = new CalculateCourse();
        public static CalculateVelocity VelocityCalc = new CalculateVelocity();

        public static List<ITrackObject> NewTrackList = new List<ITrackObject>();
        public static List<ITrackObject> OldTrackList = new List<ITrackObject>();

        //public static TrackObjectifier Objectifier = new TrackObjectifier(receiver, MonitoredArea, Parser, Position, Formatter, Handler);

        static void Main(string[] args)
        {
            receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            TrackObjectifier objectifier =
                new TrackObjectifier(receiver, MonitoredArea, Parser, Position, Formatter, Handler);

            //receiver.TransponderDataReady += receiver_TransponderDataReady;

            objectifier.TrackListReady += ObjectifierOnTrackListReady;

            //TrackObjectSystem.CreateTrackObjectSystem(receiver, MonitoredArea, ParseTrack, Position, Timestamp, TimestampFormatter, ExtractedFlight, Output);

            Console.ReadKey();
        }

        private static void ObjectifierOnTrackListReady(object sender, TrackListUpdatedArgs trackListUpdatedArgs)
        {
            NewTrackList = trackListUpdatedArgs.TrackList;

            foreach (var data in NewTrackList)
            {
                foreach (var track in OldTrackList)
                {
                    if (track.Tag == data.Tag)
                    {
                        track.Velocity = VelocityCalc.Velocity(data, track);
                        //need to pass track object, new and old 
                        Console.WriteLine("update stuff");
                    }
                }
            }

            foreach (var track in OldTrackList)
                Console.WriteLine(track);

            OldTrackList.Clear();

            OldTrackList = NewTrackList;
        }
    }
}

