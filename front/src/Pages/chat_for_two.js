import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { axiosInstance } from "../Components/axios_server";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../assets/css/chat_for_two.css';
import 'https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js';
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
    }, [])

    const [messages, setMessages] = useState(null); 
    const [interval, setInterval] = useState(null); 
    const [message, setMessage] = useState('');


    useEffect(() => {
        let room;
        var response = axiosInstance.get(`/im/chat?id=${nickname}`,
        {
           headers:{
               Authorization: `Bearer ${token}`,
               Accept : "application/json"
           }
        }) 
        .then(response => {
            console.log(response.data);
            room = response.data; // выводим данные, полученные из сервера
            callbackSignalR(room);
        })
        .catch(error => {
            console.log(error); // обработка ошибок
        }); 
    }, [])

    function callbackSignalR(roomData){

        let connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7015/chatHub").build();

        connection.on("ReceivePrivateMessage", function (user, message){
            console.log(user);
            var elem = document.createElement("div");
            elem.className="message-from";

            var content = document.createElement("span");
            content.className = "message-text";
            content.textContent = user + " (private): " + message;

            elem.appendChild(content);

            document.getElementById("messagesList").appendChild(elem);
        });


        connection.start().then(res => {connection.invoke("GetGroupMessages", `${roomData.name}`, `${roomData.secondUserId}`)
            .catch(function (err) {
                return console.error(err.toString());
            })});

        document.getElementById("sendButton").addEventListener("click", function (event) { 
            var message = document.getElementById("messageInput").value;
            connection.invoke("SendPrivateMessage", `${roomData.secondUserId}`, message, `${roomData.firstUserId}`, `${roomData.name}`).catch(function (err) { 
                return console.error(err.toString());
            });
            event.preventDefault();
        });
    }
    

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>

                <div id="messagesList" className='chat-messages__content'>
                    {/* <ul id="messagesList"> */}
                    {/* <div className="message-from">
                        <span className="message-from">Глеб: </span>
                        <span className="message-text">Первое сообщение</span>
                    </div> */}
                    {/* <div className="message-to">
                        <span className="message-from">Глеб: </span>
                        <span className="message-text">Первое сообщение</span>
                    </div> */}
                   {/*  </ul> */}
                </div>
            </div>
            <div className='chat-input'>
{/*                 <form method='post' id='chat-form' className="form-input">
 */}                    <input type='text' hidden={true} value={uid} readOnly={true} />
                    <input type='text' autoComplete="off" id='messageInput' className='chat-form__input' placeholder='Введите сообщение' value={message} onChange={(e) => setMessage(e.target.value)} />
                    <input type='submit' id="sendButton" className='chat-form__submit' value='Send' />
{/*                 </form>
 */}            </div>

        </div>
    )
}

export default ChatForTwoPage;