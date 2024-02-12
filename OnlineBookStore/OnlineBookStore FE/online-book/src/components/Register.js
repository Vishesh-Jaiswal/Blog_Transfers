import { useState } from "react";
import axios from "axios";
import './LoginUser.css';
import { Link, useNavigate } from "react-router-dom";

function Register() {
    const [userEmail, setUserEmail] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [repassword, setRePassword] = useState("");
    const [role, setRole] = useState("");
    const navigate = useNavigate();


    const signup = (event)=>{
        event.preventDefault();
 
        axios.post("http://localhost:5204/api/User/Register", {
            userEmail: userEmail,
            userName:userName,
            password: password,
            repassword:repassword,
            role:role
        })
        .then((userData)=>{
            navigate('/');
        })
        .catch((err)=>{
            console.log(err)
        })
    }

    return (
        <div className="wrapper bg-white">
            <div className="h2 text-center">Online Book App</div>
            <div className="h4 text-muted text-center pt-2">Register With Us</div>
            <form className="pt-3">
                <div className="form-group py-2">
                    <div className="input-field"> <span className="far fa-user p-2"></span>
                    <input type="text" value={userEmail} placeholder="Email Address" required className=""
                    onChange={(e)=>setUserEmail(e.target.value)}/> </div>
                </div>
                <div class="form-group py-2">
                    <div class="input-field"> <span className="far fa-user p-2"></span>
                    <input type="text" value={userName} placeholder="User Name" required className=""
                    onChange={(e)=>setUserName(e.target.value)}/> </div>
                </div>
                <div className="form-group py-1 pb-2">
                    <div className="input-field"> <span className="fas fa-lock p-2"></span>
                    <input type="password" placeholder="Enter your Password" value={password} required className=""
                    onChange={(e)=>setPassword(e.target.value)}/>  </div>
                </div>
                <div className="form-group py-1 pb-2">
                    <div className="input-field"> <span className="fas fa-lock p-2"></span>
                    <input type="password" placeholder="Enter your Password Agin" value={repassword} required className=""
                    onChange={(e)=>setRePassword(e.target.value)}/></div>
                </div>
                <div class="form-group py-1 pb-2">
                
                    <select required className="" value={role} onChange={(e)=>setRole(e.target.value)}>
                        <option selected>Select Role</option>
                        <option >Admin</option>
                        <option>User</option>
                    </select>
                 
                </div>
                <button className="btn btn-block text-center my-3" onClick={signup}>Sign Up</button>
                <div className="text-center pt-3 text-muted">Not a member? <Link to={'/'}>Log In</Link></div>
            </form>
        </div>
    );
}

export default Register;