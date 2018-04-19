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
        public static ICalculateCourse Course = new CalculateCourse();
        public static ICalculateVelocity Velocity = new CalculateVelocity();

        public static List<ITrackObject> NewTrackList = new List<ITrackObject>();
        public static List<ITrackObject> OldTrackList;

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

            if (OldTrackList == null)
            {
                OldTrackList = NewTrackList;

                foreach (var track in OldTrackList)
                    Console.WriteLine(track);

                return;

            }
            
            foreach (var data in NewTrackList)
            {
                foreach (var track in OldTrackList)
                {

                }
            }



            // Same size
            //while (TrackObjectList.Count < trackListUpdatedArgs.TrackList.Count)
            //    TrackObjectList.Add(null);

            //foreach (var data in trackListUpdatedArgs.TrackList)
            //{
            //    for (int i = 0; i < TrackObjectList.Count; i++)
            //    {
            //        if (TrackObjectList[i] == null)
            //        {
            //            Console.WriteLine("add");
            //            TrackObjectList[i] = data;
            //        }
            //        else if (TrackObjectList[i].Tag == data.Tag)
            //        {
            //            Console.WriteLine("update things");
            //        }
            //        else if (TrackObjectList[i].Tag == data.Tag)
            //        {

            //        }
            //        //else
            //        //{
            //        //    Console.WriteLine("remove");
            //        //    TrackObjectList.Remove(TrackObjectList[i]);
            //        //}

            //    }
            //}

            //foreach (var track in TrackObjectList)
            //    Console.WriteLine(track);


            //Console.Clear();
            //if (TrackObjectList == null)
            //{
            //    TrackObjectList = new List<ITrackObject>();
            //    foreach (var track in trackListUpdatedArgs.TrackList)
            //            TrackObjectList.Add(track);

            //    Console.WriteLine("init");
            //}

            //int eventListLength = trackListUpdatedArgs.TrackList.Count;

            //while (TrackObjectList.Count < eventListLength)
            //{
            //    TrackObjectList.Add(null);

            //    Console.WriteLine($"length: {TrackObjectList.Count}");
            //}

            //for (int i = 0; i < eventListLength; i++)
            //{
            //    var newTrack = trackListUpdatedArgs.TrackList[i];

            //    // Compare lists, fill 'TrackObjectList'
            //    if (TrackObjectList[i] == null) //throws exception 
            //    {
            //        TrackObjectList[i] = newTrack;
            //        Console.WriteLine("adding");
            //    }
            //    else if (TrackObjectList[i].Tag == newTrack.Tag)
            //    {
            //        Console.WriteLine("calculate things .. or validate them first");
            //    }
            //    //else if (TrackObjectList[i].Tag != newTrack.Tag)
            //    //{
            //    //    TrackObjectList.Remove(TrackObjectList[i]);
            //    //    Console.WriteLine(" not equal, removing");
            //    //}

            //}

            //foreach (var track in TrackObjectList)
            //    Console.WriteLine(track);
        }
    }
}

