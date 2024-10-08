import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { axiosInstance } from "../../Components/axios_server";
import PageNotFound from './../404';
import './../../assets/css/chats.css';

const SupporChatsPage = () => {

    const navigate = useNavigate();
    const token = Cookies.get('token');
    const [isAvailable, setIsAvailable] = useState(false);
    
    const [searchInput, setSearchInput] = useState('');
    const [searchNone, setSearchNone] = useState(true);
    const [chats, setChats] = useState([]);

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    useEffect( () => {   
        const roles = jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];    
        if(Array.isArray(roles))
        {
        roles.some(element => {
            if (element === "Moderator" || element === "Admin")
            {
                setIsAvailable(true);
            }
        })
        }
        else
        {
            setIsAvailable(roles === "Moderator" || roles === "Admin");
        }

    }, [token])

    useEffect(() => {
        axiosInstance.get('/supportChats',
         {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(chats => {setChats(chats.data); console.log(chats)})
        .catch(err => console.log(err));
    }, [token])

    const onPressEnter = (e) => {
        if (e.key === 'Enter'){
            setSearchInput('');
        }
    }

    return(
        <div>
        {isAvailable? 
        <>
            <div className='chat'>
                <div className="ref-back">
                    <a href="/admin" className="backto-home"><i className="fas fa-chevron-left"></i> Back to admin panel</a>
                </div>
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
                        <li onClick={() => navigate(`/support_chat/${chat.userName}`)} key={chat.userName}>
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
        : 
        <PageNotFound />
        }
        </div>
    )
}

export default SupporChatsPage;