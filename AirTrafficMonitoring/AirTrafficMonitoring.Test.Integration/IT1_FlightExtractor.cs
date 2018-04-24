using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    class IT1_FlightExtractor
    {
        private IFlightExtractor _uut;

        private IMonitoredArea _monitoredArea;
        private ITrackList _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private IParseTrackInfo _parseTrackInfo;
        private ITimestampFormatter _timestampFormatter;

        private List<string> _parsed;
        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _uut = new FlightExtractor();
            _parseTrackInfo = Substitute.For<IParseTrackInfo>();

            _monitoredArea = Substitute.For<IMonitoredArea>();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();

            _parsed = new List<string> { "ATR423", "39045", "12932", "14000", "20151006213456789"};

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _monitoredArea, _parseTrackInfo, _uut, _timestampFormatter);

        }

        [Test]
        public void Extract_XCoordinate_ReturnsCorrect()
        {
            _parseTrackInfo.Parse("ATR423;39045;12932;14000;20151006213456789").Returns(_parsed);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);


            Assert.AreEqual(39045, _uut.Position.XCoor);
        }

        [Test]
        public void Extract_YCoordinate_ReturnsCorrect()
        {
            _parseTrackInfo.Parse("ATR423;39045;12932;14000;20151006213456789").Returns(_parsed);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(12932, _uut.Position.YCoor);
        }

        [Test]
        public void Extract_Altitude_ReturnsCorrect()
        {
            _parseTrackInfo.Parse("ATR423;39045;12932;14000;20151006213456789").Returns(_parsed);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(14000, _uut.Position.Altitude);
        }

    }
}
