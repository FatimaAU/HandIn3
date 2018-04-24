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
    class IT7_Course
    {
        private ICourse _uut;
        private IPosition _position;
        private IListHandler _listHandler;
        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _distance;
        private List<ITrackObject> _newTracks;

        [SetUp]
        public void Setup()
        {
            _uut = new Course();
            _position = Substitute.For<IPosition>();
            _newTracks = new List<ITrackObject>();
            _velocity = Substitute.For<IVelocity>();
            _course = Substitute.For<ICourse>();
            _separation = Substitute.For<ISeparation>();
            _distance = Substitute.For<IDistance>();
            _listHandler = new ListHandler(_velocity, _course, _separation, _distance);
        }

        //public void InitiateNewList()
        //{
        //    ITrackObject trackOne = Substitute.For<ITrackObject>();
        //    trackOne.Tag.Returns("HDJ232");
        //    trackOne.Position.XCoor.Returns(45201);
        //    trackOne.Position.YCoor.Returns(78452);
        //    trackOne.Position.Altitude.Returns(4500);
        //    trackOne.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");
        //    trackOne.Velocity.Returns(150);
        //    trackOne.Course.Returns(90);
        //    trackOne.ToString()
        //        .Returns
        //        ("Tag:\t\t" + "HDJ232" + "\n" +
        //         "X coordinate:\t" + 45201 + " meters \n" +
        //         "Y coordinate:\t" + 78452 + " meters\n" +
        //         "Altitide:\t" + 4500 + " meters\n" +
        //         "Timestamp:\t" + "April 23rd, 2018, at 23:55:32 and 339 milliseconds" + "\n" +
        //         "Velocity:\t" + 150 + " m/s\n" +
        //         "Course:\t\t" + 90 + " degrees\n");

        //    ITrackObject trackTwo = Substitute.For<ITrackObject>();
        //    trackTwo.Tag.Returns("DSO458");
        //    trackTwo.Position.XCoor.Returns(45300);
        //    trackTwo.Position.YCoor.Returns(78450);
        //    trackTwo.Position.Altitude.Returns(7895);
        //    trackTwo.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");
        //    trackTwo.Velocity.Returns(300);
        //    trackTwo.Course.Returns(300);

        //    trackTwo.ToString()
        //        .Returns
        //        ("Tag:\t\t" + "DSO0458" + "\n" +
        //         "X coordinate:\t" + 45300 + " meters \n" +
        //         "Y coordinate:\t" + 78450 + " meters\n" +
        //         "Altitide:\t" + 7895 + " meters\n" +
        //         "Timestamp:\t" + "April 23rd, 2018, at 23:55:32 and 339 milliseconds" + "\n" +
        //         "Velocity:\t" + 300 + " m/s\n" +
        //         "Course:\t\t" + 300 + " degrees\n");

        //    _newTracks.Add(trackOne);
        //    _newTracks.Add(trackTwo);
        //}

        [Test]
        public void CurrentCourse_()
        {
            _newTracks.Add(Substitute.For<ITrackObject>());
            ITrackObject trackOne = Substitute.For<ITrackObject>();

            trackOne.Tag.Returns("123ABC");
            _newTracks[0].Position.XCoor.Returns(50000);
            _newTracks[0].Position.YCoor.Returns(50000);
            _newTracks[0].Position.XCoor.Returns(10000);

            _listHandler.Renew(_newTracks);
            _listHandler.Update(_newTracks);

            _uut.CurrentCourse(_newTracks[0].Position, _listHandler.CurrentTracks[0].Position, _distance);

            var temp = _newTracks[0].Position.Received().XCoor;
        }
    }
}
