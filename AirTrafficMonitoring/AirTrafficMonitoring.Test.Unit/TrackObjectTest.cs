using System;
using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
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
        private ITrackObject _uut;

        private IPosition _position;
        private string _time;
        private DateTime _dateTime;
        
        [SetUp]
        public void Setup()
        {
            _position = Substitute.For<IPosition>();
            _position.XCoor.Returns(50456);
            _position.YCoor.Returns(78455);
            _position.Altitude.Returns(7852);

            _time = "December 1st, 2018, at 11:11:11 and 111 milliseconds";
            _dateTime = new DateTime(2018, 11, 11, 11, 11, 11, 111);

            _uut = new TrackObject("KVK7896", _position, _time, _dateTime);
        }

        [Test]
        public void Track_SetTag_ReturnsTag()
        {
            string expectedTag = "KVK7896";
            Assert.AreEqual(expectedTag, _uut.Tag);
        }

        [Test]
        public void Track_SetXCoordinate_ReturnsXCoordinate()
        {
            int expectedXCoor = 50456;
            Assert.AreEqual(expectedXCoor, _uut.Position.XCoor);
        }

        [Test]
        public void Track_SetYCoordinate_ReturnYCoordinate()
        {
            int expectedYCoor = 78455;
            Assert.AreEqual(expectedYCoor, _uut.Position.YCoor);
        }

        [Test]
        public void Track_SetAltitude_ReturnAltitude()
        {
            int altitude = 7852;
            Assert.AreEqual(altitude, _uut.Position.Altitude);
        }

        [Test]
        public void Track_SetTimestamp_ReturnTimestamp()
        {
            string expectedTimestamp = "December 1st, 2018, at 11:11:11 and 111 milliseconds";
            Assert.AreEqual(expectedTimestamp, _uut.Timestamp);
        }

        [Test]
        public void Track_DateTime_ReturnCorrect()
        {
            DateTime expectedDateTime = new DateTime(2018, 11, 11, 11, 11, 11, 111);
            Assert.AreEqual(expectedDateTime, _uut.InDateTime);
        }

        [Test]
        public void Track_CourseIsZero_ReturnDefault()
        {
            int expectedCourse = 0;
            Assert.AreEqual(expectedCourse, _uut.Course);
        }

        [Test]
        public void Track_CourseIsNotSet_NotEqual()
        {
            int expectedCourse = 100;
            Assert.AreNotEqual(expectedCourse, _uut.Course);
        }

        [Test]
        public void Track_VelocityIsZero_ReturnDefault()
        {
            int expectedVelocity = 0;
            Assert.AreEqual(expectedVelocity, _uut.Velocity);
        }

        [Test]
        public void Track_VelocityIsNotSet_NotEqual()
        {
            int expectedVelocity = 200;
            Assert.AreNotEqual(expectedVelocity, _uut.Velocity);
        }

        [Test]
        public void Track_ToString_ReturnsToString()
        {
            string expectedString = "Tag:\t\t" + "KVK7896" + "\n" +
                                    "X coordinate:\t" + 50456 + " meters \n" +
                                    "Y coordinate:\t" + 78455 + " meters\n" +
                                    "Altitide:\t" + 7852 + " meters\n" +
                                    "Timestamp:\t" + "December 1st, 2018, at 11:11:11 and 111 milliseconds" + "\n" +
                                    "Velocity:\t" + 0 + " m/s\n" +
                                    "Course:\t\t" + 0 + " degrees\n";

            Assert.AreEqual(expectedString, _uut.ToString());
        }

    }
}
