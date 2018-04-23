using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class VelocityTest
    {
        private IVelocity _testCalculateVelocity;

        private ITrackObject _oldObj;
        private ITrackObject _newObj;

        private IDistance _distance;

        [SetUp]
        public void Setup()
        {
            _testCalculateVelocity = new Velocity();

            _distance = new Distance();
            /****************************
            // THIS MUST BE SUB - NEED FIX - ELSE OK
             *****************************/

            _oldObj = Substitute.For<ITrackObject>();
            _newObj = Substitute.For<ITrackObject>();

        }

        [TestCase(20000, 50000, 20400, 55000, 
            2018, 04, 19, 15, 29, 30, 100, 
            2018, 04, 19, 15, 29, 30, 800, 
            7165)]
        [TestCase(0, 50000, 0, 55000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 15, 29, 30, 800,
            2941)]
        [TestCase(50000, 50000, 0, 55000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 15, 30, 29, 800,
            827)]
        [TestCase(89500, 0, 0, 90000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 16, 29, 29, 800,
            35)]
        public void CalculateVelocity_Velocity_ReturnsVelocity(
            int xOld, int yOld, int xNew, int yNew, 
            int yearOld, int monthOld, int dayOld, int hoursOld, int minutesOld, int secondsOld, int milliOld,
            int yearNew, int monthNew, int dayNew, int hoursNew, int minutesNew, int secondsNew, int milliNew,
            double velocity)
        {
            _oldObj.Position.XCoor.Returns(xOld);
            _oldObj.Position.YCoor.Returns(yOld);
            _oldObj.Position.Altitude.Returns(500);

            _newObj.Position.XCoor.Returns(xNew);
            _newObj.Position.YCoor.Returns(yNew);
            _newObj.Position.Altitude.Returns(500);

            DateTime oldTimestamp = new DateTime(yearOld, monthOld, dayOld, hoursOld, minutesOld, secondsOld, milliOld);
            DateTime newTimestamp = new DateTime(yearNew, monthNew, dayNew, hoursNew, minutesNew, secondsNew, milliNew);

            _oldObj.InDateTime = oldTimestamp;
            _newObj.InDateTime = newTimestamp;

            Assert.AreEqual(velocity, _testCalculateVelocity.CurrentVelocity(_newObj, _oldObj, _distance));
        }
    }
}
