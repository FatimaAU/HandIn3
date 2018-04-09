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
    class ParseTrackInfoTest
    {
        private List<string> _flightList;
        private string _flightString;

        private IParseTrackInfo _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new ParseTrackInfo();

            _flightList = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _flightString = "TAGGGG;50000;50000;5000;20181111111111111";
        }

        [Test]
        public void ParseFlightInfo_StringConverts_ReturnsParsedOutput()
        {
            Assert.AreEqual(_flightList, _parser.Parse(_flightString));
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
            Assert.AreNotEqual(_flightList, _parser.Parse(_flightString));
        }

    }

}