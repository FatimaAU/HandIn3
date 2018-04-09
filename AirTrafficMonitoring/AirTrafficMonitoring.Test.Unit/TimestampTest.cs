//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AirTrafficMonitoring.Classes;
//using AirTrafficMonitoring.Classes.Interfaces;
//using NUnit.Framework;

//namespace AirTrafficMonitoring.Test.Unit
//{
//    [TestFixture]
//    class TimestampTest
//    {
//        private IPosition _position;
//        private ITimestamp _timestamp;

//        [SetUp]
//        public void Setup()
//        {
//            _position = new Position();
//            _timestamp = new Timestamp();
//        }

//        [Test]
//        public void Timestamp_StringListInserted_ReturnsTimestamp()
//        {
//            _timestamp.UnformattedTimestamp = "20181111111111111";

//            Flight _testFlight = new Flight();
//            _testFlight.ExtractFlight(_flightPosition, out string tag, ref _testPosition, ref _testTimestamp);


//            Assert.AreEqual(_flightPosition[4],_testTimestamp.UnformattedTimestamp);
//        }
//    }
//}
