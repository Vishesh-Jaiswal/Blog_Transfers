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
    const [userNameError,setUserNameError]=useState("");
    const [userEmailError,setUserEmailError]=useState("");
    const [validEmaid,setValidEmail]=useState(false);
    const [passwordError,setUserPasswordError]=useState("");
    const [repasswordError,setUserRePasswordError]=useState("");
    const emailFormat=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var checkUserData = ()=>{
        if(emailFormat.test(userEmail)){
            setValidEmail
        }
        if(userEmail==='')
        {
            setUserEmailError("User Email cannot be empty");
            setTimeout(() => setUserEmailError(""), 4000);
            return false;
        }
        if(userName==='')
        {
            setUserNameError("User Name cannot be empty");
            setTimeout(() => setUserNameError(""), 4000);
            return false;
        }
        if(password==='')
        {
            setUserPasswordError("Password cannot be empty");
            setTimeout(() => setUserPasswordError(""), 4000);
            return false;
        }
        if(password==='')
        {
            setUserRePasswordError("RePassword cannot be empty");
            setTimeout(() => setUserRePasswordError(""), 4000);
            return false;
        }
           
    }
    const signUp = (event)=>{
        event.preventDefault();
        var checkData = checkUserData();
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
                    {userEmailError && <label className="alert alert-danger">{userEmailError}</label>}
                <label>Email Address</label>
            </div>
            <div className="field">
                <input type="text" required value={userName}
                    onChange={(e) => {setUserName(e.target.value)}}/>
                   {userNameError && <label className="alert alert-danger">{userNameError}</label>}
                <label>User Name</label>
            </div>
            <div className="field">
                <input type="password" required value={password}
                    onChange={(e) => { setPassword(e.target.value) }} />
                    {passwordError && <label className="alert alert-danger">{passwordError}</label>}
                <label>Password</label>
            </div>
            <div className="field">
                    <input type="password" required  value={repassword}
                    onChange={(e) => {setrePassword(e.target.value)}}/>
                    {repasswordError && <label className="alert alert-danger">{repasswordError}</label>}
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