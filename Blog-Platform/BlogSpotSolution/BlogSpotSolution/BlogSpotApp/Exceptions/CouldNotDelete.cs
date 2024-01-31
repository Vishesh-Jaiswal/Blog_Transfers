using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{

    public class CouldNotDelete : Exception
    {
        string message;
        public CouldNotDelete()
        {
            message = "This Blog Could Not Be Deleted";
        }
        public override string Message => message;
    }
}