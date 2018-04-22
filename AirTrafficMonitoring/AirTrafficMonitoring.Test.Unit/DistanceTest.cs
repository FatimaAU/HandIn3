using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    class DistanceTest
    {
        private IDistance _uut;

        private IPosition _oldPos;
        private IPosition _newPos;

        [SetUp]
        public void Setup()
        {
            _uut = new Distance();

            _oldPos = Substitute.For<IPosition>();
            _newPos = Substitute.For<IPosition>();
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
            Assert.AreEqual(result, _uut.Point(first, second));
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
            Assert.AreEqual(result, _uut.DistanceOneDim(first, second));
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
            _oldPos.XCoor.Returns(t1X);
            _oldPos.YCoor.Returns(t1Y);
            _oldPos.Altitude.Returns(3000);

            _newPos.XCoor.Returns(t2X);
            _newPos.YCoor.Returns(t2Y);
            _newPos.Altitude.Returns(3000);

            Assert.AreEqual(result, _uut.DistanceTwoDim(_newPos, _oldPos));
        }
    }
}
