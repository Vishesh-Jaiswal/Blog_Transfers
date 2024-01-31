import React, { useState, useEffect } from "react";
import axios from "axios";
import './EditProfile.css';

function EditProfile({ userEmail, onCancel, onUpdate }) {
  const [formData, setFormData] = useState({
    bio: "",
    gender: "",
    profilePicture: null,
    dateofBirth: "",
  });

  const [minDate,setMinDate]=useState('');
  const [maxDate,setMaxDate]=useState('');

  useEffect(() => {
    const currentDate = new Date();
    const maxDate = new Date(currentDate);
    maxDate.setFullYear(maxDate.getFullYear() - 13);
    setMaxDate(maxDate.toISOString().split('T')[0]);
 
    const minDate = new Date(currentDate);
    minDate.setFullYear(minDate.getFullYear() - 80);
    setMinDate(minDate.toISOString().split('T')[0]);
  },[]);

  const genderOptions = ["Male", "Female", "Other"];

  useEffect(() => {
    const fetchData = async () => {
      try {
        const userInfoResponse = await axios.get(
          `http://localhost:5273/api/Blogger/user/${userEmail}`
        );
        setFormData({
          bio: userInfoResponse.data.bio || "",
          gender: userInfoResponse.data.gender || "",
          profilePicture: null,
          dateofBirth: userInfoResponse.data.dateofBirth || "",
        });
      } catch (error) {
        console.error("Error fetching user data for edit:", error);
      }
    };

    fetchData();
  }, [userEmail]);

  const handleInputChange = (e) => {
    const { name, value, type } = e.target;
    setFormData({
      ...formData,
      [name]: type === "file" ? e.target.files[0] : value,
    });
  };

  const handleUpdateProfile = async () => {
    try {
      const formDataForUpdate = new FormData();
      formDataForUpdate.append("userEmail", userEmail);
      formDataForUpdate.append("bio", formData.bio);
      formDataForUpdate.append("gender", formData.gender);
      formDataForUpdate.append("dateofBirth", formData.dateofBirth);
      formDataForUpdate.append("profilePicture", formData.profilePicture);

      await axios.post("http://localhost:5273/api/Blogger/Edit", formDataForUpdate);

      onUpdate();
    } catch (error) {
      console.error("Error updating user profile:", error);
    }
  };
  

  return (
    <div className="MainEdit">
      <div className="edit-profile-modal">
        <div className="edit-profile-form">
          <h2>Edit Profile</h2>
          {/* Enter Bio */}
          <label>
            Bio:
            <textarea
              name="bio"
              value={formData.bio}
              onChange={handleInputChange}
            />
          </label>
          {/* Gender */}
          <label>
            Gender:
            <select
              name="gender"
              value={formData.gender}
              onChange={handleInputChange}
            >
              <option value="">Select Gender</option>
              {genderOptions.map((option) => (
                <option key={option} value={option}>
                  {option}
                </option>
              ))}
            </select>
          </label>
          {/* Profile Picture */}
          <label>
            Profile Picture:
            <input
              type="file"
              name="profilePicture"
              onChange={handleInputChange}
            />
          </label>
          {/* Enter DOB */}
          
          <label>
            Date of Birth:
            <input
              type="date"
              name="dateofBirth"
              value={formData.dateofBirth}
              onChange={handleInputChange}
              min={minDate}
              max={maxDate}
            />
          </label>
          <div className="edit-profile-buttons">
            <button onClick={handleUpdateProfile}>Update Profile</button>
            <button onClick={onCancel}>Cancel</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default EditProfile;