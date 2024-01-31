using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{
    public class BlogNotFoundException : Exception
    {
        string message;
        public BlogNotFoundException()
        {
            message = "No Such Blog Exists";
        }
        public override string Message => message;
    }
}