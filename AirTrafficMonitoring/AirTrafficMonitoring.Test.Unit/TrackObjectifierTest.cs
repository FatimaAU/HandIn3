using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TrackObjectifierTest
    {
        private IMonitoredArea _monitoredArea;
        private IParseTrackInfo _parser;
        private IFlightDataHandler _flightHandler;
        private IPosition _position;
        private ITimestampFormatter _formatter;
        private List<ITrackObject> TrackList;

        [SetUp]
        public void Setup()
        {
            _monitoredArea = Substitute.For<IMonitoredArea>();
            _parser = Substitute.For<IParseTrackInfo>();
            _flightHandler = Substitute.For<IFlightDataHandler>();
            _position = Substitute.For<IPosition>();
            _formatter = Substitute.For<ITimestampFormatter>();
            TrackList = new List<ITrackObject>();
        }

        [Test]
        public void TrackObjectifier_()
        {

        }

    }
}
