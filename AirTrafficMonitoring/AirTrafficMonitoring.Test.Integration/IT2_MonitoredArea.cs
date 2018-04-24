using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    class IT2_MonitoredArea
    {
        private IMonitoredArea _uut;
        private TrackObjectifier _trackObjectifier;
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
            _uut = Substitute.For<IMonitoredArea>();

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _flightExtractor = new FlightExtractor();
            _parseTrackInfo = Substitute.For<IParseTrackInfo>();
            _timestampFormatter = Substitute.For<ITimestampFormatter>();

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _uut, _parseTrackInfo, _flightExtractor, _timestampFormatter);

            _parsed = new List<string> { "ATR423", "39045", "12932", "14000", "20151006213456789" };

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);
        }

        [Test]
        public void InsideMonitoredArea_Position_ReceivedCorrect()
        {
            _parseTrackInfo.Parse("ATR423;39045;12932;14000;20151006213456789").Returns(_parsed);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _uut.Received().InsideMonitoredArea(_flightExtractor.Position);
        }
    }
}
