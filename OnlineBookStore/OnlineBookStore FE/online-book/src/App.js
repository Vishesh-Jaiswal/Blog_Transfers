import './App.css';
import LoginUser from './components/LoginUser';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginUser />} />
          </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
