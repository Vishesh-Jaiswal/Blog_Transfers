import React, { useState, useEffect } from "react";
import axios from "axios";
import './EditProfile.css';

function ReportComment({ commentIdtoReport,userEmail, onCancel, onReport }) {
  const [reportReason,setReportReason]=useState("");

  const handleReportComment = async () => {
    try {
      await axios.put("http://localhost:5273/api/Comment/ReportComment",
        {
          commentId:commentIdtoReport,
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
          <h2>Report Comment</h2>
          {/* Enter Bio */}
          <label>
            Report Reason
            <input type="text" required value={reportReason}
              onChange={(e) => {setReportReason(e.target.value)}}
            />
          </label>
          <div className="edit-profile-buttons">
            <button onClick={handleReportComment}>Report</button>
            <button onClick={onCancel}>Cancel</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ReportComment;