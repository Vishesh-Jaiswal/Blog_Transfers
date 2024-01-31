import React, { useState } from "react";
import axios from "axios";
import './DeleteProfile.css';

function DeleteProfile({ onDelete, onCancel, userEmail }) {
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleDelete = async () => {
    try {
      // Check if the password is entered
      if (!password) {
        setError("Please enter your password.");
        return;
      }

      await axios.delete("http://localhost:5273/api/Blogger/Delete", {
        data: {
          userEmail: userEmail,
          password: password,
        },
      });
      onDelete();
    } catch (error) {
      console.error("Error deleting profile:", error);
      setError("Error deleting profile. Please try again.");
    }
  };

  return (
    <div className="Mainbox">
            <div className="wrapper wrapper1">
                <div className="delConfirm">
                  Are you sure you want to delete your account?
                  Enter Your Password to confirm
                  <br/>
                  <input
                    type="password"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </div>
                {/* Delete Form*/}
                <form className="deleteForm">
                  <div className="field">
                    <input type="button" value="YES" onClick={handleDelete}/>
                  </div>
                  {error && <p className="error-message">{error}</p>}
                  <div className="field">
                    <input type="button" value="NO" onClick={onCancel}/>
                  </div>
                </form>
            </div>
        </div>
  );
}

export default DeleteProfile;
