using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackTest
    {
        private List<string> _flightList;
        private string _tag;
        private string _x;
        private string _y;
        private string _altitude;
        private string _timestamp;

        [SetUp]
        public void Setup()
        {
            _flightList = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _tag = "TAGGGG";
            _x = "50000";
            _y = "50000";
            _altitude = "5000";
            _timestamp = "20181111111111111";
        }

        [Test]
        public void Track_SetTag_ReturnsTag()
        {
            Track _testTrack = new Track(_tag, new Position(), _timestamp);
            Assert.AreEqual(_tag, _testTrack.Tag);
        }

        [Test]
        public void Track_SetXCoordinate_ReturnsXCoordinate()
        {
            Position _testPosition = new Position();
            _testPosition.SetPosition(_x,_y,_altitude);
            Track _testTrack = new Track(_tag, _testPosition, _timestamp);
            Assert.AreEqual(_x, _testTrack.XCoordinate);
        }

        [Test]
        public void Track_SetYCoordinate_ReturnYCoordinate()
        {
            Position _testPosition = new Position();
            _testPosition.SetPosition(_x, _y, _altitude);
            Track _testTrack = new Track(_tag, _testPosition, _timestamp);
            Assert.AreEqual(_y, _testTrack.YCoordinate);
        }

        [Test]
        public void Track_SetAltitude_ReturnAltitude()
        {
            Position _testPosition = new Position();
            _testPosition.SetPosition(_x, _y, _altitude);
            Track _testTrack = new Track(_tag, _testPosition, _timestamp);
            Assert.AreEqual(_altitude, _testTrack.Altitude);
        }

        [Test]
        public void Track_SetTimestamp_ReturnTimestamp()
        {
            Position _testPosition = new Position();
            _testPosition.SetPosition(_x, _y, _altitude);
            Track _testTrack = new Track(_tag, _testPosition, _timestamp);
            Assert.AreEqual(_timestamp, _testTrack.TimeStamp);
        }

    }
}
