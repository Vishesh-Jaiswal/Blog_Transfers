import { Navigate } from "react-router-dom";

function ProtectedRoute({children}){

    var token = localStorage.getItem("token");
    if(!token){
        return <Navigate to="/"/>
    }
    return children;
}

export default ProtectedRoute;