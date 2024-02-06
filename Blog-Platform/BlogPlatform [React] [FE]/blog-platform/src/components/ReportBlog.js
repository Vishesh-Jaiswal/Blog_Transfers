import React, { useState } from "react";
import axios from "axios";
import './EditProfile.css';

function ReportBlog({ blogIdtoReport,userEmail, onCancel, onReport }) {
  const [reportReason,setReportReason]=useState("");

  const handleReportBlog = async () => {
    try {
      await axios.put("http://localhost:5273/api/Blog/ReportBlog",
        {
          blogId:blogIdtoReport,
          reportReason:reportReason,
          reportedBy:userEmail
        })
        onReport();
      } catch (error) {
        console.error("Error updating content:", error);
      }
  };
  

  return (
    <div className="MainEdit">
      <div className="edit-profile-modal">
        <div className="edit-profile-form">
          <h2>Report Blog</h2>
          {/* Enter Report Reason */}
          <label>
            Report Reason
            <input type="text" required value={reportReason}
              onChange={(e) => {setReportReason(e.target.value)}}
            />
          </label>
          <div className="edit-profile-buttons">
            <button onClick={handleReportBlog}>Report</button>
            <button onClick={onCancel}>Cancel</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ReportBlog;