using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class PositionFactory : IPositionFactory
    {
        public IPosition CreatePosition(int x, int y, int al)
        {
            return new Position(x, y, al);
        }
    }
}
