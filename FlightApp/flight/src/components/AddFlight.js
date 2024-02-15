import Navbar from "./Navbar";
import './AddFlight.css';
import { useState } from "react";
import axios from "axios";
import {useNavigate} from 'react-router-dom';

function AddFlight(){
    const [airlines,setAirLines]=useState("");
    const [departureAirport,setdepartureAirport]=useState("");
    const [arrivalAirport,setarrivalAirport]=useState("");
    const [departure,setDeparture]=useState("");
    const [arrival,setArrival]=useState("");
    const [price, setPrice]=useState("");
    const [economy,setEconomy]=useState("");
    const [business,setBusiness]=useState("");
    const [firstClass,setfristClass]=useState("");
    var currentEmail=localStorage.getItem('userEmail');
    const [formerror,setfromError]=useState("");
    const navigate = useNavigate();

    var checkUserData = ()=>{
        if(airlines===''){
            setfromError("Please fill form correctly: No Airline Entered");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
        if(departureAirport===''){
            setfromError("Please fill form correctly: No DEP Airport");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
        if(arrivalAirport===''){
            setfromError("Please fill form correctly: No Arr Airport");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
        if(arrival===''){
            setfromError("Please fill form correctly: No Arrival Time");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
        if(departure===''){
            setfromError("Please fill form correctly: No Dep Time");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
        if(price==='' || economy==='' || business==='' || firstClass===''){
            setfromError("Please fill form correctly: Fill Seat and Price");
            setTimeout(()=> setfromError(""),4000);
            return false;
        }
    }

    const handleAddFlight=(event)=>{
        event.preventDefault();
        var checkData = checkUserData();
        if(checkData===false)
        {
            alert('please check your data')
            return;
        }


        axios.post("http://localhost:5263/api/Flight/AddFlight",{
            airlines:airlines,
            departureAirport:departureAirport,
            arrivalAirport:arrivalAirport,
            departure:departure,
            arrival:arrival,
            price:price,
            economy:economy,
            business:business,
            firstClass:firstClass
        })
        .then(()=>{
            alert('Flight Added Successfully');
            navigate('/getallflights')
        })
        .catch((error)=>{
            console.log(error);
        })
    }

    return(
        <div>
            <Navbar/>
            <form>
                {formerror}
            <div class="form-row align-items-center extra">
            <div class="col-auto my-1">
                <label class="mr-sm-2" for="inlineFormCustomSelect">AirLines</label>
                <select class="custom-select mr-sm-2" id="inlineFormCustomSelect" value={airlines} onChange={(e) => {setAirLines(e.target.value)}}>
                    <option selected>Choose</option>
                    <option >IndiGO</option>
                    <option >AirIndia</option>
                    <option >Magma</option>
                </select>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Departure Airport</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                 
                    </div>
                    <input type="text" class="form-control" id="inlineFormInputGroup" placeholder="Departure Airport"
                    value={departureAirport} onChange={(e) => {setdepartureAirport(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Arrival Airport</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                 
                    </div>
                    <input type="text" class="form-control" id="inlineFormInputGroup" placeholder="Arrival Airport"
                    value={arrivalAirport} onChange={(e) => {setarrivalAirport(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Departure</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                 
                    </div>
                    <input type="datetime-local" class="form-control" id="inlineFormInputGroup" placeholder="Departure"
                    value={departure} onChange={(e) => {setDeparture(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Arrival time</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                 
                    </div>
                    <input type="datetime-local" class="form-control" id="inlineFormInputGroup" placeholder="Arrival"
                    value={arrival} onChange={(e) => {setArrival(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Economy</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                    </div>
                    <input type="number" class="form-control" id="inlineFormInputGroup" placeholder="Economy"
                    value={economy} onChange={(e) => {setEconomy(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Business</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                    </div>
                    <input type="number" class="form-control" id="inlineFormInputGroup" placeholder="Busniness"
                    value={business} onChange={(e) => {setBusiness(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">First Class</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                    </div>
                    <input type="number" class="form-control" id="inlineFormInputGroup" placeholder="First Class"
                    value={firstClass} onChange={(e) => {setfristClass(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <label class="sr-only" for="inlineFormInputGroup">Price</label>
                <div class="input-group mb-2">
                    <div class="input-group-prepend">
                    </div>
                    <input type="number" class="form-control" id="inlineFormInputGroup" placeholder="Price"
                    value={price} onChange={(e) => {setPrice(e.target.value)}}/>
                </div>
                </div>
                <div class="col-auto">
                <button type="submit" class="btn btn-primary mb-2" onClick={handleAddFlight}>Submit</button>
                </div>
            </div>
            </form>
        </div>
    )
}
export default AddFlight;