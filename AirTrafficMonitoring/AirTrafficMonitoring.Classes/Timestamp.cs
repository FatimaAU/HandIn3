using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class Timestamp : ITimestamp
    {
        public string UnformattedTimestamp { get; set; }
        //public void Timestamp(string time)
        //{
        //    FormattedTimestamp = time;
        //}
    }
}
