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

        private IPosition _position;
        private ITrackObject _trackObject;

        private IList<ITrackObject> _trackList;
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

            _position = Substitute.For<IPosition>();

            _uut = new TrackObjectifier(
                _receiver, 
                _monitoredArea, 
                _parser, 
                _flightHandler, 
                _formatter);

            _argList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _args = new RawTransponderDataEventArgs(_argList);

            _uut.TrackListReady += (sender, updatedArgs) =>
            {
                _trackList = updatedArgs.TrackList;
            };
        }

        public void RaiseFakeTransponderEvent()
        {
            _receiver.TransponderDataReady += Raise.EventWith(_args);
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedCorrect()
        {
            RaiseFakeTransponderEvent();

            _flightHandler.Received().Extract(_parser.Parse(_argList[0]));
        }

        [Test]
        public void TrackObjectifier_FlighthandlerDistribute_ReceivedSecondString()
        {
            _argList.Add("ADE458;78942;14520;1400;20111106213456459");
            RaiseFakeTransponderEvent();

            _flightHandler.Received().Extract(_parser.Parse(_argList[1]));
        }


        [Test]
        public void TrackObjectifier_MonitoredAreaInsideMonitoredArea_ReceivedCorrectPosition()
        {
            RaiseFakeTransponderEvent();

            _monitoredArea.Received().InsideMonitoredArea(_flightHandler.Position);
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_ReceivedCall()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);
            RaiseFakeTransponderEvent();

            _formatter.Received().FormatTimestamp();
        }

        [Test]
        public void TrackObjectifier_FormatterFormateTimestamp_DidNotReceiveCall()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(false);

            RaiseFakeTransponderEvent();

            _formatter.DidNotReceive().FormatTimestamp();
        }


        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedEvent()
        {
            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            ManualResetEvent received = new ManualResetEvent(false);

            _uut.TrackListReady += (sender, updatedArgs) => received.Set();

            RaiseFakeTransponderEvent();

            Assert.That(received.WaitOne());
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedTag()
        {
            string expectedTag = "ATR234";

            _flightHandler.Tag.Returns(expectedTag);

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.AreEqual(expectedTag, _trackList[0].Tag);
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedXCoordinate()
        {
            int expectedXCoor = 39045;

            _flightHandler.Position.XCoor.Returns(expectedXCoor);

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedXCoor, Is.EqualTo(_trackList[0].Position.XCoor));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedYCoordinate()
        {
            int expectedYCoor = 12932;

            _flightHandler.Position.YCoor.Returns(expectedYCoor);

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedYCoor, Is.EqualTo(_trackList[0].Position.YCoor));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedAltitude()
        {
            int expectedAltitude = 14000;

            _flightHandler.Position.Altitude.Returns(expectedAltitude);

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedAltitude, Is.EqualTo(_trackList[0].Position.Altitude));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedTimestamp()
        {
            string formattedTimestamp = "February 10, 2016, at 15:23:23 and 324 milliseconds";

            _formatter.InPretty = formattedTimestamp;

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(formattedTimestamp, Is.EqualTo(_trackList[0].Timestamp));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedDateTime()
        {
            DateTime inDateTime = new DateTime(2016, 02, 10, 15, 23, 23, 324);

            _formatter.InDateTime = inDateTime;

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(inDateTime, Is.EqualTo(_trackList[0].InDateTime));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedCourseIsZero()
        {
            int expectedCourse = 0;

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedCourse, Is.EqualTo(_trackList[0].Course));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedVelocityIsZero()
        {
            int expectedVelocity = 0;

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedVelocity, Is.EqualTo(_trackList[0].Velocity));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedFullTrack()
        {
            string expectedTrackOutput = "Tag:\t\t" + "HAJ7852" + "\n" +
                                         "X coordinate:\t" + 45623 + " meters \n" +
                                         "Y coordinate:\t" + 78452 + " meters\n" +
                                         "Altitide:\t" + 4562 + " meters\n" +
                                         "Timestamp:\t" + "October 16th, 2008, at 13:10:12 and 326 milliseconds" + "\n" +
                                         "Velocity:\t" + 0 + " m/s\n" +
                                         "Course:\t\t" + 0 + " degrees\n";

            _flightHandler.Tag.Returns("HAJ7852");
            _flightHandler.Position.XCoor.Returns(45623);
            _flightHandler.Position.YCoor.Returns(78452);
            _flightHandler.Position.Altitude.Returns(4562);

            _formatter.InPretty = "October 16th, 2008, at 13:10:12 and 326 milliseconds";

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedTrackOutput, Is.EqualTo(_trackList[0].ToString()));
        }

        [Test]
        public void TrackObjectifier_ITrackEventRaised_ReceivedFullSecondTrack()
        {
            _argList.Add("ADS7850;74105;90000;19000;20120509103456459");

            string expectedTrackOutput = "Tag:\t\t" + "ADS7850" + "\n" +
                                         "X coordinate:\t" + 74105 + " meters \n" +
                                         "Y coordinate:\t" + 90000 + " meters\n" +
                                         "Altitide:\t" + 19000 + " meters\n" +
                                         "Timestamp:\t" + "May 9th, 2012, at 10:34:56 and 459 milliseconds" + "\n" +
                                         "Velocity:\t" + 0 + " m/s\n" +
                                         "Course:\t\t" + 0 + " degrees\n";

            _flightHandler.Tag.Returns("ADS7850");
            _flightHandler.Position.XCoor.Returns(74105);
            _flightHandler.Position.YCoor.Returns(90000);
            _flightHandler.Position.Altitude.Returns(19000);

            _formatter.InPretty = "May 9th, 2012, at 10:34:56 and 459 milliseconds";

            _monitoredArea.InsideMonitoredArea(_flightHandler.Position).Returns(true);

            RaiseFakeTransponderEvent();

            Assert.That(expectedTrackOutput, Is.EqualTo(_trackList[1].ToString()));
        }


    }
}
