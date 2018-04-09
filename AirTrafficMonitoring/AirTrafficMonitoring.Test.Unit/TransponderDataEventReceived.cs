using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    class TransponderDataEventReceived
    {
        private ITransponderReceiver _transponderReceiver;
        private List<string> _argList;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _argList = new List<string> {"ATR423;39045;12932;14000;20151006213456789"};

        }
        
        [Test]
        public void TransponderDataReady_EventFired_EventReceived()
        {
            var args = new RawTransponderDataEventArgs(_argList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(args.TransponderData, Is.EqualTo(_argList));

        }


        [Test]
        public void TransponderDataReady_EventFired_EventDataNotEqualToTest()
        {
            List<string> testList = new List<string> { "ATR423;39045;12932;15000;20151006213456789" };
            var args = new RawTransponderDataEventArgs(_argList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.AreNotEqual(args.TransponderData, testList);

        }

    }
}
