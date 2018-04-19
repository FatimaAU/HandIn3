//using System.Collections.Generic;
//using AirTrafficMonitoring.Classes;
//using AirTrafficMonitoring.Classes.Interfaces;
//using NSubstitute;
//using NUnit.Framework;

//namespace AirTrafficMonitoring.Test.Unit
//{
//    /*
//    * UNIT TEST DESCRIPTION
//    * Unit tests on TrackObject that test
//    * the correct information is received when assigned
//    */
//    [TestFixture]
//    class TrackObjectTest
//    {
//        private List<string> _flightList;
//        private string _tag;
//        private string _x;
//        private string _y;
//        private string _altitude;
//        private string _timestamp;

//        private IPosition _position;
//        private ITrackObject _trackObject;

//        [SetUp]
//        public void Setup()
//        {
//            _flightList = new List<string> { "TAGGGG", "50000", "40000", "5000", "20181111111111111" };

//            _tag = _flightList[0];
//            _x = _flightList[1];
//            _y = _flightList[2];
//            _altitude = _flightList[3];
//            _timestamp = _flightList[4];

//            _position = new Position();
//            _position.SetPosition(_x, _y, _altitude);
//            _trackObject = new TrackObject(_tag, _position, _timestamp);
//        }

//        [Test]
//        public void Track_SetTag_ReturnsTag()
//        {
//            Assert.AreEqual(_tag, _trackObject.Tag);
//        }

//        [Test]
//        public void Track_SetXCoordinate_ReturnsXCoordinate()
//        {
//            Assert.AreEqual(_x, _trackObject.XCoordinate);
//        }

//        [Test]
//        public void Track_SetYCoordinate_ReturnYCoordinate()
//        {
//            Assert.AreEqual(_y, _trackObject.YCoordinate);
//        }

//        [Test]
//        public void Track_SetAltitude_ReturnAltitude()
//        {
//            Assert.AreEqual(_altitude, _trackObject.Altitude);
//        }

//        [Test]
//        public void Track_SetTimestamp_ReturnTimestamp()
//        {
//            Assert.AreEqual(_timestamp, _trackObject.TimeStamp);
//        }
//    }
//}
