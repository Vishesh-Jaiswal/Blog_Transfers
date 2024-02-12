import React, { useState, useEffect } from "react";
import axios from "axios";
import { useParams, Link, useNavigate } from "react-router-dom";
import './BlogView.css';
import Navbar from "./Navbar";
import moment from "moment/moment";

function ReportedComment() {
  const { commentId } = useParams();
  const [reportedComment, setReportedComment] = useState(null);
  const userEmail = localStorage.getItem('userEmail');
  const [isApproveBoxOpen,setApproveBox] = useState(false);
  const [isDeleteBoxOpen,setDeleteBox] = useState(false);
  const [apiDelay,setApiDelay] = useState(false);
  const navigate = useNavigate();


  //Fetches the blog
  useEffect(() => {
  const fetchBlog = async () => {
    try {
      const commentResponse = await axios.get(`http://localhost:5273/api/Comment/GetCommentByCommentID/${commentId}`);
      setReportedComment(commentResponse.data);

    } catch (error) {
        if (error.commentResponse) {
          console.error(`Error for API call: ${error.config.url}`, error.commentResponse.data);
        } else if (error.request) {
          console.error(`No response received for API call: ${error.config.url}`);
        } else {
          console.error(`Error setting up the API call:`, error.message);
        }
      }
    };
  fetchBlog();
  }, [commentId, userEmail]);

  const handleApprove = async () =>{
    setApiDelay(true);
    const response = await axios.put(`http://localhost:5273/api/Comment/ApproveReportComment/${commentId}`)
    .then(response => {
      console.log(response.data);
      setApproveBox(true);
      setTimeout(()=>setApproveBox(false),2000);
      setTimeout(()=>{setApiDelay(false);navigate('/reportedcomments');},2000);
      
    })
    .catch(error => {
      console.error("Error in approval:", error);
    });
  };

  const handleDelete = async () => {
    setApiDelay(true);
    // Display a confirmation dialog
    const confirmDelete = window.confirm("Are you sure you want to delete this Comment?");
  
    if (confirmDelete) {
      try {
        const response = await axios.delete("http://localhost:5273/api/Comment/Delete", {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem("token"),
          },
          data: {
            commentId: commentId,
            userEmail: userEmail
          },
        }).then(response => {
          console.log(response.data);
          setDeleteBox(true);
          setTimeout(()=>setDeleteBox(false),2000);
          setTimeout(()=>{setApiDelay(false);navigate('/reportedblogs');},2000);
        });
      } catch (error) {
        console.error("Error deleting content:", error);
      }
    }
  };

  return (
    <div className="MainBlogView">
      <Navbar />
      {/* Shows a single blog */}
      {isApproveBoxOpen && (
        <>
        <div className="veil"></div>
        <div className="approveBox">
          Comment Approved
        </div>
        </>
      )}
      {isDeleteBoxOpen && (
        <>
        <div className="veil"></div>
        <div className="approveBox">
          Comment Deleted
        </div>
        </>
      )}
      <div className="ReportedView">
        <div className="singleBlogContainer reportedblog">
          <div className="titleAndReportButtons">
            <h2 className="titleofBlog">Reported Comment</h2>
            <div className="blogFunctions">
              <button className="editBlog" onClick={handleApprove}>
                Approve
              </button>
              <button className="editBlog" onClick={handleDelete}>
                Delete
              </button>
            </div>
          </div>
          <hr id="hrrule" />
            <div className="contentofBlog">
              {/* Content of the blog*/}
              <p>{reportedComment?.content}</p>
            </div>
          {/* Blogger Profile Link*/}
          <Link to={`/profile/${reportedComment?.userEmail}`} className="profileLink">
            <p className="author">------<i>{reportedComment?.userEmail}</i>------</p>
          </Link>
        </div>
        <div className="colorfill">
          <div className="singleBlogContainer review">
          <div className="contentofBlog">
            <h2 className="titleofBlog">Report Review</h2>
            <hr id="hrrule" />
            <p id="reportReview"><b>Reported At:</b>{moment(reportedComment?.reportedAt).format("DD/MM/YY HH:mm")}</p> 
            <p id="reportReview"><b>Report By:</b> {reportedComment?.reportedBy}</p>
            <p id="reportReview"><b>Report Reason:</b></p>  <p>{reportedComment?.reportReason}</p>
          </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ReportedComment;