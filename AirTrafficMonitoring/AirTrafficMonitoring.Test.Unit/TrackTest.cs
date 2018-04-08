using System.Collections.Generic;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackTest
    {
        private List<string> _trackList;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            
            // For testing that correct string is returned
            _trackList = new List<string> { "TAGGGG", "50000", "50000", "5000", "20181111111111111" };
            _output = Substitute.For<IOutput>();
        }

        [Test]
        public void TrackTest_TagInserted_ReturnsCorrectOutput()
        {
            //_trackList[0] = "ERERER";
            _output.Received().Print(Arg.Is<Track>(str => str.Tag.Contains("223")));
        }


    }
}
