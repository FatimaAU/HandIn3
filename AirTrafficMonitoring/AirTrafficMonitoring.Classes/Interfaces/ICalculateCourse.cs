namespace AirTrafficMonitoring.Classes.Interfaces
{
    public interface ICalculateCourse
    {
        int Course(IPosition oldTrack, IPosition newTrack, IDistance dist);
    }
}