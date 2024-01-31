using BlogSpotApp.Interfaces;
using BlogSpotApp.Models;

namespace BlogSpotApp.Services
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IRepository<int, UserFollower> _userFollowerRepository;

        public UserFollowerService(IRepository<int, UserFollower> userFollowerRepository)
        {
            _userFollowerRepository = userFollowerRepository;
        }



        public UserFollower? ToggleFollower(UserFollower userFollower)
        {
            var checkFollow = FollowStatus(userFollower);
            if (checkFollow == null)
            {
                _userFollowerRepository.Add(userFollower);
                return userFollower;
            }
            else if (checkFollow != null)
            {
                _userFollowerRepository.Delete(checkFollow.RelationId);
                return userFollower;
            }
            return null;
        }

        public UserFollower? FollowStatus(UserFollower userFollower)
        {
            var checkFollow = _userFollowerRepository.GetAll()?
                        .SingleOrDefault(f => f.FollowerId == userFollower.FollowerId && f.FollowingId == userFollower.FollowingId);
            if (checkFollow == null)
            {
                return null;
            }
            return checkFollow;

        }
        public List<UserFollower>? GetFollowers(string userEmail)
        {
            var checkFollow = _userFollowerRepository.GetAll()?.Where(f=>f.FollowingId==userEmail).ToList();
            if (checkFollow == null)
            {
                return new List<UserFollower>(); ;
            }
            return checkFollow;
        }

        public List<UserFollower>? GetFollowees(string userEmail)
        {
            var checkFollow = _userFollowerRepository.GetAll()?.Where(f => f.FollowerId == userEmail).ToList();
            if (checkFollow == null)
            {
                return new List<UserFollower>();
            }
            return checkFollow;
        }
    }
}
