import Navbar from "./Navbar";
import {useParams,Link,useNavigate} from 'react-router-dom';
import React, { useState, useEffect } from "react";
import axios from "axios";
import './GetFlightById.css';
import moment from "moment/moment";


function GetFlightById(){
    const {flightId}=useParams();
    const [flight,setFlight]=useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchFlight = async () => {
          try {
            const flightResponse = await axios.get(`http://localhost:5263/api/Flight/GetFlightById/${flightId}`);
            setFlight(flightResponse.data);
    
              
          } catch (error) {
              if (error.response) {
                console.error(`Error for API call: ${error.config.url}`, error.response.data);
              } else if (error.request) {
                console.error(`No response received for API call: ${error.config.url}`);
              } else {
                console.error(`Error setting up the API call:`, error.message);
              }
            }
          };
      
          fetchFlight();
        }, [flightId]);

    const handleDelete=()=>{
        axios.delete(`http://localhost:5263/api/Flight/DeleteFlight/${flightId}`,{
            flightId:flightId
        })
        .then((response)=>{
            alert('Deleted')
            navigate('/homepage')
        }).catch((erroe)=>{
            alert('Could Not delete')
        })
    }

    return(
        <div>
            <Navbar/>
            <div class="card extraForCard">
                <div class="card-body">
                    <h5 class="card-title">{flight?.airlines}</h5>
                    <div className="time">
                      <p><b>Arrival:</b>
                      {flight?.arrival
                          ? moment(flight?.arrival).format("DD/MM/YY")
                          : "N/A"}
                      </p>
                      <p><b>Departure:</b>
                      {flight?.arrival
                          ? moment(flight?.departure).format("DD/MM/YY")
                          : "N/A"}
                      </p>
                    </div>
                    <div className="time">
                      <p><b>From:</b>
                      {flight?.departureAirport	}
                      </p>
                      <p><b>To:</b>
                      {flight?.arrivalAirport	}
                      </p>
                    </div>
                    <div>
                      <p>Economy:{flight?.economy}</p>
                      <p>Business:{flight?.business}</p>
                      <p>First Class:{flight?.firstClass}</p>
                    </div>
                    <div className="buttons">
                    <Link to={`/update/${flightId}`} class="btn btn-primary">Update Flight</Link>
                    <button class="btn btn-primary" onClick={handleDelete}>Delete Flight</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default GetFlightById;