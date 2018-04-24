using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;


namespace AirTrafficMonitoring.Test.Integration
{
    [TestFixture]
    class IT3_Course
    {
        private ICourse _uut;
        private IListHandler _listHandler;
        private IVelocity _velocity;
        private ISeparation _separation;
        private IDistance _distance;
        private List<ITrackObject> _newTracks;

        [SetUp]
        public void Setup()
        {
            _uut = new Course();
            _newTracks = new List<ITrackObject>();
            _velocity = Substitute.For<IVelocity>();
            _separation = Substitute.For<ISeparation>();
            _distance = Substitute.For<IDistance>();
            _listHandler = new ListHandler(_velocity, _uut, _separation, _distance);
        }

        [Test]
        public void CurrentCourse_PositionXCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.CurrentCourse(_newTracks[0].Position, _listHandler.CurrentTracks[0].Position, _distance);

            var temp = _newTracks[0].Position.Received().XCoor;
        }

        [Test]
        public void CurrentCourse_PositionYCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.CurrentCourse(_newTracks[0].Position, _listHandler.CurrentTracks[0].Position, _distance);

            var temp = _newTracks[0].Position.Received().YCoor;
        }
    }
}
