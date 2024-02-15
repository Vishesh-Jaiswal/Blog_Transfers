using System.Runtime.Serialization;

namespace FlightApp.Exceptions
{

    public class CouldNotEdit : Exception
    {
        string message;
        public CouldNotEdit()
        {
            message = "This Flight Could Not Be Deleted";
        }
        public override string Message => message;
    }
}