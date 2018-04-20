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
    class DistanceTest
    {
        private Separation _separation;
        private IDistance _distance;

        private ITrackObject _oldObj;
        private ITrackObject _newObj;
        private IPosition _oldPos;
        private IPosition _newPos;

        [SetUp]
        public void Setup()
        {
            _separation = new Separation();
            _distance = new Distance();

            _oldPos = new Position();
            _newPos = new Position();

            _oldObj = new TrackObject("TAGGGG", _oldPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
            _newObj = new TrackObject("TAGGGG", _newPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
        }

        [TestCase(0, 0, 0)]
        [TestCase(19000, 0, -19000)]
        [TestCase(-30000, 0, 30000)]
        [TestCase(0, 44333, 44333)]
        [TestCase(0, -44332, -44332)]
        [TestCase(10322, 23433, 13111)]
        [TestCase(-30000, 90000, 120000)]
        [TestCase(10322, 23433, 13111)]
        [TestCase(-35000, -90000, -55000)]
        [TestCase(0, 90000, 90000)]
        [TestCase(90000, 0, -90000)]
        [TestCase(90000, 90000, 0)]
        [TestCase(90000, -90000, -180000)]
        [TestCase(-90000, 90000, 180000)]

        public void CalculateCourse_Point_ReturnsCorrect(int first, int second, int result)
        {
            Assert.AreEqual(result, _distance.Point(first, second));
        }

        [TestCase(0, 0, 0)]
        [TestCase(20000, 0, 20000)]
        [TestCase(0, 50000, 50000)]
        [TestCase(-23333, 0, 23333)]
        [TestCase(0, -40303, 40303)]
        [TestCase(50003, 30223, 19780)]
        [TestCase(-10002, 31232, 41234)]
        [TestCase(23212, -90000, 113212)]
        [TestCase(-90000, -90000, 0)]
        [TestCase(-40000, -50000, 10000)]
        public void CalculateVelocity_DistanceOneDim_ReturnsLength(int first, int second, int result)
        {
            Assert.AreEqual(result, _distance.DistanceOneDim(first, second));
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 32312, 0, 0, 32312)]
        [TestCase(0, -20000, 0, 0, 20000)]
        [TestCase(0, -0, -12322, 0, 12322)]
        [TestCase(0, -0, -0, 32313, 32313)]
        [TestCase(0, 0, 90000, 90000, 127279.22)]
        [TestCase(33322, 33241, 20000, 20341, 18544.15)]
        [TestCase(0, 20004, 0, 46250, 26246)]
        public void Distance_DistanceTwoDim_ReturnsResult(int t1X, int t1Y, int t2X, int t2Y, double result)
        {
            _oldPos.SetPosition(t1X, t1Y, 3000);
            _newPos.SetPosition(t2X, t2Y, 3000);

            Assert.AreEqual(result, _distance.DistanceTwoDim(_newPos, _oldPos));
        }

        [TestCase(10000,20000,5000,11000,21000,5200,true)]
        [TestCase(10000, 20000, 5000, 10000, 20000, 15000, false)]
        [TestCase(10000, 20000, 2700, 10000, 70000, 2700, false)]
        [TestCase(10000, 20000, 5000, 80000, 20000, 5000, false)]
        [TestCase(10000, 20000, 5000, 12500, 22500, 5300, true)]

        public void Distance_DistanceThreeDim_ReturnsResult(int t1X, int t1Y, int t1Alt, int t2X, int t2Y,
            int t2Alt, bool result)
        {
            _oldPos.SetPosition(t1X, t1Y, t1Alt);
            _newPos.SetPosition(t2X, t2Y, t2Alt);

            //double horizontalDistance = _uut.DistanceTwoDim(_newPos, _oldPos);
            //int verticalDistance = _uut.DistanceOneDim(t1Alt, t2Alt);

            Assert.AreEqual(result, _separation.IsConflicting(_oldObj,_newObj,_distance));
            
        }

        //[TestCase(0, 0, 0)]
        //[TestCase(19000, 0, -19000)]
        //[TestCase(-30000, 0, 30000)]
        //[TestCase(0, 44333, 44333)]
        //[TestCase(0, -44332, -44332)]
        //[TestCase(10322, 23433, 13111)]
        //[TestCase(-30000, 90000, 120000)]
        //[TestCase(10322, 23433, 13111)]
        //[TestCase(-35000, -90000, -55000)]
        //[TestCase(0, 90000, 90000)]
        //[TestCase(90000, 0, -90000)]
        //[TestCase(90000, 90000, 0)]
        //[TestCase(90000, -90000, -180000)]
        //[TestCase(-90000, 90000, 180000)]

        //public void CalculateCourse_Length_ReturnsCorrect(int first, int second, int result)
        //{
        //    Assert.AreEqual(result, _testCalculateCourse.Length(first, second));
        //}
    }
}
