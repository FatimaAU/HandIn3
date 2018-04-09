using System.Collections.Generic;
using System.Linq;

namespace AirTrafficMonitoring.Classes
{
    public class ParseTrackInfo
    {
        public List<string> Parse(string data)
        {
            return data.Split(';').ToList();
        }
    }
}
