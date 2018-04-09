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
    class ParseFlightInfoTest
    {
        private List<string> _flightList;
        private string _flightString; 

        [SetUp]
        public void Setup()
        {
            _flightList = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _flightString = "TAGGGG;50000;50000;5000;20181111111111111";
        }

        [Test]
        public void ParseFlightInfo_StringConverts_ReturnsParsedOutput()
        {
            Assert.AreEqual(_flightList, ParseFlightInfo.Parse(_flightString));
        }

        [Test]
        public void ParseFlightInfo_StringDoesNotConverts_ReturnsFalseOutput()
        {
            Assert.AreNotEqual(_flightList, _flightString);
        }

        [Test]
        public void ParseFlightInfo_DelimitorNotCorrect_ReturnsFalseOutput()
        {
            _flightString = "TAGGGG,50000,50000,5000,20181111111111111";
            Assert.AreNotEqual(_flightList, ParseFlightInfo.Parse(_flightString));
        }

    }

}