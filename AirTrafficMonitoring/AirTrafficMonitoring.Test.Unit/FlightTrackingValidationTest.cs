using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    public class FlightTrackingValidationTest
    {
        private IExtractPosition extractPosition;

    [SetUp]
        public void Setup()
        {
            extractPosition = Substitute.For<IExtractPosition>();
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("90000", "50000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000,10000,20000,500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("90001", "50000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("9999", "50000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("10000", "50000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "90000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "90001", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "9999", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "10000", "5000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideUpperMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "50000", "20000");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideUpperMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "50000", "20001");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideLowerMonitor_ReturnsFalse()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "50000", "499");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideLowerMonitor_ReturnsTrue()
        {
            Position _Position = new Position();
            _Position.SetPosition("50000", "50000", "500");
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea(_Position), Is.EqualTo(true));
        }

    }
}

