using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    class ExtractTimestamp
    {
        private string Timestamp { get; }
        public ExtractTimestamp(List<string> data)
        {
            Timestamp = data[4];
        }
    }
}
