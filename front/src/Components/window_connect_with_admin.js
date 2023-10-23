import { axiosInstance } from "../Components/axios_server";
import * as signalR from "@microsoft/signalr";
import Cookies from "js-cookie";
import React, { useCallback, useEffect, useState, useRef} from 'react';
import { useParams } from "react-router-dom";
import './../assets/css/chat_with_admin.css'
import ServerURL from './ServerURL';

const ChatWindow = () => {
  const token = Cookies.get('token');
  const [newMessage, setNewMessage] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const messagesListRef = useRef(null);
  const { nickname } = useParams();

  useEffect(() => {
    messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
    }, [messagesListRef, isOpen])


  const handleSendMessage = useCallback((msg) => {
    var elem = document.createElement("div");
    var author = document.createElement("span");
    var content = document.createElement("span");
    elem.className="message-from";

    author.className = "message-from";
    if(msg.senderName === nickname){
        elem.className="message-from";

        author.className = "message-from";
    }
    else{
        elem.className="message-to";

        author.className = "message-to";
    }
    author.textContent = msg.senderName + ":";

    content.className = "message-text";
    content.textContent = msg.content;

    elem.appendChild(author);
    elem.appendChild(content);

    document.getElementById("messagesList").appendChild(elem);
  },[nickname])
  const togglePopup = () => {
    setIsOpen(!isOpen);
  }
    const callbackSignalR = useCallback((roomData) => {
        let connection = new signalR.HubConnectionBuilder().withUrl(`${ServerURL}/supportChatHub`).build();

        connection.on("Receive", function (user, message){
            console.log("received back");
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

        connection.start().then(res => {connection.invoke("ConnectToRoom", `${roomData.roomName}`)
            .catch(function (err) {
                return console.error(err.toString());
            })});
    
        // document.addEventListener('DOMContentLoaded', function(){
        //     var a = document.getElementById("sendButton");
        //     console.log(a);
        //   });
        document.getElementById('sendButton').addEventListener("click", function (event) {
            console.log("Sended");
            var message = document.getElementById("messageInput").value;
            document.getElementById("messageInput").value = "";
            messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
            connection.invoke("SendPrivateMessage", `${roomData.senderName}`, message, `${roomData.receiverName}`, `${roomData.roomName}`).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }, [nickname])

    useEffect(() => {
        let room;
        axiosInstance.get(`/im/supportChat?username=Admin`,
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
        axiosInstance.get(`/history?username=Admin`,
            {
                headers:{
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(response => {
                messages = response.data; // выводим данные, полученные из сервера
                if(messages){
                    messages.forEach(msg => handleSendMessage(msg))
                }
            })
            .catch();
    }, [callbackSignalR, handleSendMessage, nickname, token])

  return (
    <div>
      <button className='open-chat-button' onClick={togglePopup}>Связаться с администратором</button>
      
        <div className={
            isOpen? "popup": "displayChat"
        }>
          <div className="popup-content">
            <div className='header-window'>
                <h5>Connect with administration </h5>
                <button className='close-button' onClick={togglePopup}>&times;</button>
            </div>
            <div className='chat-messages'>

                <div 
                    ref={messagesListRef} 
                    id="messagesList" 
                    className='chat-messages__content main-content log-reg-inner'>
                    
                </div>
            
                <input
                type="text"
                id="messageInput"
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                />
                <input type='submit' id="sendButton" className='chat-form__submit' value='Send' />
            </div>
          </div>
        </div>
      
    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
    </div>
  );
};

export default ChatWindow;