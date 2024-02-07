import RegisterUser from './components/UserRegistration';
import LoginUser from './components/UserLogin';
import NewBlog from './components/NewBlog';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ProtectedRoute from './ProtectedRoute';
import LogoutButton from './components/LogoutButton';
import GetBlog from './components/GetBlogs';
import BlogView from './components/BlogView';
import NewComment from './components/NewBlog';
import Homepage from './components/Homepage';
import Profile from './components/Profile';
import GetReportedBlogs from './components/GetReportedBlogs';
import GetReportedComments from './components/GetReportedComments';
import ReportedBlog from './components/ReportedBlog';


function App() {
  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginUser />} />
          <Route path='/register' element={<RegisterUser/>}/>
          <Route path='/homepage' element={<ProtectedRoute><Homepage/></ProtectedRoute>}/>
          <Route path='/profile/:userEmail' element={<ProtectedRoute><Profile/></ProtectedRoute>}/>
          <Route path="/blogs" element={<ProtectedRoute><NewBlog /></ProtectedRoute>} />
          <Route path="/reportedblogs" element={<ProtectedRoute><GetReportedBlogs /></ProtectedRoute>} />
          <Route path="/reportedcomments" element={<ProtectedRoute><GetReportedComments /></ProtectedRoute>} />
          <Route path='/showblogs' element={<ProtectedRoute><GetBlog /></ProtectedRoute>} />
          <Route path="/showblogs/:blogId" element={<ProtectedRoute><BlogView /></ProtectedRoute>} />
          <Route path="/showreportedblog/:blogId" element={<ProtectedRoute><ReportedBlog /></ProtectedRoute>} />
          <Route path="/logout" element={<ProtectedRoute><LogoutButton /></ProtectedRoute>} />
          <Route path="/newcomment" element={<ProtectedRoute><NewComment/></ProtectedRoute>}/>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
