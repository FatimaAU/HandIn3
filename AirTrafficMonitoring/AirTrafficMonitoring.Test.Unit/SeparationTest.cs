using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Calculators;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class SeparationTest
    {
        private ISeparation _uut;

        private IDistance _distance;
        private ITrackObject _oldObj;
        private ITrackObject _newObj;

        [SetUp]
        public void Setup()
        {
            _uut = new Separation();

            /****************************
            // THIS MUST BE SUB - NEED FIX - ELSE OK
             *****************************/
            _distance = new Distance();

            _oldObj = Substitute.For<ITrackObject>();
            _newObj = Substitute.For<ITrackObject>();
        }

        [TestCase(10000, 20000, 5000, 11000, 21000, 5200, true)]
        [TestCase(10000, 20000, 5000, 10000, 20000, 15000, false)]
        [TestCase(10000, 20000, 2700, 10000, 70000, 2700, false)]
        [TestCase(10000, 20000, 5000, 80000, 20000, 5000, false)]
        [TestCase(10000, 20000, 5000, 12500, 22500, 5300, true)]
        public void Distance_DistanceThreeDim_ReturnsResult(int t1X, int t1Y, int t1Alt, int t2X, int t2Y,
            int t2Alt, bool result)
        {
            _oldObj.Position.XCoor.Returns(t1X);
            _oldObj.Position.YCoor.Returns(t1Y);
            _oldObj.Position.Altitude.Returns(t1Alt);

            _newObj.Position.XCoor.Returns(t2X);
            _newObj.Position.YCoor.Returns(t2Y);
            _newObj.Position.Altitude.Returns(t2Alt);

            Assert.AreEqual(result, _uut.IsConflicting(_oldObj, _newObj, _distance));
        }
    }
}
