using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class ParseTrackInfo : IParseTrackInfo
    {
        public List<string> Parse(string data)
        {
            return data.Split(';').ToList();
        }
    }
}
