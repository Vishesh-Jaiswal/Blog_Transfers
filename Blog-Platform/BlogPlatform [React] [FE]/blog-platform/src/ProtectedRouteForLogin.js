import { Navigate } from "react-router-dom";

function ProtectedRouteForLogin({children}){

    var token = localStorage.getItem("token");
    if(token!==null){
        return <Navigate to="/homepage"/>
    }
    return children;
}

export default ProtectedRouteForLogin;