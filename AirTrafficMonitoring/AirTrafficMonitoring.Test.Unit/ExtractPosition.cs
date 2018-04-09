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
    class ExtractPosition
    {
        private List<string> _flightPosition;

        [SetUp]
        public void Setup()
        {
            _flightPosition = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
        }

        [Test]
        public void ExtractTimestamp_StringListInserted_ReturnsTimestamp()
        {
            ExtractPosition _extractPosition = new ExtractPosition();
            Assert.AreEqual(_flightPosition, _extractPosition.);
        }
    }
}
