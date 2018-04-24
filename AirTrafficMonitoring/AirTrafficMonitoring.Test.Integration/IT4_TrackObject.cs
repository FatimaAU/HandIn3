using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    class IT4_TrackObject
    {
        private IListHandler _listHandler;
        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _distance;
        private ITrackObject _uut;
        private List<ITrackObject> _newTracks;
        private IPosition _position;
        private DateTime _inDateTime;

        [SetUp]
        public void Setup()
        {
            _position = Substitute.For<IPosition>();
            _inDateTime = new DateTime();
            _uut = new TrackObject("123ABC", _position, "20181111111111111", _inDateTime);
            _newTracks = new List<ITrackObject>();
            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _separation = Substitute.For<ISeparation>();
            _distance = Substitute.For<IDistance>();
            _listHandler = new ListHandler(_velocity, _course, _separation, _distance);
        }

        [Test]
        public void TrackObject_PositionXCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            Assert.AreEqual(_uut.Position.XCoor, _newTracks[0].Position.XCoor);
        }

        [Test]
        public void TrackObject_PositionYCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            Assert.AreEqual(_uut.Position.YCoor, _newTracks[0].Position.YCoor);
        }

        [Test]
        public void TrackObject_PositionAltitude_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            Assert.AreEqual(_uut.Position.Altitude, _newTracks[0].Position.Altitude);
        }
    }
}
