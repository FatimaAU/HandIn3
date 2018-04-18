using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface IPosition
    {
        int XCoor { get; }
        int YCoor { get; }
        int Altitude { get; }
        void SetPosition(int x, int y, int alt);
    }
}
