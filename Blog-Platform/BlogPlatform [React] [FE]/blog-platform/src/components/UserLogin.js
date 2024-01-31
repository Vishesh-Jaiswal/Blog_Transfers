import { useState } from "react";
import './UserLogin.css';
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

function LoginUser() {
    const [userEmail, setUserEmail] = useState("");
    const [password, setPassword] = useState("");
    const [UserEmailError,setUserEmailError]=useState("");
    const navigate = useNavigate();


    const userLogin = (event)=>{
        event.preventDefault();
 
        axios.post("http://localhost:5273/api/Blogger/Login", {
            userEmail: userEmail,
            password: password
        })
        .then((userData)=>{
            console.log(userData); 
            var token = userData.data.token;
            localStorage.setItem("token",token);
            var role=userData.data.role;
            localStorage.setItem("role",role);
            var userEmail=userData.data.userEmail;
            localStorage.setItem("userEmail",userEmail);
            var userName=userData.data.userName;
            localStorage.setItem("userName",userName);
            navigate('/homepage');
        })
        .catch((err)=>{
            console.log(err)
        })
    }

    return (
        <div className="Main">
            <div className="wrapper logwrap">
                <div className="title">
                    Login Form
                </div>
                <form className="loginForm row g-3">
                    <div className="field col-md-4">
                        <input className="form-control" type="email" id="validationDefault02" required value={userEmail}
                            onChange={(e) => { setUserEmail(e.target.value) }}  pattern="[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$"/>
                        <label for="validationDefaultUsername" className="form-label">Email Address</label>
                    </div>
                    <div className="field">
                        <input type="password" required value={password}
                            onChange={(e) => { setPassword(e.target.value) }} />
                        <label>Password</label>
                    </div>
                    <div className="field">
                        <input type="button" value="Login" onClick={userLogin}/>
                    </div>
                    <div className="signup-link">
                        Not a member? <Link to="/register">Signup now</Link>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default LoginUser;
