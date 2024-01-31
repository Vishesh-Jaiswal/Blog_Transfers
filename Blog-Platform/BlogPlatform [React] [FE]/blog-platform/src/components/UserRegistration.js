import { useState } from "react";
import './UserRegistration.css';
import axios from "axios";
import { Link } from "react-router-dom";

function RegisterUser(){
    const roles =["Blogger","Reader"];
    const [userEmail,setUserEmail] = useState("");
    const [userName,setUserName] = useState("");
    const [password,setPassword] = useState("");
    const [repassword,setrePassword] = useState("");
    const [role,setRole] = useState("");
    var setUsernameError=useState("");
    var checkUSerData = ()=>{
        if(userEmail==='')
        {
            setUsernameError("Username cannot be empty");
            return false;
        }
        if(userName==='')
        {
            setUsernameError("Username cannot be empty");
            return false;
        }
           
        if(password==='')
            return false;
        if(role==='Select Role')
            return false;
        return true;
    }
    const signUp = (event)=>{
        event.preventDefault();
        var checkData = checkUSerData();
        if(checkData===false)
        {
            alert('please check your data')
            return;
        }
        
        axios.post("http://localhost:5273/api/Blogger/Register",{
            userEmail:userEmail,
            userName: userName,
            role:	role,
            password:password
    })
        .then((userData)=>{
            console.log(userData)
            alert('Registration Successfull. Visit Login page');
            setUserEmail("");
            setUserName("");
            setPassword("");
            setrePassword("");
            setRole("");
        })
        .catch((err)=>{
            console.log(err)
        })
    }
    
    return(
        <div className="Main">
            <div className="wrapper regwrap">
            <div className="title">
            Register Now
            </div>
            <form className="registerForm">
            <div className="field">
                <input type="text" required value={userEmail}
                    onChange={(e) => { setUserEmail(e.target.value) }} />
                <label>Email Address</label>
            </div>
            <div className="field">
                <input type="text" required value={userName}
                    onChange={(e) => {setUserName(e.target.value)}}/>
                    <label>User Name</label>
            </div>
            <div className="field">
                <input type="password" required value={password}
                    onChange={(e) => { setPassword(e.target.value) }} />
                <label>Password</label>
            </div>
            <div className="field">
                    <input type="password" required  value={repassword}
                    onChange={(e) => {setrePassword(e.target.value)}}/>
                    <label>Re-Type Password</label>
            </div>
            <div className="field">
                <select className="form-select" onChange={(e)=>{setRole(e.target.value)}}>
                    <option className="options" value="select">Select Role</option>
                    {roles.map((r)=>
                        <option value={r} key={r}>{r}</option>
                    )}
                </select>
            </div>
            <div className="field">
                <input type="button" value="Sign Up" onClick={signUp}/>
            </div>
            <div className="signin-link">
                        Already a member? <Link to="/">SignIn now</Link>
                    </div>
            </form>
        </div>
     </div>
    );
}

export default RegisterUser;