using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public interface IOutput
    {
        void Print(Track track);
    }
}
