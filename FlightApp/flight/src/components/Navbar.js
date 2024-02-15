import {Link } from 'react-router-dom';
function Navbar(){
    return(
        <div>
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <Link class="navbar-brand" to={'/homepage'}>FlightApp</Link>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                    <li class="nav-item active">
                        <Link class="nav-link"to={'/addflight'}>Add Flight </Link>
                    </li>
                    <li class="nav-item active">
                        <Link class="nav-link"to={'/getallflights'}>Get All Flights</Link>
                    </li>
                    <li class="nav-item active">
                        <Link class="nav-link"to={'/logout'}>Logout</Link>
                    </li>
                    </ul>
                </div>
            </nav>
        </div>
    )
}
export default Navbar;