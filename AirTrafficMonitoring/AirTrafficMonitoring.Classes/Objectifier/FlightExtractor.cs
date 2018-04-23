using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class FlightExtractor : IFlightExtractor
    {
        public string Tag { get; set; }
        public IPosition Position { get; set; }
        public string RawTimestamp { get; set; }

        public void Extract(List<string> data)
        {
            Tag = data[0];

            var x = int.Parse(data[1]);
            var y = int.Parse(data[2]);
            var al = int.Parse(data[3]);

            Position = new Position();
            Position.SetPosition(x, y, al);

            RawTimestamp = data[4];
        }
    }
}
