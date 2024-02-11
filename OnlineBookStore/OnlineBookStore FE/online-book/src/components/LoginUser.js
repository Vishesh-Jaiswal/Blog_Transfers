import { useState } from "react";
import axios from "axios";
import './LoginUser.css';
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
        <div class="wrapper bg-white">
    <div class="h2 text-center">Creativity</div>
    <div class="h4 text-muted text-center pt-2">Enter your login details</div>
    <form class="pt-3">
        <div class="form-group py-2">
            <div class="input-field"> <span class="far fa-user p-2"></span> <input type="text" placeholder="Username or Email Address" required class=""/> </div>
        </div>
        <div class="form-group py-1 pb-2">
            <div class="input-field"> <span class="fas fa-lock p-2"></span> <input type="text" placeholder="Enter your Password" required class=""/> <button class="btn bg-white text-muted"> <span class="far fa-eye-slash"></span> </button> </div>
        </div>
 <button class="btn btn-block text-center my-3">Log in</button>
        <div class="text-center pt-3 text-muted">Not a member? <a href="#">Sign up</a></div>
    </form>
</div>
    );
}

export default LoginUser;