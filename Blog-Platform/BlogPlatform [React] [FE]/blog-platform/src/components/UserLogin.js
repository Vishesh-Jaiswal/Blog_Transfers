import { useState } from "react";
import './UserLogin.css';
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

function LoginUser() {
    const [userEmail, setUserEmail] = useState("");
    const [password, setPassword] = useState("");
    const [userEmailError,setUserEmailError]=useState("");
    const [validEmail,setValidEmail]=useState(false);
    const emailFormat=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const navigate = useNavigate();

    var checkUserData = ()=>{
        if(userEmail==='')
        {
            setUserEmailError("User Email cannot be empty");
            setTimeout(() => setUserEmailError(""), 4000);
            return false;
        }
        if(!emailFormat.test(userEmail)){
            setValidEmail("Invalid Email a big line that says something");
            setTimeout(() => setValidEmail(""), 4000);
            return false;
        }
    }

    const userLogin = (event)=>{
        event.preventDefault();
        var checkData = checkUserData();
        if(checkData===false)
        {
            alert('please check your data')
            return;
        }
 
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
                <form className="loginForm">
                <div className="field">
                    <input type="text" required value={userEmail}
                        onChange={(e) => { setUserEmail(e.target.value) }} />
                        {userEmailError && <label className="alert alert-danger">{userEmailError}</label>}
                        {!userEmailError && validEmail && <label className="alert alert-danger">{validEmail}</label>}
                        {!userEmailError && !validEmail && (<label>Email Address</label>)}
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
