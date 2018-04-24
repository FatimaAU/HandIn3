using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace AirTrafficMonitoring.Test.Unit
{
    class ATMSystemTest
    {
        private ATMSystem _uut;

        private ITrackList _objectifier;
        private IListHandler _listHandler;

        private ITrackObject _trackOne;
        private List<ITrackObject> _argList;
        private TrackListUpdatedArgs _args;

        [SetUp]
        public void Setup()
        {
            _trackOne = Substitute.For<ITrackObject>();

            _listHandler = Substitute.For<IListHandler>();
            _objectifier = Substitute.For<ITrackList>();
            
            _uut = new ATMSystem(_objectifier, _listHandler);

            _trackOne.Tag.Returns("HDJ232");
            _trackOne.Position.XCoor.Returns(45201);
            _trackOne.Position.YCoor.Returns(78452);
            _trackOne.Position.Altitude.Returns(4500);
            _trackOne.Timestamp.Returns("April 23rd, 2018, at 23:55:32 and 339 milliseconds");
            _trackOne.Velocity.Returns(150);
            _trackOne.Course.Returns(90);
            _trackOne.ToString()
                .Returns
                ("Tag:\t\t" + "HDJ232" + "\n" +
                 "X coordinate:\t" + 45201 + " meters \n" +
                 "Y coordinate:\t" + 78452 + " meters\n" +
                 "Altitide:\t" + 4500 + " meters\n" +
                 "Timestamp:\t" + "April 23rd, 2018, at 23:55:32 and 339 milliseconds" + "\n" +
                 "Velocity:\t" + 150 + " m/s\n" +
                 "Course:\t\t" + 90 + " degrees\n");

            _argList = new List<ITrackObject> { _trackOne };

            _args = new TrackListUpdatedArgs(_argList);
        }

        public void RaiseTrackEvent()
        {
            _objectifier.TrackListReady += Raise.EventWith(_args);
        }

        [Test]
        public void SystemATM_InitiateCalled_ReceivedCall()
        {
            RaiseTrackEvent();

            _listHandler.Received().Initiate(_argList);
        }

        [Test]
        public void SystemATM_InitiateCalledReturnsTrue_ReceivedCall()
        {
            _listHandler.Initiate(_argList).Returns(true);

            RaiseTrackEvent();

        }

        [Test]
        public void SystemATM_UpdateCalled_ReceivedCall()
        {
            RaiseTrackEvent();

            _listHandler.Received().Update(_argList);
        }

        [Test]
        public void SystemATM_ListhandlerCurrentSeperationEvents_ReceivedCall()
        {
            RaiseTrackEvent();

            _listHandler.Received().CurrentSeperationEvents();
        }

        [Test]
        public void SystemATM_ListhandlerRenew_ReceivedCall()
        {
            RaiseTrackEvent();

            _listHandler.Received().Renew(_argList);
        }
    }
}
