import { Routes, Route } from 'react-router-dom';
import ChatForTwoPage from './Pages/chat_for_two';
import PageNotFound from './Pages/404';
import RegisterPage from './Pages/register';
import ContactPage from './Pages/contact';
import HeaderApp from './Components/header';
import LoginPage from './Pages/login';
import AdminPage from './Pages/admin/admin';
import ShopsPage from './Pages/shops';
import ChatsPage from './Pages/chats';
import HomePage from './Pages/home';
import LikePage from './Pages/LikePage';
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
        <Route path='/chats/:nickname' element={<ChatForTwoPage />} />
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
