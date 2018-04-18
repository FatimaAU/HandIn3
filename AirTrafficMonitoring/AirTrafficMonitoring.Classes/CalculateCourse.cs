using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class CalculateCourse : ICalculateCourse
    {
        public int Course(int oldX, int newX, int oldY, int newY)
        {
            int x = Math.Abs(newX - oldX);
            int y = Math.Abs(newY - oldY);

            if (x == 0)
            {
                throw new DivideByZeroException();
            }

            return (int)((Math.Atan(y / x))*360/(2*Math.PI));
        }
    }
}
