using System.Runtime.Serialization;

namespace BlogSpotApp.Exceptions
{
    public class NoCommentsAvaiable : Exception
    {
        string message;
        public NoCommentsAvaiable()
        {
            message = "This Blog has no comments";
        }
        public override string Message => message;
    }
}