import { axiosInstance } from "../Components/axios_server";
import Cookies from "js-cookie";
import React, { useCallback, useEffect, useState, useRef} from 'react';
import { useParams } from "react-router-dom";
import './../assets/css/chat_with_admin.css'
import { ChatClient } from "../generated/chat_grpc_web_pb";
import { MessageGrpc, JoinRequest } from "../generated/chat_pb"
import { FileUpload } from "./file_uploader";

const ChatWindow = () => {
  const token = Cookies.get('token');
  const [newMessage, setNewMessage] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const messagesListRef = useRef(null);
  const { nickname } = useParams();
  const client = new ChatClient('http://localhost:8080', null, null);

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

    document.getElementById("messagesList-admin").appendChild(elem);
  },[nickname])

  const togglePopup = () => {
    setIsOpen(!isOpen);
  }
    const callbackGrpc = useCallback((roomData) => {

        console.log(roomData);
        const request = new JoinRequest();
        request.setRoomName(roomData.roomName);
        request.setUserName(roomData.senderName);

        const stream = client.connectToRoom(request);


        stream.on('data', (message) => {
            console.log(message.getMessage());
            console.log("normal chat recieved");
            var elem = document.createElement("div");
            var author = document.createElement("span");
            var content = document.createElement("span");
            if(message.from === nickname){
                elem.className="message-from";

                author.className = "message-from";
            }
            else{
                elem.className="message-to";

                author.className = "message-to";
            }
            author.textContent = user + ":";

            content.className = "message-text";
            content.textContent = message.content;

            elem.appendChild(author);
            elem.appendChild(content);
            elem.setAttribute("key", `${counterMessagesKey++}`)

            document.getElementById("messagesList").appendChild(elem);

            console.log(message);
            setMessages(prev => [...prev, message])
        });

        
        document.getElementById('sendButton-admin').addEventListener("click", function (event) {
            console.log("Sended");
            var message = document.getElementById("messageInput-admin").value;
            document.getElementById("messageInput-admin").value = "";
            messagesListRef.current.scrollTop = messagesListRef.current.scrollHeight;
            const request = new MessageGrpc();
            request.setMessage(message)
            request.setUserName(roomData.senderName)
            request.setReceiverUserName("Admin")
            request.setGroupName(roomData.roomName)
            // request.setFiles([])
            console.log(request)
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
        axiosInstance.get(`/im/supportChat?username=Admin`,
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
    }, [callbackGrpc, handleSendMessage, nickname, token])


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
            <div className='chat-messages-admin'>

                <div 
                    ref={messagesListRef} 
                    id="messagesList-admin" 
                    className='chat-messages__content main-content log-reg-inner'>
                    
                </div>
                <div id="files-admin" className="files-admin"></div>
            
                <div  className='chat-admin-input'>
                    <textarea
                        className="input-admin"
                        type="text"
                        id="messageInput-admin"
                        value={newMessage}
                        onChange={(e) => setNewMessage(e.target.value)}
                    />
                    <FileUpload 
                        sendButtonId="sendButton-admin"
                        addFilesArea="files-admin"
                        idForDiv="admin"
                    />
                    <input type='submit' id="sendButton-admin" className='chat-form__submit-admin' value='Send' />
                </div>
            </div>
          </div>
        </div>
      
    <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
    </div>
  );
};

export default ChatWindow;