using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    interface IPosition
    {
        void SetPosition(string x, string y, string alt);
    }
}
