using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{

    public class AlreadyLiked : Exception
    {
        string message;
        public AlreadyLiked()
        {
            message = "This Blog Could Not Be Deleted";
        }
        public override string Message => message;
    }
}