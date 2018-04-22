using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class Position : IPosition
    {
        public int XCoor { get; private set; }
        public int YCoor { get; private set; }
        public int Altitude { get; private set; }

        public void SetPosition(int x, int y, int alt)
        {
            XCoor = x;
            YCoor = y;
            Altitude = alt;
        }
    }
}
