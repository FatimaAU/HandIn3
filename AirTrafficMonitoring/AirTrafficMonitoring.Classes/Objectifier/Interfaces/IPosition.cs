namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface IPosition
    {
        int XCoor { get; }
        int YCoor { get; }
        int Altitude { get; }
        void SetPosition(int x, int y, int alt);
    }
}
