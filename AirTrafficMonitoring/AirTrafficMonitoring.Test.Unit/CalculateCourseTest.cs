using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class CalculateCourseTest
    {
        [TestCase("10","20","10","20")]
        [TestCase("10", "-20", "-10", "20")]
        [TestCase("-10", "-20", "-10", "-20")]
        [TestCase("-10", "20", "10", "-20")]
        public void CalculateCourse_CalculateCourse_ReturnsCourse(string oldX, string newX, string oldY, string newY)
        {
            double _testX = Math.Abs(int.Parse(newX) - int.Parse(oldX));
            double _testY = Math.Abs(int.Parse(newY) - int.Parse(oldY));

            double _test = Math.Atan(_testY/_testX);
            Assert.AreEqual(_test,new CalculateCourse().Course(oldX,newX,oldY,newY));
        }
    }
}
