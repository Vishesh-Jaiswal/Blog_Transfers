import './App.css';
import LoginUser from './components/LoginUser';
import Register from './components/Register';
import Homepage from './components/Homepage';
import Navbar from './components/Navbar';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AddBook from './components/AddBook';
import GetAllBooks from './components/GetAllBooks';
import GetOneBook from './components/GetOneBook';

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginUser />} />
          <Route path='/register' element={<Register />} />
          <Route path='/homepage' element={<Homepage />} />
          <Route path='/navbar' element={<Navbar />} />
          <Route path='/addbook' element={<AddBook />} />
          <Route path='/getbooks' element={<GetAllBooks />} />
          <Route path='/getbookbyid/:bookId' element={<GetOneBook />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
