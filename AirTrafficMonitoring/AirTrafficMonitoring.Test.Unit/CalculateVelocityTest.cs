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
    class CalculateVelocityTest
    {
        private CalculateVelocity _testCalculateVelocity;

        [SetUp]
        public void Setup()
        {
            _testCalculateVelocity = new CalculateVelocity();
        }

        [TestCase(10, 20, 10, 20, 14)]
        [TestCase(10, -20, -10, 20, 42)]
        [TestCase(-10, -20, -10, -20, 14)]
        [TestCase(33322, 33241, 20000, 20341, 350)]
        [TestCase(0, 0, 0, 0, 0)]

        public void CalculateVelocity_CalculateVelocity_ReturnsVelocity(int oldX, int newX, int oldY, int newY, int result)
        {
            Assert.AreEqual(_testCalculateVelocity.Velocity(oldX, newX, oldY, newY), result);
        }
    }
}
