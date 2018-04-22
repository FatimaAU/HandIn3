using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    /*
     * UNIT TEST DESCRIPTION
     * Unit tests on MonitorArea that test the upper
     * and lower boundary of the monitored area 
     */
    [TestFixture]
    public class MonitoredAreaTest
    {
        private IMonitoredArea _uut;
        private IPosition _position;

        [SetUp]
        public void Setup()
        {
            _position = Substitute.For<IPosition>();
            _uut = new MonitoredArea(90000, 10000, 20000, 500);
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(90000);
            _position.YCoor.Returns(50000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(90001);
            _position.YCoor.Returns(50000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(9999);
            _position.YCoor.Returns(50000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(10000);
            _position.YCoor.Returns(50000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(90000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(90001);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(9999);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(10000);
            _position.Altitude.Returns(5000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideUpperMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(10000);
            _position.Altitude.Returns(20000);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideUpperMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(10000);
            _position.Altitude.Returns(20001);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideLowerMonitor_ReturnsFalse()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(10000);
            _position.Altitude.Returns(499);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideLowerMonitor_ReturnsTrue()
        {
            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(10000);
            _position.Altitude.Returns(500);

            Assert.That(_uut.InsideMonitoredArea(_position), Is.EqualTo(true));
        }
    }
}

