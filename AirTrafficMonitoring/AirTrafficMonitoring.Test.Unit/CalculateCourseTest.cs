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
    class CalculateCourseTest
    {
        private CalculateCourse _testCalculateCourse;

        private IDistance _distance;
        private IPosition _oldPosition;
        private IPosition _newPosition;

        [SetUp]
        public void Setup()
        {
            _distance = new Distance();

            _oldPosition = new Position();
            _newPosition = new Position();

            _testCalculateCourse = new CalculateCourse();
        }

        [TestCase(0, 0, 90)]
        [TestCase(0, 500, 0)]
        [TestCase(-0, -300, 180)]
        [TestCase(40000, 0, 90)]
        [TestCase(700, 0, 90)]
        [TestCase(-10322, 0, 270)]
        [TestCase(500, 90000, 0.32)]
        [TestCase(-400, 123, 287.09)]
        [TestCase(456, -145, 107.64)]
        [TestCase(-789, -432, 241.3)]
        [TestCase(90000, 0, 90)]
        [TestCase(90000, 90000, 45)]
        [TestCase(90000, -90000, 135)]
        [TestCase(-90000, 90000, 315)]
        public void CalculateCourse_CalculateInRadians_ReturnsCorrect(int x, int y, double result)
        {
            Assert.AreEqual(result, _testCalculateCourse.CalculateInDegrees(x, y));
        }

        [TestCase(10000, 20000, 10000, 20000, 45)]
        [TestCase(10000, -20000, -10000, 20000, 315)]
        [TestCase(-10000, -20000, -10000, -20000, 225)]
        [TestCase(33322, 33241, 20000, 20341, 346)]
        public void CalculateCourse_CalculateCourse_ReturnsCourse(int oldX, int newX, int oldY, int newY, int result)
        {
            _oldPosition.SetPosition(oldX, oldY, 3000);
            _newPosition.SetPosition(newX, newY, 3000);
            
            Assert.AreEqual(result, _testCalculateCourse.Course(_oldPosition, _newPosition, _distance));
        }


    }
}
