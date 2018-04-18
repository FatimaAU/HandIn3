using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class FlightDataHandler : IFlightDataHandler
    {
        private IPosition _position;
        private ITimestampFormatter _formatter;

        public FlightDataHandler(
            IPosition pos, 
            ITimestampFormatter formatter)
        {
            _position = pos;
            _formatter = formatter;
        }

        public void Distribute(List<string> data, out string tag)
        {
            tag = data[0];
            var x = int.Parse(data[1]);
            var y = int.Parse(data[2]);
            var al = int.Parse(data[3]);
            var timestamp = data[4];

            _position.SetPosition(x, y, al);
            _formatter.Unformatted = timestamp;

        }
    }
}
