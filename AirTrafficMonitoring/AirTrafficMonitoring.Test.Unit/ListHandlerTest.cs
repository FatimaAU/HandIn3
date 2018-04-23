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
        private IListHandler _uut;

        private List<ITrackObject> _newTracks;

        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _distance;

        [SetUp]
        public void Setup()
        {
            _newTracks = new List<ITrackObject>();

            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _separation = Substitute.For<ISeparation>();
            _distance = Substitute.For<IDistance>();

            _uut = new ListHandler(_velocity,_course,_separation,_distance);
            
        }

        public void InitiateNewList()
        {
            ITrackObject trackOne = Substitute.For<ITrackObject>();
            trackOne.Tag.Returns("HDJ232");

            ITrackObject trackTwo = Substitute.For<ITrackObject>();
            trackTwo.Tag.Returns("HAJ232");

            _newTracks.Add(trackOne);
            _newTracks.Add(trackTwo);
        }

        [Test]
        public void ListHandler_NoCurrentTracks_InitiateReturnsTrue()
        {
            bool expectedReturn = true;

            Assert.AreEqual(expectedReturn, _uut.Initiate(_newTracks));
        }

        [Test]
        public void ListHandler_CurrentTracksAlreadyInitialized_InitiateReturnsFalse()
        {
            bool expectedReturn = false;

            InitiateNewList();
            _uut.Initiate(_newTracks);

            Assert.AreEqual(expectedReturn, _uut.Initiate(_newTracks));
        }

        [Test]
        public void ListHandler_Renew_ListIsEqualToNewList()
        {

            _newTracks.Add(Substitute.For<ITrackObject>());

            _uut.Renew(_newTracks);

            Assert.AreEqual(_newTracks, _uut.CurrentTracks);
        }

        [Test]
        public void ListHandler_DoesNotRenew_ListIsNotEqualToNewList()
        {
            _uut.Renew(_newTracks);

            _newTracks.Add(Substitute.For<ITrackObject>());

            Assert.AreNotEqual(_newTracks, _uut.CurrentTracks);
        }

        //[Test]
        //public void ListHandler_NoCurrentTracks_AddCurrentTracks()
        //{
        //    _newTracks.Add(Substitute.For<ITrackObject>());


        //    CurrentTracks.Received().Add(_Track);
        //}


        [Test]
        public void ListHandler_UpdateVelocity_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            ITrackObject trackOne = Substitute.For<ITrackObject>();
            trackOne.Tag.Returns("HDJ232");

            // Renew to update
            _uut.Renew(_newTracks);
            _uut.Update(_newTracks);

            _velocity.Received().CurrentVelocity(_newTracks[0], _uut.CurrentTracks[0], _distance);
        }

        [Test]
        public void ListHandler_UpdateVelocity_UpdatedCorrect()
        {
            InitiateNewList();

            _uut.Initiate(_newTracks);

            _velocity.CurrentVelocity(_newTracks[0], _uut.CurrentTracks[0], _distance).Returns(200);

            _uut.Update(_newTracks);

            Assert.That(_uut.CurrentTracks[0].Velocity, Is.EqualTo(200));
        }

        [Test]
        public void ListHandler_UpdateVelocity_NotEqualTagsNotUpdating()
        {
            InitiateNewList();

            _uut.Initiate(_newTracks);
            _uut.Update(_newTracks);

            _velocity.DidNotReceive().CurrentVelocity(_newTracks[0], _uut.CurrentTracks[1], _distance);
        }

        [Test]
        public void ListHandler_UpdateCourse_ReceivedCorrect()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());

            ITrackObject trackOne = Substitute.For<ITrackObject>();
            trackOne.Tag.Returns("HDJ232");

            // Renew to update
            _uut.Renew(_newTracks);
            _uut.Update(_newTracks);

            _course.Received().CurrentCourse(_newTracks[0].Position, _uut.CurrentTracks[0].Position, _distance);
        }

        [Test]
        public void ListHandler_UpdateCourse_NotEqualTagsNotUpdating()
        {
            InitiateNewList();

            _uut.Initiate(_newTracks);
            _uut.Update(_newTracks);

            _course.DidNotReceive().CurrentCourse(_newTracks[0].Position, _uut.CurrentTracks[1].Position, _distance);
        }

        [Test]
        public void ListHandler_UpdateCourse_UpdatedCorrect()
        {
            InitiateNewList();

            _uut.Initiate(_newTracks);

            _course.CurrentCourse(_newTracks[0].Position, _uut.CurrentTracks[0].Position, _distance).Returns(150);

            _uut.Update(_newTracks);

            Assert.That(_uut.CurrentTracks[0].Course, Is.EqualTo(150));
        }

        [Test]
        public void ListHandler_CurrentSeperationEvents_Received()
        {
            InitiateNewList();

            ITrackObject trackThree = Substitute.For<ITrackObject>();
            trackThree.Tag.Returns("HAJ232");

            _newTracks.Add(trackThree);

            _uut.Initiate(_newTracks);

            _uut.CurrentSeperationEvents();

            _separation.Received().IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[1], _distance);
        }

        [Test]
        public void ListHandler_CheckOnSameObject_DoesNotReceive()
        {
            InitiateNewList();

            _uut.Initiate(_newTracks);

            _uut.CurrentSeperationEvents();

            _separation.DidNotReceive().IsConflicting(_uut.CurrentTracks[1], _uut.CurrentTracks[1], _distance);
        }

        [Test]
        public void ListHandler_CurrentSeperationEvents_NoEventsDetected()
        {
            string expectedReturn = "Current separation events:\nNo current events detected\n";

            InitiateNewList();

            ITrackObject trackThree = Substitute.For<ITrackObject>();
            trackThree.Tag.Returns("HAJ232");

            _newTracks.Add(trackThree);

            _uut.Initiate(_newTracks);

            _separation.IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[1], _distance).Returns(false);

            Assert.AreEqual(expectedReturn, _uut.CurrentSeperationEvents());
        }

        [Test]
        public void ListHandler_CurrentSeperationEvents_EventsDetected()
        {
            string expectedReturn = "Current separation events:\nNo current events detected\n";

            InitiateNewList();

            ITrackObject trackThree = Substitute.For<ITrackObject>();
            trackThree.Tag.Returns("HAJ232");
            trackThree.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");

            _newTracks.Add(trackThree);

            _uut.Initiate(_newTracks);

            _separation.IsConflicting(_uut.CurrentTracks[2], _uut.CurrentTracks[1], _distance).Returns(true);

            Assert.AreEqual(expectedReturn, _uut.CurrentSeperationEvents());
        }


        //[Test]
        //public void ListHandler_LogSeparationEvent_ReceivedCorrect()
        //{
        //    _uut.LogSeperationEvent("hej","du");
        //    Assert.AreEqual("hej", File.AppendText("du"));
        //}

        //[Test]
        //public void ListHandler_CurrentTracksEmpty_ReturnsEmptyList()
        //{
        //    Assert.AreEqual(_uut.ToString(), "Current list is empty\n");
        //}

        //[Test]
        //public void ListHandler_ToString_Returns()
        //{
        //    TrackObject _TrackObject = new TrackObject("BBB111", _position, "20181111111111111", _inDateTime);

        //    CurrentTracks.Add(_TrackObject);

        //    _uut.Renew(CurrentTracks);

        //    Assert.AreNotEqual(_uut, "Current list is empty\n");
        //}


    }
}
