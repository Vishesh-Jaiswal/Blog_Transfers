import { useState } from "react";
import axios from "axios";
import './LoginUser.css';
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
 
        const response=axios.post("http://localhost:5263/api/User/Login", {
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
            if(err.response && err.response.status===401)
            alert(err.response.data)
            console.log(err)
        })
    }

    return (
        <div class="wrapper bg-white">
            <div class="h2 text-center">Flight Management</div>
            <div class="h4 text-muted text-center pt-2">Enter your login details</div>
            <form class="pt-3">
                <div className="form-group py-2"><div class="input-field"> <span class="far fa-user p-2"></span>
                    <input type="text" required value={userEmail}
                        onChange={(e) => { setUserEmail(e.target.value) }} /></div>
                        {userEmailError && <label className="alert alert-danger">{userEmailError}</label>}
                        {!userEmailError && validEmail && <label className="alert alert-danger">{validEmail}</label>}
                        {!userEmailError && !validEmail && (<label>Email Address</label>)}
                </div>
                    <div className="form-group py-1 pb-2">
                    <div class="input-field"> <span class="far fa-user p-2"></span>
                        <input type="password" required value={password}
                            onChange={(e) => { setPassword(e.target.value) }} /></div>
                        <label>Password</label>
                    </div>
                    <div className="field">
                        <input className="btn btn-block text-center my-3" type="button" value="Login" onClick={userLogin}/>
                    </div>
                    <div className="text-center pt-3 text-muted">
                        Not a member? <Link to="/register">Signup now</Link>
                    </div>
                </form>
            </div>

    );
}

export default LoginUser;