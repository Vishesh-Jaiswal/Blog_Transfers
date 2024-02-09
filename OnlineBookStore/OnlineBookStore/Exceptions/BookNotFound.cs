using System.Runtime.Serialization;

namespace OnlineBookStore.Exceptions
{
    public class BookNotFound : Exception
    {
        string message;
        public BookNotFound()
        {
            message = "Blog Not Found";
        }
        public override string Message => message;
    }
}