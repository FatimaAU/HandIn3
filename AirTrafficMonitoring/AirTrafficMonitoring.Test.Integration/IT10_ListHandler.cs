using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Integration
{
    class IT10_ListHandler
    {
        private ATMSystem ATM;

        private IMonitoredArea _monitoredArea;
        private TrackObjectifier _trackObjectifier;
        private IPosition _position;
        private ITransponderReceiver _transponderReceiver;
        private IParseTrackInfo _parseTrackInfo;
        private IFlightExtractor _flightExtractor;
        private ITimestampFormatter _timestampFormatter;
        private ISeparation _seperationEvent;

        private IListHandler _listHandler;

        private List<ITrackObject> _trackList;
        private List<string> _argList;
        private RawTransponderDataEventArgs _args;

        [SetUp]
        public void Setup()
        {
            _listHandler = Substitute.For<IListHandler>();
            _monitoredArea = new MonitoredArea(90000, 10000, 20000, 500);

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _flightExtractor = new FlightExtractor();
            _parseTrackInfo = new ParseTrackInfo();
            _timestampFormatter = new TimestampFormatter();

            _trackObjectifier = new TrackObjectifier(_transponderReceiver, _monitoredArea, _parseTrackInfo, _flightExtractor, _timestampFormatter);

            ATM = new ATMSystem(_trackObjectifier, _listHandler);

            _argList = new List<string>
            {
                "ATR423;39045;12932;14000;20151006213456789",
                "DSD323;40000;12930;15000;20151006213456789"
            };

            _args = new RawTransponderDataEventArgs(_argList);

            _trackObjectifier.TrackListReady += (sender, updatedArgs) =>
            {
                _trackList = updatedArgs.TrackList;
            };
        }

        [Test]
        public void Listhandler_RaiseEvent_ReceivedInitiate()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _listHandler.Received().Initiate(_trackList);
        }

        [Test]
        public void Listhandler_RaiseEvent_ReceivedUpdate()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _listHandler.Received().Update(_trackList);
        }

        [Test]
        public void Listhandler_RaiseEvent_ReceivedCurrentSeperationEvents()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _listHandler.Received().CurrentSeperationEvents();
        }

        [Test]
        public void Listhandler_RaiseEvent_ReceivedRenew()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_args);

            _listHandler.Received().Renew(_trackList);
        }
    }
}
