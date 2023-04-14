import './App.css';
import { Routes, Route } from 'react-router-dom';
import HeaderApp from './Components/header';
import RegisterPage from './Pages/register';
import LoginPage from './Pages/login';
import AdminPage from './Pages/admin/admin';
import HomePage from './Pages/home';
import CommunityPage from './Pages/community';
import ShopsPage from './Pages/shops';
import BlogsPage from './Pages/blogs';
import ContactPage from './Pages/contact';

function App() {
  return (
    <>
      <HeaderApp />
      <Routes>
        <Route path='/admin' element={<AdminPage />} />
        <Route path='/login' element={<LoginPage />} />
        <Route path='/home' element={<HomePage />} />
        <Route path='/community' element={<CommunityPage />} />
        <Route path='/shops' element={<ShopsPage />} />
        <Route path='/blogs' element={<BlogsPage />} />
        <Route path='/contact' element={<ContactPage />} />
        <Route path='*' element={<RegisterPage />} />
      </Routes>
    </>
  );
}

export default App;
