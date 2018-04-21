using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class HandleLists
    {
        public bool Initiate(ref List<ITrackObject> oldList, List<ITrackObject> newList)
        {
            if (oldList == null)
            {
                oldList = new List<ITrackObject>();

                foreach (var track in newList)
                    oldList.Add(track);

                return true;
            }

            return false;
        }

        public void Update(ref List<ITrackObject> oldList, List<ITrackObject> newList,
            ICalculateVelocity vel, ICalculateCourse cou, IDistance dist)
        {
            foreach (var data in newList)
            {
                foreach (var track in oldList)
                {
                    if (track.Tag == data.Tag)
                    {
                        track.Velocity = (int) vel.Velocity(data, track, dist);
                        track.Course = cou.Course(track.Position, data.Position, dist);
                        break;
                    }
                }
            }
        }

        public void PrintSeperationEvents(List<ITrackObject> oldList, List<ITrackObject> newList,
            ISeparation separation, IDistance distance)
        {
            for (int i = 0; i < oldList.Count; i++)
            {
                var trackOne = oldList[i];

                for (int j = i + 1; j < oldList.Count; j++)
                {
                    var trackTwo = oldList[j];

                    if (separation.IsConflicting(trackOne, trackTwo, distance))
                    {
                        string info =
                            $"{trackOne.Tag} and {trackTwo.Tag} at {trackOne.Timestamp}";

                        LogSeperationEvent(info);
                    }

                }
            }
        }

        public void LogSeperationEvent(string info)
        {
            using (StreamWriter file = File.AppendText("seperationlog.txt"))
            {
                file.WriteLine($"{info}");
            }
        }
    }
}
