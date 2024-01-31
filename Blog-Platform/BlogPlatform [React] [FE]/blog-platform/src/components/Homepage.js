import React from 'react';
import Navbar from './Navbar';
import { Link } from 'react-router-dom';
import './Homepage.css';

function Homepage() {
  const userName =localStorage.getItem('userName');
  return (
    <div className='homepage'>
         <Navbar />
         <div className='subprofile'>

         
         <div className='box'>
            <h1 id='welome'>Welcome {userName}</h1>
            <p>Start Your Journey - Unveiling Stories, Ideas, and Creativity</p>
            <Link class="btn" to={'/showblogs'}>
						<span class="btn-content">Explore us</span>
						<span class="icon"><i class="fa fa-arrow-right" aria-hidden="true"></i></span>
					</Link>
         </div>
         </div>
    </div>
  );
}

export default Homepage;