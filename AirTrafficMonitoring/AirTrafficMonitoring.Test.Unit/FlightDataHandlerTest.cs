using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class FlightDataExtractorTest
    {
        /*
        * UNIT TEST DESCRIPTION
        * Unit tests on FlightDataExtractor that test that the
        * flight data is extracted correctly from the string
        */
        private List<string> _flightList;


        private IPosition _position;
        private ITimestampFormatter _timestampObj;
        private IFlightDataHandler _flightData;

        [SetUp]
        public void Setup()
        {
            _position = new Position();
            _timestampObj = new TimestampFormatter();
            _flightData = new FlightDataHandler();

            _flightList = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            // Define tag and check correct tag returned
            string expectedTag = _flightList[0];
            _flightData.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(expectedTag, tag);
        }

        [Test]
        public void Flight_SetX_ReturnsX()
        {
            // Define x coodrinate and check correct x coordinate returned
            int expectedXCoor = int.Parse(_flightList[1]);
            _flightData.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(expectedXCoor, _position.XCoor);
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            // Define y coordinate and check correct y coordinate returned
            int expectedYCoor = int.Parse(_flightList[2]);
            _flightData.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(expectedYCoor, _position.YCoor);
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            // Define altitude and check correct altitude returned
            int expectedAltitude = int.Parse(_flightList[3]);
            _flightData.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(expectedAltitude, _position.Altitude);
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            // Define timestamp and check correct timestamp returned
            string expectedTimestamp = _flightList[4];
            _flightData.Distribute(_flightList, out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(expectedTimestamp, _timestampObj.Unformatted);
        }
    }
}
