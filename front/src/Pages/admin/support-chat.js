import { useCallback, useEffect, useState, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { axiosInstance } from "../../Components/axios_server";
import * as signalR from "@microsoft/signalr";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../../assets/css/chat_for_two.css';
import ServerURL from "../../Components/server_url";
import { ChatClient } from "../../generated/chat_grpc_web_pb";
import { MessageGrpc, JoinRequest } from "../../generated/chat_pb"

const SupportChatPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const messagesListRef = useRef(null);
    const { nickname } = useParams();
    let counterMessagesKey = 0;
    const client = new ChatClient('http://localhost:8080', null, null);

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    useEffect(() => {
        messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
        }, [messagesListRef])

    const [message, setMessage] = useState('');

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
      }, [nickname])

      const callbackGrpc = useCallback((roomData) => {

        const request = new JoinRequest();
        request.setRoomName(roomData.roomName);
        request.setUserName(roomData.senderName);

        const stream = client.connectToRoom(request);


        stream.on('data', (message) => {
            var msg = {
                "content": message.getMessage(),
                "senderName" : message.getUserName()
            };
            handleSendMessage(msg);
        });

        
        document.getElementById('sendButton').addEventListener("click", function (event) {
            var message = document.getElementById("messageInput").value;
            document.getElementById("messageInput").value = "";
            messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
            const request = new MessageGrpc();
            request.setMessage(message)
            request.setUserName(roomData.senderName)
            request.setReceiverUserName("Admin")
            request.setGroupName(roomData.roomName)
            // request.setFiles([])
            const headers = {
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            };
            client.sendMessage(request, headers, (err, resp) => {
                if (err) console.log(err);
                console.log(resp);
            })
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
            callbackGrpc(room);
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
                if(messages){
                    messages.forEach(msg => handleSendMessage(msg))
                }
            })
            .catch();
    }, [callbackGrpc, handleSendMessage, nickname, token])
    

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/support_chat" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>
                    
                <div
                ref={messagesListRef} 
                id="messagesList"
                 className='chat-messages__content'>
                    
                </div>
            </div>
            <div className='chat-input'>
                    <input type='text' hidden={true} value={uid} readOnly={true} />
                    <input type='text' autoComplete="off" id='messageInput' className='chat-form__input' placeholder='Введите сообщение' value={message} onChange={(e) => setMessage(e.target.value)} />
                    <input type='submit' id="sendButton" className='chat-form__submit' value='Send' />
            </div>
        </div>
    )
}

export default SupportChatPage;