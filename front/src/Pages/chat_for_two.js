import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../assets/css/chat_for_two.css';

const ChatForTwoPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const { nickname } = useParams();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    const [messages, setMessages] = useState(null); 
    const [interval, setInterval] = useState(null); 
    const [message, setMessage] = useState('');

    function SendForm() {

    }

    return(
        <div className='chat'>
            <div className="ref-back">
                    <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
                </div>
            <div className='chat-messages'>
                
                <div className='chat-messages__content' id='messages'>
                    
                    {
                        messages ?
                        <> {console.log('messages')}</>
                        :
                        <>
                            <h5>
                                History messages is clear...
                            </h5>
                            <div className="message-from">
                                <span className="message-from">Глеб: </span>
                                <span className="message-text">Первое сообщение</span>
                            </div>
                            <div className="message-to">
                                <span className="message-from">Глеб: </span>
                                <span className="message-text">Первое сообщение</span>
                            </div>
                        </>
                    }
                </div>
            </div>
            <div className='chat-input'>
                <form method='post' id='chat-form' className="form-input">
                    <input type='text' hidden={true} value={uid} readOnly={true}/>
                    <input type='text' autoComplete="off" id='message-text' className='chat-form__input' placeholder='Введите сообщение' value={message} onChange={(e) => setMessage(e.target.value)}/> 
                    <input type='submit' className='chat-form__submit' value='Send' />
                </form>
            </div>
                    
        </div>
    )
}

export default ChatForTwoPage;