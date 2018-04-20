using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class CalculateVelocityTest
    {
        private CalculateVelocity _testCalculateVelocity;

        private ITrackObject _oldObj;
        private ITrackObject _newObj;
        private IPosition _oldPos;
        private IPosition _newPos;

        private IDistance _distance;

        [SetUp]
        public void Setup()
        {
            _oldPos = new Position();
            _newPos = new Position();
            _distance = new Distance();

            _oldObj = new TrackObject("TAGGGG", _oldPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
            _newObj = new TrackObject("TAGGGG", _newPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));

            _testCalculateVelocity = new CalculateVelocity();
        }

        [TestCase(20000, 50000, 20400, 55000, 
            2018, 04, 19, 15, 29, 30, 100, 
            2018, 04, 19, 15, 29, 30, 800, 
            7165.67)]
        [TestCase(0, 50000, 0, 55000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 15, 29, 30, 800,
            2941.18)]
        [TestCase(50000, 50000, 0, 55000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 15, 30, 29, 800,
            827.83)]
        [TestCase(89500, 0, 0, 90000,
            2018, 04, 19, 15, 29, 29, 100,
            2018, 04, 19, 16, 29, 29, 800,
            35.25)]
        public void CalculateVelocity_Velocity_ReturnsVelocity(
            int xOld, int yOld, int xNew, int yNew, 
            int yearOld, int monthOld, int dayOld, int hoursOld, int minutesOld, int secondsOld, int milliOld,
            int yearNew, int monthNew, int dayNew, int hoursNew, int minutesNew, int secondsNew, int milliNew,
            double velocity)
        {
            _oldObj.Position.SetPosition(xOld, yOld, 500);
            _newObj.Position.SetPosition(xNew, yNew, 500);

            DateTime oldTimestamp = new DateTime(yearOld, monthOld, dayOld, hoursOld, minutesOld, secondsOld, milliOld);
            DateTime newTimestamp = new DateTime(yearNew, monthNew, dayNew, hoursNew, minutesNew, secondsNew, milliNew);

            _oldObj.InDateTime = oldTimestamp;
            _newObj.InDateTime = newTimestamp;
            
            Assert.AreEqual(velocity, _testCalculateVelocity.Velocity(_newObj, _oldObj, _distance));
        }
    }
}
