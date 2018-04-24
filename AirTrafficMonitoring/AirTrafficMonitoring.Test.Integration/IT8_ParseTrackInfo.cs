using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    class IT8_ParseTrackInfo
    {
        private IMonitoredArea _monitoredArea;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private IParseTrackInfo _parseTrackInfo;
        private IFlightExtractor _flightExtractor;
        private ITimestampFormatter _timestampFormatter;
        private IList<ITrackObject> _trackList;

        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _monitoredArea = Substitute.For<IMonitoredArea>();

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _flightExtractor = new FlightExtractor();
            _parseTrackInfo = new ParseTrackInfo();
            _timestampFormatter = Substitute.For<ITimestampFormatter>();

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _monitoredArea, _parseTrackInfo, _flightExtractor, _timestampFormatter);

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);
        }

        [Test]
        public void Parse_PositionXCoordinate_ReceivedCorrect()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(39045, _flightExtractor.Position.XCoor);
        }

        [Test]
        public void Parse_PositionYCoordinate_ReceivedCorrect()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(12932, _flightExtractor.Position.YCoor);
        }

        [Test]
        public void Parse_PositionAltitude_ReceivedCorrect()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(14000, _flightExtractor.Position.Altitude);
        }
    }
}
