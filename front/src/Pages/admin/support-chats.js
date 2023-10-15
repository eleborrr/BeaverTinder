import React, { useEffect, useState, useCallback } from "react";
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
    const getAllUsersAxios = useCallback(() => {
        axiosInstance.get('/all',
        {
            headers: {
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {
            console.log(res.data);
        })
    }, [token])

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
            if (element === "Moderator")
            {
                setIsAvailable(true);
            }
            return element;
        })
        }
        else
        {
            setIsAvailable(roles === "Moderator" || roles === "Admin");
        }

        getAllUsersAxios();

    }, [getAllUsersAxios, token])

    useEffect(() => {
        axiosInstance.get('/supportChats',
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
        <div>
        {isAvailable? 
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
                        <li onClick={() => navigate(`/support_chat/${chat.userName}`)}>
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