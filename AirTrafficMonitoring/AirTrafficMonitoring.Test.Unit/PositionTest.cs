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
//    /*
//    * UNIT TEST DESCRIPTION
//    * Unit tests on Position that test the correct
//    * position is returned when assigned
//    */
//    [TestFixture]
//    class PositionTest
//    {
//        private IPosition _position;

//        [SetUp]
//        public void Setup()
//        {
//            _position = new Position();
//        }

//        [Test]
//        public void Timestamp_SetXCoordinate_ReturnsXCoordinate()
//        {
//            string x = "20000";
//            _position.SetPosition(x, "50000", "5000");
//            Assert.AreEqual(x, _position.XCoor);
//        }

//        [Test]
//        public void Timestamp_SetYCoordinate_ReturnsYCoordinate()
//        {
//            string y = "20000";
//            _position.SetPosition("50000", y, "5000");
//            Assert.AreEqual(y, _position.YCoor);
//        }

//        [Test]
//        public void Timestamp_SetAltitude_ReturnsAltitude()
//        {
//            string altitude = "2000";
//            _position.SetPosition("50000", "50000", altitude);
//            Assert.AreEqual(altitude, _position.Altitude);
//        }
//    }
//}
