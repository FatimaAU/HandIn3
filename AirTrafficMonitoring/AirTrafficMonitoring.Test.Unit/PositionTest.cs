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
    class PositionTest
    {

        [Test]
        public void Timestamp_SetXCoordinate_ReturnsXCoordinate()
        {
            string x = "20000";
            Position _Position = new Position();
            _Position.SetPosition(x, "50000", "5000");
            Assert.AreEqual(x,_Position.XCoor);
        }

        [Test]
        public void Timestamp_SetYCoordinate_ReturnsYCoordinate()
        {
            string y = "20000";
            Position _Position = new Position();
            _Position.SetPosition("50000", y, "5000");
            Assert.AreEqual(y, _Position.YCoor);
        }

        [Test]
        public void Timestamp_SetAltitude_ReturnsAltitude()
        {
            string altitude = "2000";
            Position _Position = new Position();
            _Position.SetPosition("50000", "50000", altitude);
            Assert.AreEqual(altitude, _Position.Altitude);
        }
    }
}
