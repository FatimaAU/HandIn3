//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AirTrafficMonitoring.Classes;
//using NUnit.Framework;

//namespace AirTrafficMonitoring.Test.Unit
//{
//    [TestFixture]
//    class CalculateCourseTest
//    {
//        private CalculateCourse _testCalculateCourse;

//        [SetUp]
//        public void Setup()
//        {
//            _testCalculateCourse = new CalculateCourse();
//        }

//        [TestCase(10, 20, 10, 20, 45)]
//        [TestCase(10, -20, -10, 20, 45)]
//        [TestCase(-10, -20, -10, -20, 45)]
//        [TestCase(33322, 33241, 20000, 20341, 75)]
        

//        public void CalculateCourse_CalculateCourse_ReturnsCourse(int oldX, int newX, int oldY, int newY, int result)
//        {
//            Assert.AreEqual(_testCalculateCourse(oldX, newX, oldY, newY),result);
//        }

//        [Test]
//        public void CalculateCourse_DivideByZero_ExceptionThrown()
//        {
//            Assert.Throws<DivideByZeroException>(() => _testCalculateCourse.Course(0,0,0,0));
//        }
//    }
//}
