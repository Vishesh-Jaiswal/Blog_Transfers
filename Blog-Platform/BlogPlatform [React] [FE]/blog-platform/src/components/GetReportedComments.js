import React, { useState, useEffect } from "react";
import axios from "axios";
import "./GetBlogs.css";
import { Link } from "react-router-dom";
import Navbar from "./Navbar";

function GetReportedComments() {
  const [reportedComments, setReportedComments] = useState([]);

  useEffect(() => {
    const fetchBlogs = async () => {
      try {
        const blogResponse = await axios.get('http://localhost:5273/api/Comment/ReportedComments');
        setReportedComments(blogResponse.data|| []);

      } catch (error) {
        console.error('Error fetching blogs:', error);
      }
    };

    fetchBlogs();
  }, []);

  return (
    <div className="show-blogs">
      <Navbar />
      <div className="blog-list-container">
        <div className="blogContents">
          <h2 className="PageTitle">Reported Comments</h2>
          {Array.isArray(reportedComments) && reportedComments.length > 0 ? (
            <ul>
              {reportedComments.map((blog) => (
                <Link to={`/showreportedcomment/${blog.commentId}`} className="blog-link">
                <li key={blog.commentId} className="blog-item">
                  <b><i>{blog.userEmail}</i></b>
                  <p className="blog-content">{blog.content}</p>
                </li>
                </Link>
              ))}
            </ul>
          ) : (
            <p id="EmplyBlogList">No Reported Comments Available</p>
          )}
        </div>
      </div>
    </div>
  );
}

export default GetReportedComments;