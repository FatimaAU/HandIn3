//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AirTrafficMonitoring.Classes;
//using AirTrafficMonitoring.Classes.Calculators;
//using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
//using AirTrafficMonitoring.Classes.TrackListReadyEvent;
//using NSubstitute;
//using NUnit.Framework;

//namespace AirTrafficMonitoring.Test.Unit
//{
//    class ATMSystemTest
//    {
//        private ATMSystem _uut;

//        private ITrackList _objectifier;

//        [SetUp]
//        public void Setup()
//        {
//            _objectifier = Substitute.For<ITrackList>();
//            _uut = new ATMSystem();

//            _distance = new Distance();
//            /****************************
//            // THIS MUST BE SUB - NEED FIX - ELSE OK
//             *****************************/

//            _oldPosition = Substitute.For<IPosition>();
//            _newPosition = Substitute.For<IPosition>();
//        }
//    }
//}
