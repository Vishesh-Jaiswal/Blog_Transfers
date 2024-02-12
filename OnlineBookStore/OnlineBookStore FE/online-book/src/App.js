import './App.css';
import LoginUser from './components/LoginUser';
import Register from './components/Register';
import Homepage from './components/Homepage';
import Navbar from './components/Navbar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginUser />} />
          <Route path='/register' element={<Register />} />
          <Route path='/homepage' element={<Homepage />} />
          <Route path='/navbar' element={<Navbar />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
