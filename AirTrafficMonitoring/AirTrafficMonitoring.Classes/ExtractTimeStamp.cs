using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public class ExtractTimestamp
    {
        public string Timestamp(List<string> data)
        {
            return data[4];
        }
    }
}
