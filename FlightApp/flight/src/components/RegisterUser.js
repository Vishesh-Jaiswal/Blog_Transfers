import { useState } from "react";
import axios from "axios";
import { Link,useNavigate } from "react-router-dom";

function RegisterUser(){
    const navigate = useNavigate();
    const roles =["User"];
    const [userEmail,setUserEmail] = useState("");
    const [userName,setUserName] = useState("");
    const [password,setPassword] = useState("");
    const [repassword,setrePassword] = useState("");
    const [role,setRole] = useState("");
    const [userNameError,setUserNameError]=useState("");
    const [userEmailError,setUserEmailError]=useState("");
    const [validEmail,setValidEmail]=useState(false);
    const [validName,setValidName]=useState(false);
    const [passwordError,setUserPasswordError]=useState("");
    const [repasswordError,setUserRePasswordError]=useState("");
    const [noRoleError,setNoRoleError]=useState("");
    const [passMismatch,setPassMismatch]=useState("");
    const emailFormat=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const nameFormat=/^[a-zA-Z\s]*$/;
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
        
        if(userName==='')
        {
            setUserNameError("User Name cannot be empty");
            setTimeout(() => setUserNameError(""), 4000);
            return false;
        }
        if(!nameFormat.test(userName)){
            setValidName("Give Proper Name");
            setTimeout(() => setValidName(""), 4000);
            return false;
        }
        if(password==='')
        {
            setUserPasswordError("Password cannot be empty");
            setTimeout(() => setUserPasswordError(""), 4000);
            return false;
        }
        if(repassword==='')
        {
            setUserRePasswordError("RePassword cannot be empty");
            setTimeout(() => setUserRePasswordError(""), 4000);
            return false;
        }
        if(password!==repassword){
            setPassMismatch("Passwords do not match");
            setTimeout(()=> setPassMismatch(""),4000);
            return false;
        }
        if(role!=='User'){
            setNoRoleError("No Role Selected");
            setTimeout(()=> setNoRoleError(""),4000);
            return false;
        }
           
    }
    const signUp = async (event)=>{
        event.preventDefault();
        var checkData = checkUserData();
        if(checkData===false)
        {
            alert('please check your data')
            return;
        }
        
        axios.post("http://localhost:5263/api/User/Register",{
            userEmail:userEmail,
            userName: userName,
            role:	role,
            password:password
    })
        .then((userData)=>{
            console.log(userData)
            alert('Registration Successfull. Visit Login page');
            navigate('/');
        })
        .catch((err)=>{
            if(err.response && err.response.status === 400)
            alert(err.response.data)
            console.log(err)
        })
    }

    
    return(
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
                    <div className="form-group py-2"><div class="input-field"> <span class="far fa-user p-2"></span>
                        <input type="text" required value={userName}
                            onChange={(e) => {setUserName(e.target.value)}}/></div>
                            {userNameError && <label className="alert alert-danger">{userNameError}</label>}
                            {!userNameError && validName && <label className="alert alert-danger">{validName}</label>}
                            {!userNameError && !validName && (<label>User Name</label>)}
                    </div>
                    <div className="form-group py-2"><div class="input-field"> <span class="far fa-user p-2"></span>
                        <input type="password" required value={password}
                            onChange={(e) => { setPassword(e.target.value) }} /></div>
                            {passwordError && <label className="alert alert-danger">{passwordError}</label>}
                        <label>Password</label>
                    </div>
                    <div className="form-group py-2"><div class="input-field"> <span class="far fa-user p-2"></span>
                            <input type="password" required  value={repassword}
                            onChange={(e) => {setrePassword(e.target.value)}}/></div>
                            {repasswordError && <label className="alert alert-danger">{repasswordError}</label>}
                            <label>Re-Type Password</label>
                    </div>
                    {passMismatch && <label className="alert alert-danger">{passMismatch}</label>}
                    <div className="field">
                        <select className="form-select" onChange={(e)=>{setRole(e.target.value)}}>
                            <option className="options" value="select">Select Role</option>
                            {roles.map((r)=>
                                <option value={r} key={r}>{r}</option>
                            )}
                        </select>
                    </div>
                    {noRoleError && <label className="alert alert-danger">{noRoleError}</label>}
                    <div className="field">
                        <input type="button" className="btn btn-block text-center my-3" value="Sign Up" onClick={signUp}/>
                    </div>
                    <div className="signin-link">
                        Already a member? <Link to="/">SignIn now</Link>
                    </div>
                </form>
            </div>
    );
}

export default RegisterUser;