//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Security.AccessControl;
//using System.Security.Policy;
//using AirTrafficMonitoring.Classes;
//using AirTrafficMonitoring.Classes.Calculators;
//using AirTrafficMonitoring.Classes.Calculators.Interfaces;
//using AirTrafficMonitoring.Classes.Objectifier;
//using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
//using AirTrafficMonitoring.Classes.TrackListReadyEvent;
//using AirTrafficMonitoring.Classes.UpdateAndCheck;
//using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
//using TransponderReceiver;

//namespace AirTrafficMonitoring.Application
//{
//    class Program
//    {
//        private static ITransponderReceiver _receiver;
//        public static readonly IMonitoredArea MonitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
//        public static readonly IParseTrackInfo Parser = new ParseTrackInfo();
//        public static IPosition Position = new Position();
//        public static ITimestampFormatter Formatter = new TimestampFormatter();
//        public static IFlightExtractor Handler = new FlightDataHandler();
//        public static ICourse CourseCalc = new Course();
//        public static IVelocity VelocityCalc = new Velocity();
//        public static IDistance Distance = new Distance();
//        public static ISeparation Separation = new Separation();
//        public static IListHandler TrackListHandler = new ListHandler(VelocityCalc, CourseCalc, Separation, Distance);


//        //public static TrackObjectifier Objectifier = new TrackObjectifier(receiver, MonitoredArea, Parser, Position, Formatter, Handler);

//        static void Main(string[] args)
//        {
//            _receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
//            ITrackList objectifier =
//                new TrackObjectifier(_receiver, MonitoredArea, Parser, Handler, Position, Formatter);

//            //receiver.TransponderDataReady += receiver_TransponderDataReady;

//            objectifier.TrackListReady += ObjectifierOnTrackListReady;

//            //TrackObjectSystem.CreateTrackObjectSystem(receiver, MonitoredArea, ParseTrack, Position, Timestamp, TimestampFormatter, ExtractedFlight, Output);

//            Console.ReadKey();
//        }


//        private static void ObjectifierOnTrackListReady(object sender, TrackListUpdatedArgs trackListUpdatedArgs)
//        {
//            Console.Clear();

//            List<ITrackObject> newTrackList = trackListUpdatedArgs.TrackList;

//            if (TrackListHandler.Initiate(newTrackList))
//                return;

//            TrackListHandler.Update(newTrackList);

//            Console.WriteLine(TrackListHandler);

//            TrackListHandler.PrintSeperationEvent();

//            TrackListHandler.Renew(newTrackList);
//        }
//    }
//}

