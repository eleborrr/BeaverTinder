import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { axiosInstance } from "../Components/axios_server";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import './../assets/css/chat_for_two.css';
import * as signalR from "@microsoft/signalr";
import ServerURL from "../Components/server_url";
import { FileUpload } from "../Components/file_uploader";
import "../assets/css/file_uploader.css"

const ChatForTwoPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const [files, setFiles] = useState([]);
    const [roomData, setRoomData] = useState([]);
    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);
    const { nickname } = useParams();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    const handleSend = useCallback((event) => {
        const reader = new FileReader();
        let totalArrayBuffer = new ArrayBuffer();
        const arrayFiles= Array.from(files);
        if (message === "" && files.length === 0)
                return;
        function readFilesSequentially(index) {
            console.log("start");
        if (index < arrayFiles.length) {
            const file = arrayFiles[index];
            console.log(file);
            reader.onloadend = function(event) {
            console.log(event.target);
            console.log(event.target.result);
            const arrayBuffer = event.target.result;
            const combinedArrayBuffer = new Uint8Array(totalArrayBuffer.byteLength + arrayBuffer.byteLength);
            combinedArrayBuffer.set(new Uint8Array(totalArrayBuffer), 0);
            combinedArrayBuffer.set(new Uint8Array(arrayBuffer), totalArrayBuffer.byteLength);
            totalArrayBuffer = combinedArrayBuffer.buffer;
            readFilesSequentially(index + 1); // Read the next file recursively
            };
        reader.readAsArrayBuffer(file);
        } else {
            console.log("start to back");
            console.log(Array.from(totalArrayBuffer));
            console.log(totalArrayBuffer);
            connection.invoke("SendPrivateMessage", 
                            `${roomData.senderName}`,
                            message, 
                            Array.from(new Uint8Array(totalArrayBuffer)),   
                            `${roomData.receiverName}`,
                            `${roomData.roomName}`)
                .catch(function (err) { 
            console.log("error sending message");
            console.log("form data:");
            console.log(files);
            return console.error(err.toString());
        });
        }
        }

        readFilesSequentially(0);
        
        setMessage("");
        event.preventDefault();
    },[connection, message, files])

    const callbackSignalR = useCallback((roomData) => {
        setRoomData(roomData);
        let connection = new signalR.HubConnectionBuilder()
                .withUrl(`${ServerURL}/chatHub`)
                .build();

        connection.start().then(res => {
            connection.invoke("GetGroupMessages", `${roomData.roomName}`)
                    .catch(function (err) {
                return console.error(err.toString());
            });
        });

        connection.on("ReceivePrivateMessage", function (user, message){
            console.log("normal chat recieved");
            let newMessage = 
            {
                belongsToSender : user === nickname,
                message : message,
                senderName : user
            };
            setMessages(prev => [...prev, newMessage])
        });
        setConnection(connection);
            
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
      
    const  handleFileChange = (e) => {
        let newArray = [ ...files, ...Array.from(e.target.files) ];
        setFiles(newArray);
        console.log(newArray);
        console.log(files);
    };
    
    const handleRemoveFile = (id) => {
        const updatedFiles = files.filter((file, index) => index !== id);
        setFiles(updatedFiles);
      };

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>
                <div id="messagesList" className='chat-messages__content'>
                    {
                        messages.map((mes, index) => (
                            <Message 
                                key={index}
                                senderName={mes.senderName}
                                message={mes.message}
                                belongsToSender={mes.belongsToSender}
                            />
                        ))
                    }
                </div>
            </div>
            <div className="files">
                <div className="files-list" id="files-list">
                    {
                        files.map((fileData, index) => (
                            <File 
                                name={fileData.name}
                                size={fileData.size}
                                fileId={index}
                                dataUrl={fileData.dataUrl}
                                onClick={() => handleRemoveFile(index)}/>
                        ))
                    }
                </div>
            </div>
            <div className='chat-input'>
                    <input 
                        type='text' 
                        hidden={true} 
                        value={uid} 
                        readOnly={true} 
                    />
                    <div className="on-input">
                        <textarea 
                            type='text'
                            autoComplete="off" 
                            id='messageInput' 
                            className='chat-form__input' 
                            placeholder='Введите сообщение' 
                            value={message} 
                            onChange={(e) => setMessage(e.target.value)} 
                        />
                    </div>
                    <div className="uploader-container" id="chat">
                        <label htmlFor="fileInput" className="file-inputer">
                            <i className="fas fa-paperclip"></i>
                        </label>
                        <input type="file" multiple id="fileInput" style={{display: "none"}} onChange={handleFileChange}/>
                    </div>
                    <input type='submit' 
                        id="sendButton" 
                        className='chat-form__submit' 
                        value='Send' 
                        onClick={(event) => handleSend(event)}
                    />
                    
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
        </div>
    )
}

const Message = ({ senderName, message, belongsToSender }) => {

    return (
        <div className={belongsToSender ? "message-from" : "message-to"}>
            <span className={belongsToSender ? "message-from" : "message-to"}> {senderName}: </span>
            <span className="message-text"> {message} </span>
        </div>
    )
}

const File = ({ name, size, fileId, type, dataUrl, onClick }) => {

    return (
        <div className="file-item" id={fileId} key={fileId}>
            <img src={dataUrl} key={fileId}/>
            <span className="file-description"> {name} </span>
            <span className="file-description"> {`${(size / 1024).toFixed(2)} KB`} </span>
            <button className="remove-btn" onClick={onClick}>&times;</button>
        </div>
    )
}
export default ChatForTwoPage;