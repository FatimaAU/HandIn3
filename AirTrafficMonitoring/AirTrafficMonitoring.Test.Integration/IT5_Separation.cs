using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Calculators;
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
    class IT5_Separation
    {
        private IListHandler _listHandler;
        private IVelocity _velocity;
        private ICourse _course;
        private IDistance _distance;
        private ISeparation _uut;
        private List<ITrackObject> _newTracks;

        [SetUp]
        public void Setup()
        {
            _uut = new Separation();
            _newTracks = new List<ITrackObject>();
            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _distance = Substitute.For<IDistance>();
            _listHandler = new ListHandler(_velocity, _course, _uut, _distance);
        }

        [Test]
        public void IsConflicting_PositionXCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.IsConflicting(_newTracks[0], _listHandler.CurrentTracks[0], _distance);

            var temp = _newTracks[0].Position.Received().XCoor;
        }

        [Test]
        public void IsConflicting_PositionYCoor_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.IsConflicting(_newTracks[0], _listHandler.CurrentTracks[0], _distance);

            var temp = _newTracks[0].Position.Received().YCoor;
        }

        [Test]
        public void IsConflicting_PositionAltitude_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.IsConflicting(_newTracks[0], _listHandler.CurrentTracks[0], _distance);

            var temp = _newTracks[0].Position.Received().Altitude;
        }
    }
}
