using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    public class MonitoredAreaTest
    {
        private Position _position;
        private MonitoredArea _monitoredArea;

        [SetUp]
        public void Setup()
        {
            _position = new Position();
            _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            _position.SetPosition("90000", "50000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            _position.SetPosition("90001", "50000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            _position.SetPosition("9999", "50000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            _position.SetPosition("10000", "50000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            _position.SetPosition("50000", "90000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            _position.SetPosition("50000", "90001", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            _position.SetPosition("50000", "9999", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            _position.SetPosition("50000", "10000", "5000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideUpperMonitor_ReturnsTrue()
        {
            _position.SetPosition("50000", "50000", "20000");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideUpperMonitor_ReturnsFalse()
        {
            _position.SetPosition("50000", "50000", "20001");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideLowerMonitor_ReturnsFalse()
        {
            _position.SetPosition("50000", "50000", "499");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideLowerMonitor_ReturnsTrue()
        {
            _position.SetPosition("50000", "50000", "500");
            Assert.That(_monitoredArea.InsideMonitoredArea(_position), Is.EqualTo(true));
        }
    }
}

