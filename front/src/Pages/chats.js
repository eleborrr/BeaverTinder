import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import { axiosInstance } from "../Components/axios_server";
import './../assets/css/chats.css';
import { to } from "@react-spring/web";

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
    }, [])

    useEffect(() => {
        axiosInstance.get('/im',
         {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(chats => setChats(chats.data))
    }, [])

    const onPressEnter = (e) => {
        if (e.key === 'Enter'){
            console.log('activate search');
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
                            <a className={searchNone? 'fa fa-search' : 'fa fa-search fa-close'} onClick={() => setSearchNone(!searchNone)}></a>
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
                        <a className='thumbnail'>
                        image?
                        </a>
                        <div className='content'>
                        <h3>{chat.firstName} {chat.lastName}</h3>
                        <span className='preview'>hey how are things going on the...</span>
                        <span className='meta'>
                            2h ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
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