import React, { useState, useEffect } from "react";
import axios from "axios";
import { useParams, Link, useNavigate } from "react-router-dom";
import './BlogView.css';
import NewComment from "./NewComment";
import Navbar from "./Navbar";
import moment from "moment/moment";
import ReportComment from "./ReportComment";

function BlogView() {
  const { blogId } = useParams();
  const [blog, setBlog] = useState(null);
  const [comments, setComments] = useState([]);
  const [showNewComment, setShowNewComment] = useState(false);
  const [editing, setEditing] = useState(false);
  const [editedContent, setEditedContent] = useState("");
  const [blogLikeStatus, setBlogLikeStatus] = useState(false);
  const userEmail = localStorage.getItem('userEmail');
  const role = localStorage.getItem('role');
  const navigate = useNavigate();
  const [commentLikes, setCommentLikes] = useState([]);
  const [editingCommentId, setEditingCommentId] = useState(null);
  const [reportedcommentId, setReportedcommentId] = useState("");
  const [editedCommentContent, setEditedCommentContent] = useState("");
  const [isReportCommentModalOpen, setIsReportCommentModalOpen] = useState(false);
  const [isReportBlogModalOpen, setIsReportBlogModalOpen] = useState(false);
  var currentEmail = userEmail;


  const handleReportComment = (commentId) => {
    setIsReportCommentModalOpen(true);
    setReportedcommentId(commentId);
  };

  const handleCancelReportCommnet = () => {
    setIsReportCommentModalOpen(false);
  };

  const handleConfirmCommentReport = () =>{
    setIsReportCommentModalOpen(false);
  }

  const handleReportBlog = () => {
    setIsReportCommentModalOpen(true);
  };

  const handleCancelBlogReport = () => {
    setIsReportCommentModalOpen(false);
  };

  const handleConfirmBlogReport = () =>{
    setIsReportCommentModalOpen(false);
  }


  //Fetches the blog, it's comments and bloglikes
  useEffect(() => {
  const fetchBlog = async () => {
    try {
      const blogResponse = await axios.get(`http://localhost:5273/api/Blog/${blogId}`);
      setBlog(blogResponse.data);

      const commentsResponse = await axios.get(`http://localhost:5273/api/Comment/${blogId}`);
        setComments(commentsResponse.data);

      const blogLikeStatusResponse = await axios.post('http://localhost:5273/api/BlogLike/BlogLikeStatus', {
        userEmail,
        blogId,
      });
      setBlogLikeStatus(blogLikeStatusResponse.data);
      
      const commentLikesResponse = await axios.get(`http://localhost:5273/api/CommentLike/${blogId}/${userEmail}`);
        if (Array.isArray(commentLikesResponse.data)) {
          setCommentLikes(commentLikesResponse.data.map((like) => like.commentId));
        } else if (typeof commentLikesResponse.data === 'object') {
          setCommentLikes([commentLikesResponse.data.someProperty]);
        } 
        
    } catch (error) {
        if (error.response) {
          console.error(`Error for API call: ${error.config.url}`, error.response.data);
        } else if (error.request) {
          console.error(`No response received for API call: ${error.config.url}`);
        } else {
          console.error(`Error setting up the API call:`, error.message);
        }
      }
    };

  fetchBlog();
  }, [blogId, userEmail]);

  //Fetches Comments Likes


  const handleCommentLikeToggle = async (commentId) => {
    try {
      await axios.post(`http://localhost:5273/api/CommentLike/CommentLikeToggle`, {
        userEmail,
        commentId,
        blogId
      });
      setCommentLikes((prevLikes) =>
        prevLikes.includes(commentId) ? prevLikes.filter((id) => id !== commentId) : [...prevLikes, commentId]
      );
    } catch (error) {
      console.error("Error toggling comment like:", error);
    }
  };

  const handleCommentAdded = () => {
    axios.get(`http://localhost:5273/api/Comment/${blogId}`)
      .then(response => setComments(response.data))
      .catch(error => console.error("Error fetching comments:", error));
    setShowNewComment(false);
  };

  const handleCancel = () => {
    setShowNewComment(false);
  };

  const handleEdit = () => {
    setEditing(true);
    setEditedContent(blog?.content || "");
  };

  const handleConfirmEdit = async () => {
    try {
      await axios.post("http://localhost:5273/api/Blog/Edit",
        {
          blogId: blogId,
          title: blog.title,
          content: editedContent,
          userEmail: userEmail,
        },
        {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("token"),
          },
        }
      );

      const updatedBlogResponse = await axios.get(`http://localhost:5273/api/Blog/${blogId}`);
      setBlog(updatedBlogResponse.data);

      setEditing(false);
    } catch (error) {
      console.error("Error updating content:", error);
    }
  };

  const handleDelete = async () => {
    // Display a confirmation dialog
    const confirmDelete = window.confirm("Are you sure you want to delete this blog?");
  
    if (confirmDelete) {
      try {
        const response = await axios.delete("http://localhost:5273/api/Blog/Delete", {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("token"),
          },
          data: {
            blogId: blogId,
            title: blog.title,
            content: blog.content,
            userEmail: userEmail,
          },
        });
  
        console.log(response.data);
        navigate('/homepage');
      } catch (error) {
        console.error("Error deleting content:", error);
      }
    }
  };

  const handleBlogLikeToggle = async () => {
    try {
      await axios.post(`http://localhost:5273/api/BlogLike/BlogLikeToggle`, {
        userEmail,
        blogId,
      });
  
      setBlogLikeStatus(prevStatus => !prevStatus);
    } catch (error) {
      console.error("Error toggling like:", error);
    }
  };

  const handleEditComment = (commentId) => {
    const commentToEdit = comments.find((comment) => comment.commentId === commentId);
    setEditingCommentId(commentId);
    setEditedCommentContent(commentToEdit.content);
  };
  
  const handleConfirmCommentEdit = async (commentId) => {
    try {
      await axios.post("http://localhost:5273/api/Comment/EditComment", {
        commentId,
        content: editedCommentContent,
        userEmail,
        blogId
      });
  
      const updatedCommentsResponse = await axios.get(`http://localhost:5273/api/Comment/${blogId}`);
      setComments(updatedCommentsResponse.data);
  
      setEditingCommentId(null);
    } catch (error) {
      console.error("Error updating comment content:", error);
    }
  };
  
  const handleCancelCommentEdit = () => {
    setEditingCommentId(null);
  };
  
  const handleDeleteComment = async (commentId) => {
    // Display a confirmation dialog
    const confirmDelete = window.confirm("Are you sure you want to delete this comment?");
  
    if (confirmDelete) {
      try {
        await axios.delete(`http://localhost:5273/api/Comment/Delete`, {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          data: {
            blogId,
            commentId,
            userEmail,
          },
        });
  
        const updatedCommentsResponse = await axios.get(`http://localhost:5273/api/Comment/${blogId}`);
        setComments(updatedCommentsResponse.data);
      } catch (error) {
        console.error("Error deleting comment:", error);
      }
    }
  };
  


  return (
    <div className="MainBlogView">
      <Navbar />
      {isReportCommentModalOpen && (
        <ReportComment commentIdtoReport={reportedcommentId} userEmail={currentEmail} onCancel={handleCancelReportCommnet} onReport={handleConfirmCommentReport}/>
      )}
      {isReportBlogModalOpen && (
        <ReportBlog blogIdtoReport={blogId} userEmail={currentEmail} onCancel={handleCancelBlogReport} onReport={handleConfirmBlogReport}/>
      )}
      {/* Shows a single blog */}
      <div className="singleBlogContainer">
        <div>
          <div className="blogButtons">
            {!editing && blog?.userEmail === userEmail && (
              <div>
                <button className="editBlog" onClick={handleEdit}>
                  Edit
                </button>
                <button className="editBlog" onClick={handleDelete}>
                  Delete
                </button>
              </div>
            )}
            <button className="editBlog" onClick={handleReportBlog}>
              Report
            </button>
          </div>
          {/* Shows the title of the  blog */}
          <h2 className="titleofBlog">{blog?.title}</h2>
        </div>
        <hr id="hrrule" />
        {/* Editable blog content */}
        {editing ? (
          <textarea
            className="contentofBlog editable"
            value={editedContent}
            onChange={(e) => setEditedContent(e.target.value)}/>
        ) : (
          <div className="contentofBlog">
            {/* Content of the blog*/}
            <p>{blog?.content}</p>
          </div>
        )}
        {/* Blogger Profile Link*/}
        <Link to={`/profile/${blog?.userEmail}`} className="profileLink">
          <p className="author">------<i>{blog?.userEmail}</i>------</p>
        </Link>
        {/* Blog Like Button*/}
        <button className={`blogLikeButton ${blogLikeStatus ? 'likedBlog' : ''}`} onClick={handleBlogLikeToggle}>
          <i className="fas fa-thumbs-up"></i>
        </button>
        {/* Blog Confirm Edit Button and Cancel Button*/}
        {editing && (
          <div className="buttonsnew">
            <button className="confirmEditButton" onClick={handleConfirmEdit}>Confirm Edit</button>
            <button className="cancelButton" onClick={() => setEditing(false)}>Cancel</button>
          </div>
        )}
      </div>

      {/* Comments Section*/}
      <div className="commentContainer1">
        <div className="commentContainer2">
          <div className="Comment-Span">
            {role === 'Reader' && !showNewComment && (
              <Link to='#' className="comment-Link" onClick={() => setShowNewComment(true)}>
                <i>Add Comment</i>
              </Link>
            )}
          </div>
          {/* Renders NewComment.js */}
          <div className="newcomment">
            {showNewComment && (
              <NewComment path="/AddComment" blogId={blogId} onCommentAdded={handleCommentAdded} onCancel={handleCancel} />
            )}
          </div>
          {/* Comments List */}
          {Array.isArray(comments) && comments.length > 0 ? (
            <ul>
              {comments.map((comment) => (
                <li className="singleComment" key={comment.commentId}>
                  {/* Edit COmment */}
                  {editingCommentId === comment.commentId ? (
                    <div>
                      <textarea
                        className="contentofComment editable editeofComment"
                        value={editedCommentContent}
                        onChange={(e) => setEditedCommentContent(e.target.value)}/>
                      <button className="confirmEditButton" onClick={() => handleConfirmCommentEdit(comment.commentId)}>
                        Confirm Edit
                      </button>
                      <button className="cancelButton" onClick={() => handleCancelCommentEdit(comment.commentId)}>
                        Cancel
                      </button>
                    </div>
                  ) : (
                    <div>
                      {/* Non-Editable Comment List */}
                      <div className="buttonandTitle">
                        <Link to={`/profile/${comment.userEmail}`}>{comment.userEmail}</Link><br />
                        <div className="buttonsforComment">
                        {comment.userEmail === userEmail && (
                          <div>
                            <button id="forCommentEdit" onClick={() => handleEditComment(comment.commentId)}>
                              Edit
                            </button>
                            <button id="forCommentEdit" onClick={() => handleDeleteComment(comment.commentId)}>
                              Delete
                            </button>
                        </div>
                        )}
                        <button id="forCommentEdit" onClick={() => handleReportComment(comment.commentId)}>
                          Report
                        </button></div>
                      </div>
                      <div className="actualComment"><p>{comment.content}</p></div> <br />
                      {moment(comment.commentedAt).format("DD/MM/YY HH:mm")}
                      {/* Comment Likes */}
                      <button
                        className={`commentLikeButton ${commentLikes.includes(comment.commentId) ? 'likedComment' : ''}`}
                        onClick={() => handleCommentLikeToggle(comment.commentId)}>
                        <i className="fas fa-thumbs-up"></i>
                      </button>
                      {commentLikes.includes(comment.commentId) ? (
                        <span className="likedStatus">Liked by you</span>
                      ) : (
                        <span className="likedStatus">Not liked by you</span>
                      )}
                    </div>
                  )}
                </li>
              ))}
            </ul>
          ) : (
            <p id="noComments">No comments available.</p>
          )}
        </div>
      </div>
    </div>
  );
}

export default BlogView;