import { useState, useEffect } from "react";
import axios from "axios";
import './Profile.css';
import Navbar from "./Navbar";
import { Link, useNavigate, useParams } from "react-router-dom";
import DeleteProfile from "./DeleteProfile";
import EditProfile from "./EditProfile";
import moment from "moment/moment";

function Profile() {
    
    const navigate=useNavigate();
    const { userEmail: currentEmail } = useParams();
    var currentUserEmail = localStorage.getItem('userEmail');
    const [userBlogs, setUserBlogs] = useState([]);
    const [userComments, setUserComments] = useState([]);
    const [userInfo, setUserInfo] = useState({});
    const [userFollowerInfo, setFollowerUserInfo] = useState([]);
    const [userFollowingInfo, setFollowingUserInfo] = useState([]);
    const [isFollowing, setIsFollowing] = useState(false);
    const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const role = userInfo.role;

  const handleEditProfile = () => {
    setIsEditModalOpen(true);
  };

  const handleCancelEdit = () => {
    setIsEditModalOpen(false);
  };

  const handleUpdateProfile = () => {
    setIsEditModalOpen(false);
    setUserInfo({});
  };

    const handleDeleteProfile = () => {
      setIsDeleteModalOpen(true);
    };
  
    const handleCancelDelete = () => {
      setIsDeleteModalOpen(false);
    };
  
    const handleConfirmedDelete = () => {
      setIsDeleteModalOpen(false);
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('userEmail');
      localStorage.removeItem('userName');
      navigate('/');
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                if (role === 'Blogger') {
                    const userBlogResponse = await axios.get(`http://localhost:5273/api/Blog/userBlogs/${currentEmail}`);
                    console.log(userBlogResponse.data);
                    setUserBlogs(userBlogResponse.data);

                    const userFollowerResponse = await axios.get(`http://localhost:5273/api/UserFollower/followers/${currentEmail}`);
                    console.log(userFollowerResponse.data);
                    setFollowerUserInfo(userFollowerResponse.data);

                } else if (role === 'Reader') {
                    const userCommentResponse = await axios.get(`http://localhost:5273/api/Comment/userComments/${currentEmail}`);
                    console.log(userCommentResponse.data);
                    setUserComments(userCommentResponse.data);

                    const userFollowingResponse = await axios.get(`http://localhost:5273/api/UserFollower/followees/${currentEmail}`);
                    console.log(userFollowingResponse.data);
                    setFollowingUserInfo(userFollowingResponse.data);
                }
                const userInfoResponse = await axios.get(`http://localhost:5273/api/Blogger/user/${currentEmail}`);
                console.log(userInfoResponse.data);
                setUserInfo(userInfoResponse.data);

                

                

            } catch (error) {
                console.error('Error fetching datbioa:', error);
            }
        };

        fetchData();
    }, [role, currentEmail]);

    useEffect(() => {
        const checkFollowStatus = async () => {
            try {
                const followStatusResponse = await axios.post(
                    'http://localhost:5273/api/UserFollower/Status',
                    {
                        followerId: currentUserEmail,
                        followingId: userInfo.userEmail,
                    }
                );
                setIsFollowing(followStatusResponse.data);
            } catch (error) {
                console.error('Error checking follow status:', error);
            }
        };
        checkFollowStatus();
    }, [currentUserEmail, userInfo.userEmail]);

    const handleFollowToggle = async () => {
        try {
            const response = await axios.post('http://localhost:5273/api/UserFollower/Follow', {
                followerId: currentUserEmail,
                followingId: userInfo.userEmail,
            });
            console.log(response.data);
            setIsFollowing(prevIsFollowing => !prevIsFollowing);
            if (role === 'Blogger') {
            const updatedFollowerResponse = await axios.get(`http://localhost:5273/api/UserFollower/followers/${userInfo.userEmail}`);
            console.log(updatedFollowerResponse.data);
            setFollowerUserInfo(updatedFollowerResponse.data);
            } else if (role === 'Reader') {
            const updatedFollowingResponse = await axios.get(`http://localhost:5273/api/UserFollower/followees/${userInfo.userEmail}`);
            console.log(updatedFollowingResponse.data);
            setFollowingUserInfo(updatedFollowingResponse.data);
            }
        } catch (error) {
            console.error('Error toggling follow status:', error);
        }
    };
    

    const handleFollowerProfileLinkClick = () => {
        setUserInfo({});
    };

    return (
        <div className="bgcolor">
            <div className="MainProfile">
                <Navbar />
                <div className="profileBox">
                    {isEditModalOpen && (
                        <EditProfile userEmail={currentEmail} onCancel={handleCancelEdit} onUpdate={handleUpdateProfile}/>
                    )}
                    {isDeleteModalOpen && (
                        <DeleteProfile onDelete={handleConfirmedDelete} onCancel={handleCancelDelete} userEmail={userInfo.userEmail}/>
                    )}
                    <div className="fulluser">
                        <div className="user1">
                            <div className="Name-Email-Gender-DOB-Button">
                                <div className="NameAndEmail">
                                    <div>
                                        <b>Name:</b>
                                        <div class="scrolling-container">
                                            <p class="scrolling-text"> {userInfo.userName ? userInfo.userName : "Name not provided"}</p>
                                        </div>
                                    </div>

                                    <div>
                                        <b>Email:</b>
                                        <div class="scrolling-container">
                                            <p class="scrolling-text"> {userInfo.userEmail ? userInfo.userEmail : "Email not provided"}</p>
                                        </div>
                                    </div>
                                </div>

                                <div className="GenderAndDoB">
                                    <div>
                                        <b>Gender:</b>
                                        <div>
                                            <p class="gender"> {userInfo.gender ? userInfo.gender : "N/A"}</p>
                                        </div>
                                    </div>
                                    <div>
                                        <b>DOB:</b>
                                        <div class="dob">
                                            <p>
                                            {userInfo.dateofBirth
                                                ? moment(userInfo.dateofBirth).format("DD/MM/YY")
                                                : "N/A"}
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                {currentUserEmail === userInfo.userEmail ? (
                                    <div className="toggleButtons">
                                        <button className="editProfile" onClick={handleDeleteProfile}>
                                            Delete
                                        </button>
                                        <button className="editProfile" onClick={handleEditProfile}>
                                            Edit
                                        </button>
                                        
                                    </div>
                                ) : ((localStorage.getItem('role') === "Reader") && (
                                    <div className="followToggle">
                                    <input
                                            type="checkbox"
                                            id="followCheckbox"
                                            checked={isFollowing}
                                            onChange={handleFollowToggle}
                                        />
                                        <label htmlFor="followCheckbox">{isFollowing ? 'Following' : '+Follow'}</label>
                                    </div>
                                ))}
                            
                            </div>
                                <div>
                                    <p id="bio">{userInfo.bio ? userInfo.bio : "Bio not provided"}</p>
                                </div>
                        </div>
                        <div className="profilePic">
                            {userInfo.profilePicture === null ? (
                                <div className="circular-div">
                                <img
                                    src="/images/NullUser.jpeg"
                                    alt="Default Profile"
                                />
                                </div>
                            ) : (
                                <div className="circular-div">
                                    <img src={`data:image/png;base64,${userInfo.profilePicture}`} alt="Profile Preview" />
                                </div>
                            )}
                        </div>

                    </div>
                    <div className="BorR">
                        {role === 'Reader' && (
                            <div>
                                <div className="profile-blog-list-container">
                                    <h2 className="profileBlogTitle">Comments</h2>
                                    <div className="profileblogContents">
                                        {Array.isArray(userComments) && userComments.length > 0 ? (
                                            <ul>
                                                {userComments.map((comment) => (
                                                    <li className="profileBlogItem" key={comment.commentId}>
                                                        <p className="blog-title">{comment.userEmail}</p> <br />
                                                        <p className="blog-content">{comment.content}</p> <br />
                                                        <p className="blog-content time">{moment(comment.commentedAt).format("DD/MM/YY HH:mm")}</p>
                                                    </li>
                                                ))}
                                            </ul>
                                        ) : (
                                        <p id="noFollowers">No comments available.</p>
                                        )}
                                    </div>
                                </div>
                                <div className="followList">
                                    <h2 className="profileFollowTitle">Bloggers You Follow</h2>
                                    <div className="profileFollowContents">
                                        {Array.isArray(userFollowingInfo) && userFollowingInfo.length > 0 ? (
                                            <ul>
                                                {userFollowingInfo.map((userFollowInfoItem) => (
                                                    <li key={userFollowInfoItem.relationId} className="profileFollowItem">
                                                        <Link
                                                            to={`/profile/${userFollowInfoItem.followingId}`}
                                                            className="followerProfileLink"
                                                            onClick={() => handleFollowerProfileLinkClick(userFollowInfoItem.followingId)}>
                                                            <h3>{userFollowInfoItem.followingId}</h3>
                                                        </Link>
                                                    </li>
                                                ))}
                                            </ul>
                                        ) : (
                                            <p id="noFollowers">You Don't Follow Anyone.</p>
                                        )}
                                    </div>
                                </div>
                            </div>
                        )}
                        {role === 'Blogger' && (
                            <div className="profileBlogsList">
                                <div className="profile-blog-list-container">
                                    <h2 className="profileBlogTitle">Blogs</h2>
                                    <div className="profileblogContents">
                                        {Array.isArray(userBlogs) && userBlogs.length > 0 ? (
                                            <ul>
                                                {userBlogs.map((userBlog) => (
                                                    <li key={userBlog.blogId} className="profileBlogItem">
                                                        <Link to={`/showblogs/${userBlog.blogId}`} className="blog-link">
                                                            <h3 className="blog-title">{userBlog.title}</h3>
                                                        </Link>
                                                        <p className="blog-content">{userBlog.content}</p>
                                                    </li>
                                                ))}
                                            </ul>
                                        ) : (
                                            <p id="noFollowers">No Blogs to Show</p>
                                        )}
                                    </div>
                                </div>
                                <div className="followList">
                                    <h2 className="profileFollowTitleforFollowers">Followers</h2>
                                    <div className="profileFollowContents">
                                        {Array.isArray(userFollowerInfo) && userFollowerInfo.length > 0 ? (   
                                            <ul>
                                                {userFollowerInfo.map((userFollowInfoItem) => (
                                                    <li key={userFollowInfoItem.relationId} className="profileFollowItem">
                                                        <Link
                                                            to={`/profile/${userFollowInfoItem.followerId}`}
                                                            className="followerProfileLink"
                                                            onClick={() => handleFollowerProfileLinkClick(userFollowInfoItem.followerId)}>
                                                                <h3>{userFollowInfoItem.followerId}</h3>
                                                        </Link>
                                                    </li>
                                                ))}
                                            </ul>
                                        ) : (
                                            <p id="noFollowers">You Currently Have No Followers.</p>
                                        )}
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;
