import './../assets/css/chat_with_admin.css'
import jwtDecode from "jwt-decode";
import { axiosInstance } from "../Components/axios_server";
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

//   useEffect(() => {
//     if (!token){
//         navigate("/login");
//     }
//     }, [navigate, token])


  const handleSendMessage = (msg) => {
    var elem = document.createElement("div");
    var author = document.createElement("span");
    var content = document.createElement("span");
    elem.className="message-from";

    author.className = "message-from";
    // if(user === nickname){
        
    // }
    // else{
    //     elem.className="message-to";

    //     author.className = "message-to";
    // }
    author.textContent = "INSERT USER NAME" + ":";

    content.className = "message-text";
    content.textContent = message;

    elem.appendChild(author);
    elem.appendChild(content);

    document.getElementById("messagesList").appendChild(elem);
  };
  const togglePopup = () => {
    setIsOpen(!isOpen);
  }

    const [message, setMessage] = useState('');
    const callbackSignalR = useCallback((roomData) => {
    
        let connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5276/supportChatHub").build();

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
            connection.invoke("SendPrivateMessage", `${roomData.senderName}`, message, `${roomData.recieverName}`, `${roomData.roomName}`).catch(function (err) {
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
    }, [callbackSignalR, nickname, token])

  return (
    <div>
      <button className='open-chat-button' onClick={togglePopup}>Связаться с администратором</button>
      
        <div className={
            isOpen? "popup": "displayChat"
        }>
          <div className="popup-content">
            <div className='header-window'>
                <h5>Свяжитесь с администратором </h5>
                <button className='close-button' onClick={togglePopup}>&times;</button>
            </div>
            <div className='chat-messages'>

                <div id="messagesList" className='chat-messages__content'>
                    
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