import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../assets/css/chats.css';

const ChatsPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token);

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
        console.log(uid);
    }, [])

    const [messages, setMessages] = useState(null); 
    const [interval, setInterval] = useState(null); 
    const [message, setMessage] = useState('');

    var sendForm = document.getElementById('chat-form'); //Форма отправки
    var messageInput = document.getElementById('message-text'); //Инпут для текста сообщения

    return(
        <div className='chat'>
            <div className='chat-messages'>
                <div className='chat-messages__content' id='messages'>
                    {
                        messages?
                        <></>
                        :
                        <>
                            Загрузка...
                        </>
                    }
                </div>
            </div>
            <div className='chat-input'>
                <form method='post' id='chat-form'>
                    <input type='text' hidden={true} value="1" readOnly={true}/>
                    <input type='text' id='message-text' className='chat-form__input' placeholder='Введите сообщение' value={message} onChange={(e) => setMessage(e.target.value)}/> 
                    <input type='submit' className='chat-form__submit' value='=>' />
                </form>
            </div>
        </div>
    )
}

export default ChatsPage;