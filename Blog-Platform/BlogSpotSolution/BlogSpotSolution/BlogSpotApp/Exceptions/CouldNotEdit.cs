using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{

    public class CouldNotEdit : Exception
    {
        string message;
        public CouldNotEdit()
        {
            message = "This Blog Could Not Be Deleted";
        }
        public override string Message => message;
    }
}