using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;

namespace AirTrafficMonitoring.Classes.UpdateAndCheck
{
    public class ListHandler : IListHandler
    {
        public List<ITrackObject> CurrentTracks { get; }
        private IVelocity _velocity;
        private ICourse _course;
        private ISeparation _separation;
        private IDistance _distance;

        public ListHandler(
            IVelocity vel, 
            ICourse cou, 
            ISeparation separation, 
            IDistance distance)
        {
            CurrentTracks = new List<ITrackObject>();
            _velocity = vel;
            _course = cou;
            _separation = separation;
            _distance = distance;
        }
        public bool Initiate(List<ITrackObject> newList)
        {
            // Populate list if empty
            if (!CurrentTracks.Any()) // Returns false when empty
            {
                foreach (var track in newList)
                    CurrentTracks.Add(track);

                return true;
            }

            // Returns false if not empty (already populated list)
            return false;
        }
        public void Renew(List<ITrackObject> newList)
        {
            CurrentTracks.Clear();

            foreach (var track in newList)
                CurrentTracks.Add(track);
        }

        public void Update(List<ITrackObject> newList)
        {
            foreach (var newTrack in newList)
            {
                foreach (var currtrack in CurrentTracks)
                {
                    // Update when found and break immediately
                    if (currtrack.Tag == newTrack.Tag)
                    {
                        currtrack.Velocity = _velocity.CurrentVelocity(newTrack, currtrack, _distance);
                        currtrack.Course = _course.CurrentCourse(currtrack.Position, newTrack.Position, _distance);
                        break;
                    }
                }
            }
        }

        public string CurrentSeperationEvents(string filenameToLogTo = "separationlog.txt")
        {
            string title = "Current separation events:";

            StringBuilder info = new StringBuilder();

            for (int i = 0; i < CurrentTracks.Count; i++)
            {
                var trackOne = CurrentTracks[i];

                for (int j = i + 1; j < CurrentTracks.Count; j++)
                {
                    var trackTwo = CurrentTracks[j];

                    if (_separation.IsConflicting(trackOne, trackTwo, _distance))
                    {
                        string trackInfo =
                            $"{trackOne.Tag} and {trackTwo.Tag} at {trackOne.Timestamp}";

                        info.Append(trackInfo + "\n");

                        LogSeperationEvent(trackInfo, filenameToLogTo);
                    }
                }
            }

            if (info.Length == 0)
                return title + "\n" + "No current events detected\n";

            return title + "\n" + info;
        }

        public void LogSeperationEvent(string info, string filename)
        {
            using (StreamWriter file = File.AppendText(filename)) 
            {
                file.WriteLine($"{info}");
            }
        }

        public override string ToString()
        {
            if (CurrentTracks.Count == 0)
            {
                return "Current list is empty\n";
            }

            StringBuilder allTracks = new StringBuilder();

            foreach (var track in CurrentTracks)
                allTracks.Append(track + "\n");

            allTracks.Append($"Amount of flights currently being monitored: {CurrentTracks.Count}");

            return allTracks.ToString();
        }
    }
}
