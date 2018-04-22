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
    class CourseTest
    {
        private Course _uut;

        private IDistance _distance;
        private IPosition _oldPosition;
        private IPosition _newPosition;

        [SetUp]
        public void Setup()
        {
            /****************************
            // THIS MUST BE SUB - NEED FIX - ELSE OK
             *****************************/
            _uut = new Course();

            _distance = new Distance();

            _oldPosition = Substitute.For<IPosition>();
            _newPosition = Substitute.For<IPosition>();
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
            Assert.AreEqual(result, _uut.CalculateInDegrees(x, y));
        }

        [TestCase(10000, 20000, 10000, 20000, 45)]
        [TestCase(10000, -20000, -10000, 20000, 315)]
        [TestCase(-10000, -20000, -10000, -20000, 225)]
        [TestCase(33322, 33241, 20000, 20341, 346)]
        public void CalculateCourse_CalculateCourse_ReturnsCourse(int oldX, int newX, int oldY, int newY, int result)
        {
            _oldPosition.XCoor.Returns(oldX);
            _oldPosition.YCoor.Returns(oldY);
            _oldPosition.Altitude.Returns(3000);

            _newPosition.SetPosition(newX, newY, 400);

            _newPosition.XCoor.Returns(newX);
            _newPosition.YCoor.Returns(newY);
            _newPosition.Altitude.Returns(3000);

            Assert.AreEqual(result, _uut.CurrentCourse(_oldPosition, _newPosition, _distance));
        }
    }
}
