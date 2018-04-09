using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    public class FlightTrackingValidationTest
    {
        [Test]
        public void FlightTrackingValidation_XCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000,10000,20000,500);
            Assert.That(_monitoredArea.InsideMonitoredArea("90000","50000","5000"), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("90001", "50000", "5000"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("9999", "50000", "5000"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_XCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("10000", "50000", "5000"), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideUpperMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "90000", "5000"), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideUpperMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "90001", "5000"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateOutsideLowerMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "9999", "5000"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_YCoordinateInsideLowerMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "10000", "5000"), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideUpperMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "50000", "20000"), Is.EqualTo(true));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideUpperMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "50000", "20001"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeOutsideLowerMonitor_ReturnsFalse()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "50000", "499"), Is.EqualTo(false));
        }

        [Test]
        public void FlightTrackingValidation_AltitudeInsideLowerMonitor_ReturnsTrue()
        {
            MonitoredArea _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);
            Assert.That(_monitoredArea.InsideMonitoredArea("50000", "50000", "500"), Is.EqualTo(true));
        }

    }
}

