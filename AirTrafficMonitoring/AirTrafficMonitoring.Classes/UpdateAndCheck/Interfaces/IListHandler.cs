using System.Collections.Generic;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;

namespace AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces
{
    public interface IListHandler
    {
        bool Initiate(List<ITrackObject> newList);
        void Update(List<ITrackObject> newList);
        void Renew(List<ITrackObject> newList);
        void PrintSeperationEvent(string filenameToLogTo = "separationlog.txt");
        void LogSeperationEvent(string info, string filename);
    }
}