using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TimestampTest
    {
        private List<string> _flightPosition;

        [SetUp]
        public void Setup()
        {
            _flightPosition = new List<string> { "TAGGGG", "50000", "50000", "5000" };
        }

        [Test]
        public void Timestamp_StringListInserted_ReturnsTimestamp()
        {
            _flightPosition.Add("20181111111111111");
            Position _testPosition = new Position();
            Timestamp _testTimestamp = new Timestamp();
            Flight _testFlight = new Flight();
            _testFlight.ExtractFlight(_flightPosition, out string tag, ref _testPosition, ref _testTimestamp);
            Assert.AreEqual(_flightPosition[4],_testTimestamp.UnformattedTimestamp);
        }
    }
}
