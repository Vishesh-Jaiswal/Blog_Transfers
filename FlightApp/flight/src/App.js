import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import LoginUser from './components/LoginUser';
import Homepage from './components/Homepage';
import RegisterUser from './components/RegisterUser';
import AddFlight from './components/AddFlight';
import GetAllFlights from './components/GetAllFlights';
import GetFlightById from './components/GetFlightById';
import UpdateFlight from './components/UpdateFlight';
import LogoutButton from './components/LogoutButton';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginUser/>}/>
          <Route path='/register' element={<RegisterUser/>}/>
          <Route path='/homepage' element={<Homepage/>}/>
          <Route path='/logout' element={<LogoutButton/>}/>
          <Route path='/addflight' element={<AddFlight/>}/>
          <Route path='/getallflights' element={<GetAllFlights/>}/>
          <Route path="/showflight/:flightId" element={<GetFlightById/>}/>
          <Route path="/update/:flightId" element={<UpdateFlight/>}/>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
