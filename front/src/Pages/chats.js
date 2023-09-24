import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import { axiosInstance } from "../Components/axios_server";
import './../assets/css/chats.css';

const ChatsPage = () => {

    const navigate = useNavigate();
    const token = Cookies.get('token');
    
    const [searchInput, setSearchInput] = useState('');
    const [searchNone, setSearchNone] = useState(true);
    const [chats, setChats] = useState([]);

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
        if(!token){
            navigate("/login");
        }
    }, [navigate, token])

    useEffect(() => {
        axiosInstance.get('/im',
         {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(chats => {setChats(chats.data)})
    }, [token])

    const onPressEnter = (e) => {
        if (e.key === 'Enter'){
            setSearchInput('');
        }
    }

    return(
        <>
            <div className='chat'>
                <header>
                    <h2 className='title'>
                        <span className="text">Your Chats:</span>
                    </h2>
                    <ul className='tools'>
                        <li>
                            <a href="/" className={searchNone? 'fa fa-search' : 'fa fa-search fa-close'} onClick={() => setSearchNone(!searchNone)}> </a>
                        </li>
                    </ul>
                </header>
                <div className='body'>
                    <div className="search" style={{display: searchNone? "none" : "block"}}>
                        <input placeholder='Search...' type='text' value={searchInput} onChange={(e) => setSearchInput(e.target.value)} onKeyPress={(e) => onPressEnter(e)}/>
                    </div>
                    <ul>
                    {chats.map(chat => (
                        <li onClick={() => navigate(`${chat.userName}`)}>
                            <a href="/" className='thumbnail'>
                                <img alt="chat icon" src = {chat.image}/> 
                            </a>
                        <div className='content'>
                            <h3>{chat.firstName} {chat.lastName}</h3>
                        </div>
                    </li>
                    ))}    
                    
                    </ul>
                </div>
            </div>
        </>

    )
}

export default ChatsPage;