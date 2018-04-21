using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    /*
    * UNIT TEST DESCRIPTION
    * Unit tests on TrackObject that test
    * the correct information is received when assigned
    */
    [TestFixture]
    class TrackObjectTest
    {
        private List<string> _flightList;
        private string _tag;
        private int _x;
        private int _y;
        private int _altitude;
        private string _timestamp;

        private IPosition _position;
        private ITrackObject _trackObject;
        private DateTime _dateTime;

        [SetUp]
        public void Setup()
        {
            _flightList = new List<string> {"TAGGGG", "50000", "40000", "5000", "20181111111111111" };

            _tag = _flightList[0];
            _x = int.Parse(_flightList[1]);
            _y = int.Parse(_flightList[2]);
            _altitude = int.Parse(_flightList[3]);
            _timestamp = _flightList[4];

            _position = new Position();
            _position.SetPosition(_x, _y, _altitude);
            _dateTime = new DateTime();
            _trackObject = new TrackObject(_tag, _position, _timestamp, _dateTime);
        }

        [Test]
        public void Track_SetTag_ReturnsTag()
        {
            Assert.AreEqual(_tag, _trackObject.Tag);
        }

        [Test]
        public void Track_SetXCoordinate_ReturnsXCoordinate()
        {
            Assert.AreEqual(_x, _trackObject.Position.XCoor);
        }

        [Test]
        public void Track_SetYCoordinate_ReturnYCoordinate()
        {
            Assert.AreEqual(_y, _trackObject.Position.YCoor);
        }

        [Test]
        public void Track_SetAltitude_ReturnAltitude()
        {
            Assert.AreEqual(_altitude, _trackObject.Position.Altitude);
        }

        [Test]
        public void Track_SetTimestamp_ReturnTimestamp()
        {
            Assert.AreEqual(_timestamp, _trackObject.Timestamp);
        }
    }
}
