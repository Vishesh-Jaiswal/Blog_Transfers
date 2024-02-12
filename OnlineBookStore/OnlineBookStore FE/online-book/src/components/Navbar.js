import { Link } from "react-router-dom";
function Navbar(){
    return(
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-light navbbar">
                <div>
                <Link to={'/homepage'} class="navbar-brand" href="#">Navbar</Link>
                </div>
                

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                    <li class="nav-item active">
                        <Link to={'/addbook'} class="nav-link">Add Book </Link>
                    </li>
                 
                    
                   
                    </ul>
                   
                </div>
                </nav>
        </div>
    );
};
export default Navbar;