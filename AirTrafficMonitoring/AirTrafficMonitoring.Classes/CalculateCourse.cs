using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.Classes
{
    public class CalculateCourse
    {
        public double Course(string oldX, string newX, string oldY, string newY)
        {
            double x = Math.Abs(int.Parse(newX) - int.Parse(oldX));
            double y = Math.Abs(int.Parse(newY) - int.Parse(oldY));

            return Math.Atan(y / x);
        }
    }
}
