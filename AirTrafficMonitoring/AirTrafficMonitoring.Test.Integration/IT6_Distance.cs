using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    class IT6_Distance
    {
        private IListHandler _listHandler;
        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _uut;
        private IDistance _distance;
        private List<ITrackObject> _newTracks;

        [SetUp]
        public void Setup()
        {
            _uut = new Distance();
            _distance = Substitute.For<IDistance>();
            _newTracks = new List<ITrackObject>();
            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _separation = Substitute.For<ISeparation>();
            _listHandler = new ListHandler(_velocity, _course, _separation, _distance);
        }

        [Test]
        public void DistanceTwoDim_PositionXCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.DistanceTwoDim(_newTracks[0].Position, _listHandler.CurrentTracks[0].Position);

            var temp = _newTracks[0].Position.Received().XCoor;
        }

        [Test]
        public void DistanceTwoDim_PositionYCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.DistanceTwoDim(_newTracks[0].Position, _listHandler.CurrentTracks[0].Position);

            var temp = _newTracks[0].Position.Received().YCoor;
        }

    }
}
