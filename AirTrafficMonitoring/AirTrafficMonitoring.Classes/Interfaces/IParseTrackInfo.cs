using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface IParseTrackInfo
    {
        List<string> Parse(string data);
    }
}
