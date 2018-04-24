using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    class IT9_TrackObject
    {
        private IMonitoredArea _monitoredArea;
        private TrackObjectifier _trackObjectifier;
        private IPosition _position;
        private ITransponderReceiver _transponderReceiver;
        private IParseTrackInfo _parseTrackInfo;
        private IFlightExtractor _flightExtractor;
        private ITimestampFormatter _timestampFormatter;
        private ISeparation _seperationEvent;
        private IVelocity _velocity;
        private IDistance _distance;
        private ATMSystem ATM;
        private IListHandler _listHandler;

        private IList<ITrackObject> _trackList;
        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _listHandler = Substitute.For<IListHandler>();
            _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            _seperationEvent = Substitute.For<ISeparation>(); 
            _distance = new Distance();
            _velocity = new Velocity();

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _flightExtractor = new FlightExtractor();
            _parseTrackInfo = new ParseTrackInfo();
            _timestampFormatter = new TimestampFormatter();

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _monitoredArea, _parseTrackInfo, _flightExtractor, _timestampFormatter);
            ATM = new ATMSystem(_trackObjectifier, _listHandler);

            _argList = new List<string>
            {
                "ATR423;39045;12932;14000;20151006213456789",
                "DSD323;40000;12930;15000;20151006213456789"
            };
            _args = new RawTransponderDataEventArgs(_argList);

            _trackObjectifier.TrackListReady += (sender, updatedArgs) =>
            {
                _trackList = updatedArgs.TrackList;
            };
        }

        [Test]
        public void TrackObject_CreateTrackList_ReceivedCorrect()
        {
            string expectedTrack = "Tag:\t\t" + "ATR423" + "\n" +
                                  "X coordinate:\t" + 39045 + " meters \n" +
                                  "Y coordinate:\t" + 12932 + " meters\n" +
                                  "Altitide:\t" + 14000 + " meters\n" +
                                  "Timestamp:\t" + "October 6th, 2015, at 21:34:56 and 789 milliseconds" + "\n" +
                                  "Velocity:\t" + 0 + " m/s\n" +
                                  "Course:\t\t" + 0 + " degrees\n";


            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(expectedTrack, _trackList[0].ToString());
        }

        [Test]
        public void TrackObject_SeparationEventReceivedParamters_ReceivedCorrect()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _seperationEvent.Received().IsConflicting(_trackList[0], _trackList[1], _distance);
        }


    }
}
