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
            int x = newX - oldX;
            int y = newY - oldY;

            double inRad = Math.Atan2(y, x);

            return (int)(inRad*(180/Math.PI));
        }
    }
}
