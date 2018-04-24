using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.Classes.Objectifier.Interfaces;
using AirTrafficMonitoring.Classes.TrackListReadyEvent;
using AirTrafficMonitoring.Classes.UpdateAndCheck.Interfaces;

namespace AirTrafficMonitoring.Classes
{
    public class ATMSystem
    {
        private readonly IListHandler ListHandler;
        public ATMSystem(ITrackList objectifier, IListHandler listHandler)
        {
            objectifier.TrackListReady += MonitorSystem;
            ListHandler = listHandler;
        }

        private void MonitorSystem(object sender, TrackListUpdatedArgs trackListUpdatedArgs)
        {
            List<ITrackObject> newTrackList = trackListUpdatedArgs.TrackList;

            if (ListHandler.Initiate(newTrackList))
                return;

            ListHandler.Update(newTrackList);

            Console.WriteLine(ListHandler);
            Console.WriteLine(ListHandler.CurrentSeperationEvents());

            ListHandler.Renew(newTrackList);
        }
    }
}
