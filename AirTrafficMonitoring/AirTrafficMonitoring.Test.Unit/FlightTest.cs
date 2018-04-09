using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class FlightTest
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
            _flightList = new List<string>{ "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _tag = "TAGGGG";
            _x = "50000";
            _y = "50000";
            _altitude = "5000";
            _timestamp = "20181111111111111";
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightList,out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_tag, tag);
        }

        [Test]
        public void Flight_SetX_ReturnsX()
        {
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightList, out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_x, _testPosition.XCoor);
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightList, out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_y, _testPosition.YCoor);
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightList, out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_altitude, _testPosition.Altitude);
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightList, out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_timestamp, _testTimestamp.UnformattedTimestamp);
        }
    }
}
