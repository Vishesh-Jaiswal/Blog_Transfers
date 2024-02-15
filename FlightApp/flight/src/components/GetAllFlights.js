import {Link} from 'react-router-dom';
import axios from 'axios';
import { useEffect, useState } from 'react';
import './GetAllFlights.css';
import Navbar from './Navbar';

function GetAllFlights(){
    const [flights,setFlights]=useState([]);

    useEffect(()=>{
        const fecthFlights=()=>{
            const response=axios.get("http://localhost:5263/api/Flight/GetAllFlights")
            .then((response)=>{
                setFlights(response.data || []);
            })
            .catch((error)=>{
                console.log(error);
            })
            
        }
        fecthFlights();
    },[])
    return (
        <div className='back'>
            <Navbar/>
            <div className="flight-list-container">
        <div className="flightContents">
          <h2 className="PageTitle">Flights</h2>
          {Array.isArray(flights) && flights.length > 0 ? (
            <ul>
              {flights.map((flight) => (
                <li key={flight.flightId} className="flight-item">
                  <Link to={`/showflight/${flight.flightId}`} className="flight-link">
                    <h3 className="flight-title">{flight.airlines}</h3>
                  </Link>
                  <p>Price: {flight.price}</p>
                  <div className='datimes'>
                  <p>FROM:{flight.departureAirport}</p>
                    <p>TO:{flight.arrivalAirport}</p>
                  </div>
                  <div className='datimes'>
                  <p>Departure:{flight.departure}</p>
                    <p>Arrival:{flight.arrival}</p>
                  </div>
                    
                </li>
              ))}
            </ul>
          ) : (
            <p id="EmplyFlightList">No Flights Available</p>
          )}
        </div>
      </div>
        </div>
    )
}
export default GetAllFlights;