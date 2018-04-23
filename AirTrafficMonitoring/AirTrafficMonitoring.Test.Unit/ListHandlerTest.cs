using System;
using System.Collections.Generic;
using System.IO;
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

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class ListHandlerTest
    {
        private List<ITrackObject> _currentTracks;
        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _distance;
        private IPosition _position;
        private DateTime _inDateTime;
        private ListHandler _uut;

        [SetUp]
        public void Setup()
        {
            _currentTracks = Substitute.For<List<ITrackObject>>();
            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _separation = Substitute.For<ISeparation>();
            _distance = Substitute.For<IDistance>();
            _position = Substitute.For<IPosition>();
            _inDateTime = new DateTime();

            _uut = new ListHandler(_velocity,_course,_separation,_distance);
            
        }

        [Test]
        public void ListHandler_CurrentTracks_InitiateReturnsfalse()
        {
            TrackObject _TrackObject = new TrackObject("BBB111", _position, "20181111111111111", _inDateTime);

            _currentTracks.Add(_TrackObject);

            _uut.Renew(_currentTracks);

            Assert.AreEqual(_uut.Initiate(_currentTracks),false);
        }

        [Test]
        public void ListHandler_NoCurrentTracks_InitiateReturnstrue()
        {
            Assert.AreEqual(_uut.Initiate(_currentTracks), true);
        }

        [Test]
        public void ListHandler_NoCurrentTracks_AddCurrentTracks()
        {
            ITrackObject _Track = Substitute.For<ITrackObject>();
            _currentTracks.Received().Add(_Track);
        }

        [Test]
        public void ListHandler_UpdateVelocity_ReceivedCorrect()
        {
            ITrackObject _currentTrack = Substitute.For<ITrackObject>();
            ITrackObject _oldTracks = Substitute.For<ITrackObject>();
           
            _uut.Update(_currentTracks);

            _velocity.Received().CurrentVelocity(_currentTrack, _oldTracks, _distance);
        }

        [Test]
        public void ListHandler_UpdateCourse_ReceivedCorrect()
        {
            IPosition _currentPosition = Substitute.For<IPosition>();
            IPosition _oldPosition = Substitute.For<IPosition>();

            _uut.Update(_currentTracks);

            _course.Received().CurrentCourse(_currentPosition, _oldPosition, _distance);
        }

        [Test]
        public void ListHandler_Renew_ReceivedCorrect()
        {
            TrackObject _TrackObject = new TrackObject("BBB111", _position, "20181111111111111", _inDateTime);

            _currentTracks.Add(_TrackObject);

            _uut.Renew(_currentTracks);

            _currentTracks.Received().Add(_TrackObject);
        }

        [Test]
        public void ListHandler_PrintSeparationEvent_ReceivedCorrect()
        {
           
        }

        [Test]
        public void ListHandler_LogSeparationEvent_ReceivedCorrect()
        {
            _uut.LogSeperationEvent("hej","du");
            Assert.AreEqual("hej", File.AppendText("du"));
        }

        [Test]
        public void ListHandler_CurrentTracksEmpty_ReturnsEmptyList()
        {
            Assert.AreEqual(_uut.ToString(), "Current list is empty\n");
        }

        [Test]
        public void ListHandler_ToString_Returns()
        {
            TrackObject _TrackObject = new TrackObject("BBB111", _position, "20181111111111111", _inDateTime);

            _currentTracks.Add(_TrackObject);

            _uut.Renew(_currentTracks);

            Assert.AreNotEqual(_uut, "Current list is empty\n");
        }

        
    }
}
