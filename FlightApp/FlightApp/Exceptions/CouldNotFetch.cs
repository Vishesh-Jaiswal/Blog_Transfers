using System.Runtime.Serialization;

namespace FlightApp.Exceptions
{

    public class CouldNotFetch : Exception
    {
        string message;
        public CouldNotFetch()
        {
            message = "Was unable to fetch flight";
        }
        public override string Message => message;
    }
}