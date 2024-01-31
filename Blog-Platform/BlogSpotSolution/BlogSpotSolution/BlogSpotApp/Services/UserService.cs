using BlogSpotApp.Interfaces;
using BlogSpotApp.Models.DTOs;
using BlogSpotApp.Models;
using System.Security.Cryptography;
using System.Text;
using BlogSpotApp.Exceptions;

namespace BlogSpotApp.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRepository<int, Blog> _blogRepository;
        private readonly IRepository<int, Category> _categoryRepository;
        private readonly IRepository<int, Comment> _commentRepository;
        private readonly IRepository<int, CommentLike> _commentLikeRepository;
        private readonly IRepository<int, BlogLike> _blogLikeRepository;
        private readonly IRepository<int, UserFollower> _userFollowerRepository;

        public UserService(IRepository<string, User> userRepository, ITokenService tokenService,
            IRepository<int, Blog> blogRepository, IRepository<int, Category> categoryRepository,
            IRepository<int, Comment> commentRepository, IRepository<int, CommentLike> commentLikeRepository,
            IRepository<int, BlogLike> blogLikeRepository, IRepository<int, UserFollower> userFollowerRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _commentLikeRepository = commentLikeRepository;
            _blogLikeRepository = blogLikeRepository;
            _userFollowerRepository = userFollowerRepository;
        }



        //------------------------------------------REGISTER METHOD----------------------------------
        public UserDTO? Register(UserDTO userDTO)
        {
            HMACSHA512 hmac = new HMACSHA512();
            User user = new User()
            {
                UserEmail = userDTO.UserEmail,
                UserName = userDTO.UserName,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                Key = hmac.Key,

                Role = userDTO.Role,
                RegistrationDate = DateTime.Now

            };
            var result = _userRepository.Add(user);
            if (result != null)
            {
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }

        //------------------------------------------LOGIN METHOD----------------------------------
        public UserDTO? Login(UserDTO userDTO)
        {
            if (userDTO == null || userDTO.UserEmail == null || userDTO.Password == null)
            {
                return null;
            }
            var user = _userRepository.GetById(userDTO.UserEmail);
            if (user != null && user.Key != null)
            {
                HMACSHA512 hmac = new HMACSHA512(user.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (user.Password[i] != userpass[i])
                        return null;
                }
                userDTO.UserName = user.UserName ?? "";
                userDTO.Role = user.Role;
                userDTO.Token = _tokenService.GetToken(userDTO);
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }

        public ProfilePic? EditUser(ProfilePic user)
        {
            if (user == null || user.UserEmail == null)
            {
                return null;
            }
            var checkUser=_userRepository.GetById(user.UserEmail);
            if(checkUser != null)
            {
                checkUser.Bio = user.Bio;
                checkUser.DateofBirth = user.DateofBirth;
                checkUser.Gender = user.Gender;
                if (user.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        user.ProfilePicture.CopyTo(memoryStream);
                        checkUser.ProfilePicture = memoryStream.ToArray();
                    }
                }
                var result = _userRepository?.Update(checkUser);
                if(result != null)
                    return user;
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            if (users != null)
            {
                return users.ToList();
            }
            throw new NoSuchUserExists();
        }

        public List<User> GetBloggers()
        {
            var users = _userRepository.GetAll();
            var bloggers=new List<User>();
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.Role == "Blogger")
                    {
                        bloggers.Add(user);
                    }
                }
                if (bloggers.Count > 0)
                {
                    return bloggers;
                }
            }
            throw new NoSuchUserExists();
        }

        public List<User> GetReaders()
        {
            var users = _userRepository.GetAll();
            var readers = new List<User>();
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (user.Role == "Reader")
                    {
                        readers.Add(user);
                    }
                }
                if (readers.Count > 0)
                {
                    return readers;
                }
            }
            throw new NoSuchUserExists();
        }


        public User? GetUserByEmail(string userEmail)
        {
            var user=_userRepository.GetById(userEmail);
            if(user!= null)
            {
                return user;
            }
            return null;
        }

        public UserDTO? DeleteUser(UserDTO userDTO)
        {
            var checkUser = _userRepository.GetById(userDTO.UserEmail);
            if (checkUser != null && checkUser.Key != null)
            {
                HMACSHA512 hmac = new HMACSHA512(checkUser.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (checkUser.Password[i] != userpass[i])
                        return null;
                }
            }
            if(DeleteBlogCascade(userDTO.UserEmail)==true && DeleteFollowCascade(userDTO.UserEmail) ==true)
            {
                var result = _userRepository.Delete(userDTO.UserEmail);
                if (result != null)
                {
                    return userDTO;
                }
            }
            return null;
        }

        private bool DeleteFollowCascade(string email)
        {
            var check = _userFollowerRepository.GetAll()?.Where(uf=>uf.FollowerId== email);
            var check2nd=_userFollowerRepository.GetAll()?.Where(ufe=>ufe.FollowingId== email);
            if (check == null && check2nd==null)
            {
                return true;
            }
            if (check2nd != null)
            {
                foreach(var item in check2nd)
                {
                    _userFollowerRepository.Delete(item.RelationId);
                }
            }
            if(check != null)
            {
                foreach (var item in check)
                {
                    _userFollowerRepository.Delete(item.RelationId);
                }
            }
            return true;
        }

        private bool DeleteBlogCascade(string email) { 
            var getBlogs=_blogRepository.GetAll()?.Where(b=>b.UserEmail == email);
            if (getBlogs == null)
            {
                return true;
            }
            if (getBlogs != null)
            {
                foreach (var blog in getBlogs)
                {
                    DeleteBlogLikeCascade(blog.BlogId);
                    DeleteBlogCategoryCascade(blog);
                    DeleteCommentCascade(blog.BlogId);
                    _categoryRepository.Delete(blog.BlogId);
                }
            }
            return true;

        }

        private bool DeleteBlogCategoryCascade(Blog blog)
        {
            var checkCategories = _categoryRepository.GetAll()?.Where(b => b.BlogId == blog.BlogId).ToList();
            if (checkCategories == null)
            {
                return true;
            }
            if (checkCategories != null)
            {
                foreach (var category in checkCategories)
                {
                    _categoryRepository.Delete(category.RelationId);
                }
            }
            return true;
        }

        private bool DeleteBlogLikeCascade(int id)
        {
            var checkBlogLikes = _blogLikeRepository.GetAll()?.Where(b => b.BlogId == id);
            if (checkBlogLikes == null)
            {
                return true;
            }
            if (checkBlogLikes != null)
            {
                foreach (var item in checkBlogLikes)
                {
                    _blogLikeRepository.Delete(item.BlogLikeId);
                }
            }
            return true;
        }

        private bool DeleteCommentCascade(int id)
        {
            var checkBlogLikes = _commentRepository.GetAll()?.Where(b => b.BlogId == id);
            if (checkBlogLikes == null)
            {
                return true;
            }
            if (checkBlogLikes != null)
            {
                foreach (var item in checkBlogLikes)
                {
                    DeleteCommentLikeCascade(item.CommentId);
                    _commentRepository.Delete(id);
                }
            }
            return true;
        }

        private bool DeleteCommentLikeCascade(int commentID)
        {
            var getItems = _commentLikeRepository.GetAll()?.Where(cl => cl.CommentId == commentID);
            if (getItems == null)
            {
                return true;
            }
            if (getItems != null)
            {
                foreach (var item in getItems)
                {
                    var result = _commentLikeRepository.Delete(item.CommentLikeId);
                }
            }
            return true;
        }

    }
}
