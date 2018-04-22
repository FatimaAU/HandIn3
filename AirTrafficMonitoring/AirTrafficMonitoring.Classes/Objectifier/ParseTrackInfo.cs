using System.Collections.Generic;
using System.Linq;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class ParseTrackInfo : IParseTrackInfo
    {
        public List<string> Parse(string data)
        {
            return data.Split(';').ToList();
        }
    }
}
