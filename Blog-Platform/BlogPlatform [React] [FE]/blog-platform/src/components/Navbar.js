import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

function Navbar() {
  var role = localStorage.getItem('role');
  var userName=localStorage.getItem('userName');
  var userEmail=localStorage.getItem('userEmail');

  return (
    <div className='MainNav'>
      
        <nav className="navbar">
          <div className='logo'>
          <Link className="navbarlogo" to={'/homepage'}>
            Blaze Blogs
          </Link>
          </div>
          <ul className="nav-links">
          <input type="checkbox" id="checkbox_toggle" />
          <label for="checkbox_toggle" className="hamburger">&#9776;</label>
          <div className='menu'>
            {role === 'Blogger' && (<li className="nav-item">
                <Link className="nav-link" to="/blogs">
                  Post Blogs
                </Link>
            </li>)}
            <li className="nav-item">
              <Link className="nav-link" to="/showblogs">
                Show Blogs
              </Link>
            </li>
            <li className="nav-item">
                <Link className="nav-link" to={`/profile/${userEmail}`}>
                  {userName}
                </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/logout">
                Logout
              </Link>
            </li>
            </div>
          </ul>
    </nav>
    </div>
    
  );
}

export default Navbar;