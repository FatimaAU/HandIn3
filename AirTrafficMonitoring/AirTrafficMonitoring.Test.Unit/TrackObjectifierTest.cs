using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using Castle.Core.Smtp;
using NSubstitute;
using NSubstitute.Extensions;
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
        private IFlightExtractor _flightHandler;
        private ITimestampFormatter _formatter;

        private List<ITrackObject> _trackList;
        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _receiver = Substitute.For<ITransponderReceiver>();
            _monitoredArea = Substitute.For<IMonitoredArea>();
            _parser = Substitute.For<IParseTrackInfo>();
            _flightHandler = Substitute.For<IFlightExtractor>();
            _formatter = Substitute.For<ITimestampFormatter>();
            //TrackList = new List<ITrackObject>();

            _uut = new TrackObjectifier(_receiver, _monitoredArea, _parser, _flightHandler, _formatter);

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);

            _uut.TrackListReady += (sender, updatedArgs) =>
            {
                _trackList = updatedArgs.TrackList;
            };
        }

        public void RaiseFakeEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_args);
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedCorrect()
        {
            RaiseFakeEvent();

            _flightHandler.Received().Extract(_parser.Parse(_argList[0]));
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedSecondString()
        {
            _argList.Add("ADE458;78942;14520;1400;20111106213456459");
            RaiseFakeEvent();

            _flightHandler.Received().Extract(_parser.Parse(_argList[1]));
        }


        [Test]
        public void TrackObjectifier_MonitoredAreaInsideMonitoredArea_ReceivedCorrectPosition()
        {
            RaiseFakeEvent();

            _monitoredArea.Received().InsideMonitoredArea(_flightHandler.Position);
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_ReceivedCall()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeEvent();

            _formatter.Received().FormatTimestamp();
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_DidNotReceiveCall()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(false);

            RaiseFakeEvent();

            _formatter.DidNotReceive().FormatTimestamp();
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedEvent()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            ManualResetEvent received = new ManualResetEvent(false);

            _uut.TrackListReady += (sender, updatedArgs) => received.Set();

            RaiseFakeEvent();

            Assert.That(received.WaitOne());
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedList()
        {
            ManualResetEvent received = new ManualResetEvent(false);
            _flightHandler.Extract(_parser.Parse(_argList[0]));


            //_flightHandler.When(x => x.Distribute())
            //_position.XCoor.Returns(39045);
            //_position.YCoor.Returns(45120);
            //_position.Altitude.Returns(4500);


            _uut.TrackListReady += (sender, updatedArgs) => received.Set();

            string expectedTag = "ATR234";

            _monitoredArea.InsideMonitoredArea(_position).Returns(true);

            RaiseFakeEvent();

            //received.WaitOne();

            Assert.That(tag, Is.EqualTo("hh"));
        }
    }
}
