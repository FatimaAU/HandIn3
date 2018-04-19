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


        [SetUp]
        public void Setup()
        {
            _oldPos = new Position();
            //_oldPos.SetPosition(40000, 40000, 3000);

            _newPos = new Position();
            //_newPos.SetPosition(44000, 40000, 3000);

            _oldObj = new TrackObject("TAGGGG", _oldPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
            _newObj = new TrackObject("TAGGGG", _newPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 900));

            _testCalculateVelocity = new CalculateVelocity();
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, -20000, 0, 0, 20000)]
        [TestCase(0, -0, -12322, 0, 12322)]
        [TestCase(0, -0, -0, 32313, 32313)]
        [TestCase(31233, 23132, 80000, 0, 53980)]
        [TestCase(33322, 33241, 20000, 20341, 350)]
        [TestCase(0, 2000, 0, 0, 0)]
        public void CalculateVelocity_CalculateDistance_ReturnsResult(int xOld, int yOld, int xNew, int yNew, int result)
        {
            _oldPos.SetPosition(xOld, yOld, 3000);
            _newPos.SetPosition(xNew, yNew, 3000);

            Assert.AreEqual(result, _testCalculateVelocity.Distance(_newPos, _oldPos));
        }

        [TestCase(0, 0, 0)]
        [TestCase(20000, 0, 20000)]
        [TestCase(0, 50000, 50000)]
        [TestCase(-23333, 0, 23333)]
        [TestCase(0, -40303, 40303)]
        [TestCase(50003, 30223, 19780)]
        [TestCase(-10002, 31232, 21230)]
        [TestCase(23212, -90000, 66788)]
        [TestCase(-90000, -90000, 0)]
        [TestCase(-40000, -50000, 10000)]
        public void CalculateVelocity_DistanceOneDimension_ReturnsDistance(int first, int second, int result)
        {
            Assert.AreEqual(result, _testCalculateVelocity.Length(first, second));
        }
    }
}
