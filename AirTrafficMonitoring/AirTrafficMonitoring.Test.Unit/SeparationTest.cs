using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes;
using AirTrafficMonitoring.Classes.Interfaces;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class SeparationTest
    {
        private Separation _separation;
        private IDistance _distance;

        private ITrackObject _oldObj;
        private ITrackObject _newObj;
        private IPosition _oldPos;
        private IPosition _newPos;

        [SetUp]
        public void Setup()
        {
            _separation = new Separation();
            _distance = new Distance();

            _oldPos = new Position();
            _newPos = new Position();

            _oldObj = new TrackObject("TAGGGG", _oldPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
            _newObj = new TrackObject("TAGGGG", _newPos, "", new DateTime(2018, 04, 19, 15, 28, 30, 700));
        }

        [TestCase(10000, 20000, 5000, 11000, 21000, 5200, true)]
        [TestCase(10000, 20000, 5000, 10000, 20000, 15000, false)]
        [TestCase(10000, 20000, 2700, 10000, 70000, 2700, false)]
        [TestCase(10000, 20000, 5000, 80000, 20000, 5000, false)]
        [TestCase(10000, 20000, 5000, 12500, 22500, 5300, true)]
        public void Distance_DistanceThreeDim_ReturnsResult(int t1X, int t1Y, int t1Alt, int t2X, int t2Y,
            int t2Alt, bool result)
        {
            _oldPos.SetPosition(t1X, t1Y, t1Alt);
            _newPos.SetPosition(t2X, t2Y, t2Alt);

            Assert.AreEqual(result, _separation.IsConflicting(_oldObj, _newObj, _distance));
        }
    }
}
