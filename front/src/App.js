import './App.css';
import { Routes, Route } from 'react-router-dom';
import HeaderApp from './Components/header';
import RegisterPage from './Pages/register';
import LoginPage from './Pages/login';
import AdminPage from './Pages/admin/admin';
import LikePage from './Pages/LikePage';

function App() {
  return (
    <>
      <HeaderApp />
      <Routes>
        <Route path='/like' element= {<LikePage/>}/>
        <Route path='/admin' element={<AdminPage />} />
        <Route path='/login' element={<LoginPage />} />
        <Route path='*' element={<RegisterPage />} />
        
      </Routes>
    </>
  );
}

export default App;
