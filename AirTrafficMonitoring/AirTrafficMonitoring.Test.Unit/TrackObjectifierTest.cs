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
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackObjectifierTest
    {
        private TrackObjectifier _uut;

        private ITransponderReceiver _receiver;
        private IMonitoredArea _monitoredArea;
        private IParseTrackInfo _parser;
        private IFlightDataHandler _flightHandler;
        private IPosition _position;
        private ITimestampFormatter _formatter;
        //private List<ITrackObject> TrackList;

        private List<string> _argList;
        private RawTransponderDataEventArgs args;

        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _monitoredArea = Substitute.For<IMonitoredArea>();
            _parser = Substitute.For<IParseTrackInfo>();
            _flightHandler = Substitute.For<IFlightDataHandler>();
            _position = Substitute.For<IPosition>();
            _formatter = Substitute.For<ITimestampFormatter>();
            //TrackList = new List<ITrackObject>();
            _uut = new TrackObjectifier(_receiver, _monitoredArea, _parser, _flightHandler, _position, _formatter);

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            args = new RawTransponderDataEventArgs(_argList);
        }

        public void RaiseFakeEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(args);
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedCorrect()
        {
            RaiseFakeEvent();

            _flightHandler.Received().Distribute(_parser.Parse(_argList[0]), out var tag, ref _position, ref _formatter);
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedSecondString()
        {
            _argList.Add("ADE458;78942;14520;1400;20111106213456459");
            RaiseFakeEvent();

            _flightHandler.Received().Distribute(_parser.Parse(_argList[1]), out var tag, ref _position, ref _formatter);
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistributeWithWrongPosition_DidNotReceive()
        {
            IPosition fake = Substitute.For<IPosition>();
            RaiseFakeEvent();

            _flightHandler.DidNotReceive().Distribute(_parser.Parse(_argList[0]), out var tag, ref fake, ref _formatter);
        }

        [Test]
        public void TrackObjectifier_MonitoredAreaInsideMonitoredArea_ReceivedCorrectPosition()
        {
            RaiseFakeEvent();

            _monitoredArea.Received().InsideMonitoredArea(_position);
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_ReceivedCall()
        {
            _monitoredArea.InsideMonitoredArea(_position).Returns(true);

            RaiseFakeEvent();

            _formatter.Received().FormatTimestamp();
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_DidNotReceiveCall()
        {
            RaiseFakeEvent();

            _uut.TrackListReady += 
        }


    }
}
