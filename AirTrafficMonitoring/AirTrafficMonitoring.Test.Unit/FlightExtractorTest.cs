using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class FlightExtractorTest
    {
        /*
        * UNIT TEST DESCRIPTION
        * Unit tests on FlightDataHandler that test that the
        * flight data is extracted correctly from the string and given back to classes
        */
        private IFlightExtractor _uut;

        private List<string> _flightList;

        [SetUp]
        public void Setup()
        {
            _uut = new FlightExtractor();

            _flightList = new List<string> { "TAGGGG", "50000", "50032", "4000", "20181111111111111" };

            _uut.Extract(_flightList);
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            // Define tag and check correct tag returned
            string expectedTag = "TAGGGG";

            Assert.AreEqual(expectedTag, _uut.Tag);
        }


        [Test]
        public void Flight_SetX_ReturnsX()
        {
            int expectedX = 50000;
            // Define x coordinate and check correct x coordinate returned
            Assert.That(expectedX, Is.EqualTo(_uut.Position.XCoor));
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            // Define y coordinate and check correct y coordinate returned
            int expectedYCoor = 50032;

            Assert.That(expectedYCoor, Is.EqualTo(_uut.Position.YCoor));
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            // Define altitude and check correct altitude returned
            int expectedAltitude = 4000;

            Assert.That(expectedAltitude, Is.EqualTo(_uut.Position.Altitude));
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            // Define timestamp and check correct timestamp returned
            string expectedTimestamp = "20181111111111111";

            Assert.That(expectedTimestamp, Is.EqualTo(_uut.RawTimestamp));

        }
    }
}
