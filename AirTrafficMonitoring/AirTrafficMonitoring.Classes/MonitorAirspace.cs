//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AirTrafficMonitoring.Classes.Interfaces;
//using TransponderReceiver;

//namespace AirTrafficMonitoring.Classes
//{
//    public class MonitorAirspace
//    {

//  ------ UDVIND DENNE KLASSE NÅR DET HELE ER OPDELT KORREKT. DENNE SKAL JO IKKE HAVE ALT TILDELT SOM MAIN HAR LIGE NU ------
//        private readonly ITransponderReceiver _receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

//        public MonitorAirspace(IMonitoredArea MonitoredArea, IParseTrackInfo ParseTrack, IPosition Position, I)
//        {
//            _receiver.TransponderDataReady += OnTransponderDataReceived;

//        }

//        private void OnTransponderDataReceived(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
