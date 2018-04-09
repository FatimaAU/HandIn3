using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
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

        private IPosition _position;
        private ITimestamp _timestampObj;
        private IFlightData _flightData;

        [SetUp]
        public void Setup()
        {
            _position = new Position();
            _timestampObj = new Timestamp();
            _flightData = new FlightData();
            _flightList = new List<string>{ "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _tag = _flightList[0];
            _x = _flightList[1];
            _y = _flightList[2];
            _altitude = _flightList[3];
            _timestamp = _flightList[4];
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            _flightData.ExtractFlight(_flightList,out var tag, ref _position, ref _timestampObj);
            Assert.AreEqual(_tag, tag);
        }

        [Test]
        public void Flight_SetX_ReturnsX()
        {
            _flightData.ExtractFlight(_flightList, out string tag, ref _position, ref _timestampObj);
            Assert.AreEqual(_x, _position.XCoor);
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            _flightData.ExtractFlight(_flightList, out string tag, ref _position, ref _timestampObj);
            Assert.AreEqual(_y, _position.YCoor);
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            _flightData.ExtractFlight(_flightList, out string tag, ref _position, ref _timestampObj);
            Assert.AreEqual(_altitude, _position.Altitude);
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            _flightData.ExtractFlight(_flightList, out string tag, ref _position, ref _timestampObj);
            Assert.AreEqual(_timestamp, _timestampObj.UnformattedTimestamp);
        }
    }
}
