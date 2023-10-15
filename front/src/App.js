import { Routes, Route } from 'react-router-dom';
import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { OAuthAfterCallback } from './Components/after_call_back';
import ChatForTwoPage from './Pages/chat_for_two';
import PageNotFound from './Pages/404';
import RegisterPage from './Pages/register';
import ContactPage from './Pages/contact';
import MyLikesPage from './Pages/my-likes';
import HeaderApp from './Components/header';
import LoginPage from './Pages/login';
import AdminPage from './Pages/admin/admin';
import ShopsPage from './Pages/shops';
import ChatsPage from './Pages/chats';
import HomePage from './Pages/home';
import LikePage from './Pages/LikePage';
import ProfilePage from './Pages/profile';
import ChatWindow from './Components/window_connect_with_admin';
import SupporChatsPage from './Pages/admin/support-chats';
import './assets/css/App.css';

function App() {
  
  let token = Cookies.get('token');
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
        <Route path='/search' element= {<LikePage/>}/>
        <Route path='/myLikes' element= {<MyLikesPage/>}/>
        <Route path='/register' element={<RegisterPage />} />
        <Route path='/profile' element={<ProfilePage />} />
        <Route path='/support_chat' element={<SupporChatsPage />} />
        <Route path='/profile' element={<ProfilePage />} />
        <Route path='/afterCallback' element={<OAuthAfterCallback />} />
        <Route path='*' element={<PageNotFound />}
        />
      </Routes>
      {!token || !jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].includes("Admin") ? <div></div> : <ChatWindow />}
    </>
  );
}   

export default App;
