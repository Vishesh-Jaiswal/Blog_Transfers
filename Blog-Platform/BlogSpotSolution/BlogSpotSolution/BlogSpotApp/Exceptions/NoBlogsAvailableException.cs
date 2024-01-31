using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{
    public class NoBlogsAvailableException : Exception
    {
        string message;
        public NoBlogsAvailableException()
        {
            message = "No Such Blogs Are Available";
        }
        public override string Message => message;
    }
}