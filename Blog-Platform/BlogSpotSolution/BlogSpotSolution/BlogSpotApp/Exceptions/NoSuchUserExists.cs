using BlogSpotApp.Exceptions;
using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{
    public class NoSuchUserExists : Exception
    {
        string message;
        public NoSuchUserExists()
        {
            message = "No Such User Exists";
        }
        public override string Message => message;
    }
}