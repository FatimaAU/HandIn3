using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    class IT7_TimestampFormatter
    {
        private IMonitoredArea _monitoredArea;
        private TrackObjectifier _trackObjectifier;
        private IPosition _position;
        private ITransponderReceiver _transponderReceiver;
        private IParseTrackInfo _parseTrackInfo;
        private IFlightExtractor _flightExtractor;
        private ITimestampFormatter _timestampFormatter;
        private IList<ITrackObject> _trackList;

        private List<string> _parsed;
        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _flightExtractor = new FlightExtractor();
            _parseTrackInfo = Substitute.For<IParseTrackInfo>();
            _timestampFormatter = new TimestampFormatter();
            _position = Substitute.For<IPosition>();

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _monitoredArea, _parseTrackInfo, _flightExtractor, _timestampFormatter);

            _parsed = new List<string> { "ATR423", "39045", "12932", "14000", "20151006213456789" };

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);
        }

        [Test]
        public void InsideMonitoredArea_Position_ReceivedCorrect()
        {
            _parseTrackInfo.Parse("ATR423;39045;12932;14000;20151006213456789").Returns(_parsed);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            Assert.AreEqual(_timestampFormatter.Unformatted, _flightExtractor.RawTimestamp);
        }
    }
}
