using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.Objectifier
{
    public class Position : IPosition
    {
        public int XCoor { get; private set; }
        public int YCoor { get; private set; }
        public int Altitude { get; private set; }

        public Position(int x, int y, int alt)
        {
            SetPosition(x, y, alt);
        }

        public void SetPosition(int x, int y, int alt)
        {
            XCoor = x;
            YCoor = y;
            Altitude = alt;
        }
    }
}
