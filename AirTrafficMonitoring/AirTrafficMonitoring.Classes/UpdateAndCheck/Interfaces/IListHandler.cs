using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces
{
    public interface IListHandler
    {
        List<ITrackObject> CurrentTracks { get; }
        bool Initiate(List<ITrackObject> newList);
        void Update(List<ITrackObject> newList);
        void Renew(List<ITrackObject> newList);
        string CurrentSeperationEvents(string filenameToLogTo = "separationlog.txt");
        void LogSeperationEvent(string info, string filename);
        string ToString();
    }
}