using BlogSpotApp.Models;

namespace BlogSpotApp.Interfaces
{
    public interface ICommentService
    {
        Comment? AddComment(Comment comment);
        Comment? EditComment(Comment comment);
        List<Comment>? GetCommentsById(int id);
        List<Comment>? GetCommentsByEmail(string email);
        Comment? DeleteComment(Comment comment);
        Comment? ReportComment(Comment comment);
        List<Comment>? ReportedComments();
        Comment? ApproveReportComment(int id);
    }
}
