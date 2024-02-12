import React from 'react';
import { useNavigate,Link } from 'react-router-dom';
import './LogoutButton.css';

const LogoutButton = () => {
  const navigate = useNavigate ();

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userEmail');
    localStorage.removeItem('userName');
    navigate('/');
  };
  const cancelLogout=()=>{
    navigate('/homepage');
  }

  return (
    <div className="MainLogout">
            <div className="wrapper2 wrapper">
                <div className="logoutConfirm title">
                    Are you sure you want to log out?
                </div>
                <form className="logoutForm">
                  <div className="field">
                    <input type="button" value="YES" onClick={handleLogout}/>
                  </div>
                  <div className="field">
                    <input type="button" value="NO" onClick={cancelLogout}/>
                  </div>
                </form>

            </div>
        </div>
  );
};

export default LogoutButton;