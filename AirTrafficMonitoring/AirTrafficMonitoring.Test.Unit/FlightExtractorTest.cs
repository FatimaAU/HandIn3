using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class FlightExtractorTest
    {
        /*
        * UNIT TEST DESCRIPTION
        * Unit tests on FlightDataHandler that test that the
        * flight data is extracted correctly from the string
        */
        private IFlightExtractor _uut;
        private IPositionFactory _positionFactory;
        private IPosition _position;

        private List<string> _flightList;

        [SetUp]
        public void Setup()
        {
            _uut = new FlightExtractor();

            _position = Substitute.For<IPosition>();
            _positionFactory = Substitute.For<IPositionFactory>();

            _position.XCoor.Returns(50000);
            _position.YCoor.Returns(50032);
            _position.Altitude.Returns(4000);

            //_positionFactory.CreatePosition(50000, 500032, 4000).Returns(_position);

            _flightList = new List<string> { "TAGGGG", "50000", "50032", "4000", "20181111111111111" };

            _uut.Extract(_flightList, _positionFactory);
        }

        [Test]
        public void Flight_SetTag_ReturnsTag()
        {
            // Define tag and check correct tag returned
            string expectedTag = "TAGGGG";

            Assert.AreEqual(expectedTag, _uut.Tag);
        }


        [Test]
        public void Flight_SetX_ReturnsX()
        {
            _positionFactory.Received().CreatePosition(50000, 50032, 4000);
            _uut.Position.XCoor.Returns(5000);
        }

        [Test]
        public void Flight_SetY_ReturnsY()
        {
            _positionFactory.Received().CreatePosition(50000, 50032, 4000);
            _uut.Position.YCoor.Returns(50032);
        }

        [Test]
        public void Flight_SetAltitude_ReturnsAltitude()
        {
            _positionFactory.Received().CreatePosition(50000, 50032, 4000);
            _uut.Position.Altitude.Returns(4000);
        }

        [Test]
        public void Flight_SetTime_ReturnsTime()
        {
            // Define timestamp and check correct timestamp returned
            string expectedTimestamp = "20181111111111111";

            Assert.That(expectedTimestamp, Is.EqualTo(_uut.RawTimestamp));

        }
    }
}
