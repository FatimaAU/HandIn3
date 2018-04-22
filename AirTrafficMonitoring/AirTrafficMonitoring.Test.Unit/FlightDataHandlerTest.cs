using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class FlightDataHandlerTest
    {
        /*
        * UNIT TEST DESCRIPTION
        * Unit tests on FlightDataHandler that test that the
        * flight data is extracted correctly from the string and given back to classes
        */
        private IFlightDataHandler _uut;

        private IPosition _position;
        private ITimestampFormatter _timestampObj;

        private List<string> _flightList;

        [SetUp]
        public void Setup()
        {
            _uut = new FlightDataHandler();

            _position = Substitute.For<IPosition>();
            _timestampObj = Substitute.For<ITimestampFormatter>();

            _flightList = new List<string> { "TAGGGG", "50000", "50032", "4000", "20181111111111111" };
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            // Define tag and check correct tag returned
            string expectedTag = "TAGGGG";
            _uut.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);

            Assert.AreEqual(expectedTag, tag);
        }


        [Test]
        public void Flight_SetX_ReturnsX()
        {
            // Define x coordinate and check correct x coordinate returned

            int expectedXCoor = 50000;

            _uut.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);

            _position.Received().SetPosition(expectedXCoor, 50032, 4000);
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            // Define y coordinate and check correct y coordinate returned
            int expectedYCoor = 50000;

            _uut.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);

            _position.Received().SetPosition(expectedYCoor, 50032, 4000);
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            // Define altitude and check correct altitude returned
            int expectedAltitude = 4000;
            _uut.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);

            _position.Received().SetPosition(50000, 50032, expectedAltitude);
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            // Define timestamp and check correct timestamp returned
            string expectedTimestamp = "20181111111111111";

            _uut.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);

            _timestampObj.Received().Unformatted = expectedTimestamp;
        }
    }
}
