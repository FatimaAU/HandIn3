using System.Collections.Generic;
using System.IO;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
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
            trackOne.Position.XCoor.Returns(45201);
            trackOne.Position.YCoor.Returns(78452);
            trackOne.Position.Altitude.Returns(4500);
            trackOne.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");
            trackOne.Velocity.Returns(150);
            trackOne.Course.Returns(90);
            trackOne.ToString()
                .Returns
                ("Tag:\t\t" + "HDJ232" + "\n" +
                 "X coordinate:\t" + 45201 + " meters \n" +
                 "Y coordinate:\t" + 78452 + " meters\n" +
                 "Altitide:\t" + 4500 + " meters\n" +
                 "Timestamp:\t" + "April 23rd, 2018, at 23:55:32 and 339 milliseconds" + "\n" +
                 "Velocity:\t" + 150 + " m/s\n" +
                 "Course:\t\t" + 90 + " degrees\n");

            ITrackObject trackTwo = Substitute.For<ITrackObject>();
            trackTwo.Tag.Returns("DSO458");
            trackTwo.Position.XCoor.Returns(45300);
            trackTwo.Position.YCoor.Returns(78450);
            trackTwo.Position.Altitude.Returns(7895);
            trackTwo.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");
            trackTwo.Velocity.Returns(300);
            trackTwo.Course.Returns(300);
            trackTwo.ToString()
                .Returns
                ("Tag:\t\t" + "DSO0458" + "\n" +
                 "X coordinate:\t" + 45300 + " meters \n" +
                 "Y coordinate:\t" + 78450 + " meters\n" +
                 "Altitide:\t" + 7895 + " meters\n" +
                 "Timestamp:\t" + "April 23rd, 2018, at 23:55:32 and 339 milliseconds" + "\n" +
                 "Velocity:\t" + 300 + " m/s\n" +
                 "Course:\t\t" + 300 + " degrees\n");

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

            _uut.CurrentSeperationEvents();

            Assert.AreEqual(expectedReturn, _uut.CurrentSeperationEvents());
        }

        [Test]
        public void ListHandler_CurrentSeperationEvents_EventsDetected()
        {
            string expectedReturn = "Current separation events:\n" +
                                    "HDJ232 and KDS328 at April 23rd, 2018, at 23:55:32 and 339 milliseconds\n";

            InitiateNewList();

            ITrackObject trackThree = Substitute.For<ITrackObject>();
            trackThree.Tag.Returns("KDS328");

            _newTracks.Add(trackThree);

            _uut.Initiate(_newTracks);

            _separation.IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[2], _distance).Returns(true);

            _uut.CurrentSeperationEvents();

            Assert.AreEqual(expectedReturn, _uut.CurrentSeperationEvents());
        }

        [Test]
        public void ListHandler_InsertedInLogSeparationEvent_ReceivedCorrect()
        {
            string expectedLog = "seperationLoggingTest\r\n";
            string filename = "test.txt";

            StreamWriter strm = File.CreateText(filename);
            strm.Flush();
            strm.Close();

            _uut.LogSeperationEvent("seperationLoggingTest", "test.txt");

            string contents = File.ReadAllText(filename);
            Assert.AreEqual(expectedLog, contents);
        }


        [Test]
        public void ListHandler_LogSeparationEvent_ReceivedCorrect()
        {
            string filename = "test.txt";

            string expectedOutput = "HDJ232 and DSO458 at April 23rd, 2018, at 23:55:32 and 339 milliseconds\r\n";

            StreamWriter strm = File.CreateText(filename);
            strm.Flush();
            strm.Close();

            InitiateNewList();

            _uut.Initiate(_newTracks);

            _separation.IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[1], _distance).Returns(true);

            _uut.CurrentSeperationEvents(filename);
            string contents = File.ReadAllText(filename);

            Assert.AreEqual(expectedOutput, contents);
        }

        [Test]
        public void ListHandler_LogTwoEvents_ReceivedEventTwoCorrect()
        {
            string filename = "test.txt";

            string expectedOutput = "HDJ232 and DSO458 at April 23rd, 2018, at 23:55:32 and 339 milliseconds\r\n" +
                "HDJ232 and UDS323 at April 23rd, 2018, at 23:55:32 and 339 milliseconds\r\n";

            StreamWriter strm = File.CreateText(filename);
            strm.Flush();
            strm.Close();

            InitiateNewList();

            ITrackObject trackThree = Substitute.For<ITrackObject>();
            trackThree.Tag.Returns("UDS323");

            _newTracks.Add(trackThree);
            _uut.Initiate(_newTracks);

            _separation.IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[1], _distance).Returns(true);
            _separation.IsConflicting(_uut.CurrentTracks[0], _uut.CurrentTracks[2], _distance).Returns(true);

            _uut.CurrentSeperationEvents(filename);
            string contents = File.ReadAllText(filename);

            Assert.AreEqual(expectedOutput, contents);
        }

        [Test]
        public void ListHandler_CurrentTracksEmpty_ReturnsEmptyList()
        {
            Assert.AreEqual(_uut.ToString(), "Current list is empty\n");
        }

        [Test]
        public void ListHandler_ToString_Returns()
        {
            string expectedReturn = "Current separation events:\nNo current events detected\n";
            InitiateNewList();
            _uut.Initiate(_newTracks);

            Assert.AreNotEqual(expectedReturn, _uut.ToString());
        }


    }
}
