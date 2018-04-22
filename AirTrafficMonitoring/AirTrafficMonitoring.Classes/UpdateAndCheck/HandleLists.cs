using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AirTrafficMonitoring.Classes.Calculators.Interfaces;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class ListHandler : IListHandler
    {
        private List<ITrackObject> _currentTracks;
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
            _currentTracks = new List<ITrackObject>();
            _velocity = vel;
            _course = cou;
            _separation = separation;
            _distance = distance;
        }
        public bool Initiate(List<ITrackObject> newList)
        {
            if (!_currentTracks.Any())
            {
                foreach (var track in newList)
                    _currentTracks.Add(track);

                return true;
            }

            return false;
        }

        public void Update(List<ITrackObject> newList)
        {
            foreach (var newTrack in newList)
            {
                foreach (var currtrack in _currentTracks)
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

        public void Renew(List<ITrackObject> newList)
        {
            _currentTracks.Clear();

            foreach (var track in newList)
            {
                _currentTracks.Add(track);
            }
        }


        public void PrintSeperationEvent(string filenameToLogTo = "separationlog.txt")
        {
            Console.WriteLine("Current separation events:");

            for (int i = 0; i < _currentTracks.Count; i++)
            {
                var trackOne = _currentTracks[i];

                for (int j = i + 1; j < _currentTracks.Count; j++)
                {
                    var trackTwo = _currentTracks[j];

                    if (_separation.IsConflicting(trackOne, trackTwo, _distance))
                    {
                        string info =
                            $"{trackOne.Tag} and {trackTwo.Tag} at {trackOne.Timestamp}";

                        LogSeperationEvent(info, filenameToLogTo);
                    }

                }
            }
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
            if (_currentTracks.Count == 0)
            {
                return "Current list is empty\n";
            }

            StringBuilder allTracks = new StringBuilder();

            foreach (var track in _currentTracks)
                allTracks.Append(track + "\n");

            allTracks.Append($"Amount of flights currently being monitored: {_currentTracks.Count}");

            return allTracks.ToString();
        }
    }
}
