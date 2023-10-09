import './../assets/css/chat_with_admin.css'
import jwtDecode from "jwt-decode";
import { axiosInstance } from "../Components/axios_server";
import 'https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js';
import * as signalR from "@microsoft/signalr";
import Cookies from "js-cookie";
import React, {useCallback, useEffect, useState} from 'react';
import { useNavigate, useParams } from "react-router-dom";

const ChatWindow = () => {
  const navigate = useNavigate();
  const [messages, setMessages] = useState([]);
  const token = Cookies.get('token');
  const [newMessage, setNewMessage] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const { nickname } = useParams();

  useEffect(() => {
    if (!token){
        navigate("/login");
    }
    }, [navigate, token])


  const handleSendMessage = (msg) => {
    if (msg.trim() !== '') {
        const mess = { 
            message : msg,
            author: 'me' 
        }
        const m2 = {
            message : msg,
            author: 'to'
        }
        setMessages([...messages, mess, m2]);
    }
  };
  const togglePopup = () => {
    setIsOpen(!isOpen);
  }

    const [message, setMessage] = useState('');
    const callbackSignalR = useCallback((roomData) => {
    
        let connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7015/chatHub").build();
    
        connection.on("ReceivePrivateMessage", function (user, message){
            var elem = document.createElement("div");
            var author = document.createElement("span");
            var content = document.createElement("span");
            if(user === nickname){
                elem.className="message-from";
    
                author.className = "message-from";
            }
            else{
                elem.className="message-to";
    
                author.className = "message-to";
            }
            author.textContent = user + ":";
    
            content.className = "message-text";
            content.textContent = message;
    
            elem.appendChild(author);
            elem.appendChild(content);
    
            document.getElementById("messagesList").appendChild(elem);
    
        });
    
        document.getElementById("sendButton").addEventListener("click", function (event) {
            var message = document.getElementById("messageInput").value;
            connection.invoke("SendPrivateMessage", `${roomData.senderName}`, message, `${roomData.recieverName}`, `${roomData.roomName}`).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }, [nickname])

    useEffect(() => {
        let room;
        axiosInstance.get(`/im/supportChat?username=${nickname}`,
            {
                headers:{
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(response => {
                room = response.data; // выводим данные, полученные из сервера
                callbackSignalR(room);
            })
            .catch();
        let messages;
        axiosInstance.get(`/history?username=${nickname}`,
            {
                headers:{
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(response => {
                messages = response.data; // выводим данные, полученные из сервера
                messages.forEach(msg => handleSendMessage(msg))
            })
            .catch();
    }, [callbackSignalR, nickname, token])

  return (
    <div>
      <button className='open-chat-button' onClick={togglePopup}>Связаться с администратором</button>
      {isOpen && (
        <div className="popup">
          <div className="popup-content">
            <div className='header-window'>
                <h5>Свяжитесь с администратором </h5>
                <button className='close-button' onClick={togglePopup}>&times;</button>
            </div>
                {messages.map((message, index) => (
                    <div className = "chatMessage">
                        {message.author === 'me' ?
                        <div key={index} className="chat-message-from">
                            {message.message}
                        </div>                  
                    :
                    <div className='adminBlock'>
                        <img src='https://fikiwiki.com/uploads/posts/2022-02/1644852415_12-fikiwiki-com-p-kartinki-admina-12.png' className='pngAdmin'></img>
                        <div key={index} className="chat-message-to">
                            {message.message}
                        </div>
                    </div>
                                                            
                    }                        
                    </div>
                    ))}
            <div>
                <input
                type="text"
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                />
                <button onClick={handleSendMessage}>Send</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ChatWindow;