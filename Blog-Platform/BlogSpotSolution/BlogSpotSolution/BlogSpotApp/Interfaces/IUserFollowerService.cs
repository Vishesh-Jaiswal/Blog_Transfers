using BlogSpotApp.Models;

namespace BlogSpotApp.Interfaces
{
    public interface IUserFollowerService
    {
        UserFollower? ToggleFollower(UserFollower userFollower);
        List<UserFollower>? GetFollowers(string userEmail);
        List<UserFollower>? GetFollowees(string userEmail);
        UserFollower? FollowStatus(UserFollower userFollower);
    }
}
