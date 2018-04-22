using System.Collections.Generic;

namespace AirTrafficMonitoring.Classes.Objectifier.Interfaces
{
    public interface IParseTrackInfo
    {
        List<string> Parse(string data);
    }
}
