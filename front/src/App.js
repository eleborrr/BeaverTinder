import { Routes, Route } from 'react-router-dom';
import HeaderApp from './Components/header';
import RegisterPage from './Pages/register';
import LoginPage from './Pages/login';
import AdminPage from './Pages/admin/admin';
import HomePage from './Pages/home';
import ShopsPage from './Pages/shops';
import ContactPage from './Pages/contact';
import LikePage from './Pages/LikePage';
import PageNotFound from './Pages/404';
import ChatsPage from './Pages/chats';
import './assets/css/App.css';

function App() {
  return (
    <>
      <HeaderApp />
      <Routes>
        <Route path='/admin' element={<AdminPage />} />
        <Route path='/login' element={<LoginPage />} />
        <Route path='/home' element={<HomePage />} />
        <Route path='/chats' element={<ChatsPage />} />
        <Route path='/shops' element={<ShopsPage />} />
        <Route path='/contact' element={<ContactPage />} />
        <Route path='/like' element= {<LikePage/>}/>
        <Route path='/register' element={<RegisterPage />} />
        <Route path='*' element={<PageNotFound />}
        />
      </Routes>
    </>
  );
}

export default App;
