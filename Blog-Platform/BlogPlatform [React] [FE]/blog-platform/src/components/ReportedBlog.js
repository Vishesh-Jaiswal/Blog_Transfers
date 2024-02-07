import React, { useState, useEffect } from "react";
import axios from "axios";
import { useParams, Link, useNavigate } from "react-router-dom";
import './BlogView.css';
import Navbar from "./Navbar";
import moment from "moment/moment";

function ReportedBlog() {
  const { blogId } = useParams();
  const [reportedBlog, setReportedBlog] = useState(null);
  const userEmail = localStorage.getItem('userEmail');
  const navigate = useNavigate();


  //Fetches the blog
  useEffect(() => {
  const fetchBlog = async () => {
    try {
      const blogResponse = await axios.get(`http://localhost:5273/api/Blog/${blogId}`);
      setReportedBlog(blogResponse.data);

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
  const handleApprove = async () =>{
    const response = await axios.put(`http://localhost:5273/api/Blog/ApproveReportBlog/${blogId}`)
    .then(console.log(response)).catch(console.log(response))
  }

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
            title: reportedBlog.title,
            content: reportedBlog.content,
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


  return (
    <div className="MainBlogView">
      <Navbar />
      {/* Shows a single blog */}
      <div className="ReportedView">
        <div className="singleBlogContainer reportedblog">
          <h2 className="titleofBlog">{reportedBlog?.title}</h2>
          <hr id="hrrule" />
            <div className="contentofBlog">
              {/* Content of the blog*/}
              <p>{reportedBlog?.content}</p>
            </div>
          {/* Blogger Profile Link*/}
          <Link to={`/profile/${reportedBlog?.userEmail}`} className="profileLink">
            <p className="author">------<i>{reportedBlog?.userEmail}</i>------</p>
          </Link>
        </div>
        <div className="singleBlogContainer review">
        <div className="contentofBlog">
          <h2 className="titleofBlog">Report Review</h2>
          <div className="blogFunctions">

                <button className="editBlog" onClick={handleApprove}>
                  Approve
                </button>
                
                <button className="editBlog" onClick={handleDelete}>
                  Delete
                </button>
              </div>
          
          <hr id="hrrule" />
          <p id="reportReview"><b>Reported At:</b>{moment(reportedBlog?.reportedAt).format("DD/MM/YY HH:mm")}</p> 
          <p id="reportReview"><b>Report By:</b> {reportedBlog?.reportedBy}</p>
          <p id="reportReview"><b>Report Reason:</b></p>  <p>{reportedBlog?.reportReason}</p>
        </div>
        </div>
      </div>
    </div>
  );
}

export default ReportedBlog;