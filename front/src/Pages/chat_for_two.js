import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { axiosInstance } from "../Components/axios_server";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../assets/css/chat_for_two.css';
import * as signalR from "@microsoft/signalr";

const ChatForTwoPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const { nickname } = useParams();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

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


        connection.start().then(res => {connection.invoke("GetGroupMessages", `${roomData.roomName}`)
            .catch(function (err) {
                return console.error(err.toString());
            })});

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
        axiosInstance.get(`/im/chat?username=${nickname}`,
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
    }, [callbackSignalR, nickname, token])

    
    

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>

                <div id="messagesList" className='chat-messages__content'>
                    
                </div>
            </div>
            <div className='chat-input'>
                    <input type='text' hidden={true} value={uid} readOnly={true} />
                    <input type='text' autoComplete="off" id='messageInput' className='chat-form__input' placeholder='Введите сообщение' value={message} onChange={(e) => setMessage(e.target.value)} />
                    <input type='submit' id="sendButton" className='chat-form__submit' value='Send' />
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
        </div>
    )
}

export default ChatForTwoPage;