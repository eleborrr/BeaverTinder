import { axiosInstance } from "../Components/axios_server";
import { useNavigate, useParams } from "react-router-dom";
import { useCallback, useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import FileDisplay from "../Components/file_display";
import ServerURL from "../Components/server_url";
import './../assets/css/chat_for_two.css';
import "../assets/css/file_uploader.css";

const ChatForTwoPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const [files, setFiles] = useState([]);
    const [roomData, setRoomData] = useState([]);
    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);
    const [filesToLoad, setFilesToLoad] = useState([]);
    const { nickname } = useParams();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    const handleSend = (event) => {
        
        if (message === "" && filesToLoad.length === 0)
                return;
                console.log("files to load");
                console.log(filesToLoad);
                connection.invoke("SendPrivateMessage", 
                                `${roomData.senderName}`,
                                message, 
                                filesToLoad,   
                                `${roomData.receiverName}`,
                                `${roomData.roomName}`)
                    .catch(function (err) { 
                console.log("error sending message");
                console.log("form data:");
                console.log(files); 
                return console.error(err.toString());
                });
        
        setMessage("");
        setFiles([]);
        setFilesToLoad([]);
        event.preventDefault();
    }

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

        connection.on("ReceivePrivateMessage", function (user, message, file){
            console.log("normal chat recieved");
            let newMessage = 
            {
                belongsToSender : user === nickname,
                message : message,
                senderName : user,
                file: file
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
        if (e.target.files)
        {
            Array.from(e.target.files).forEach(inputFile => {
                setFiles((prev) => [...prev, inputFile]);

                let reader = new FileReader();
                reader.onload = () => {
                    let newFile = 
                        {
                            //name: file.name,
                            BytesArray: Array.from(new Uint8Array(reader.result))
                        }
                    setFilesToLoad(prev => [...prev, newFile]);
                }
                reader.readAsArrayBuffer(inputFile);
            })
            
        }
        
        
    };
    
    const handleRemoveFile = (id) => {
        const updatedFiles = files.filter((file, index) => index !== id);
        const updatedFilesToLoad = filesToLoad.filter((file, index) => index !== id);
        setFiles(updatedFiles);
        setFilesToLoad(updatedFilesToLoad);
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
                            <>
                                <Message 
                                    key={index}
                                    senderName={mes.senderName}
                                    message={mes.message}
                                    belongsToSender={mes.belongsToSender}
                                />
                                {
                                    mes.file 
                                    ? 
                                    <FileDisplay fileBytes={mes.file}/>
                                    :
                                    <></>
                                }
                            </>
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
                        <input type="file"
                         id="fileInput"
                         style={{display: "none"}} 
                         multiple onChange={handleFileChange}/>
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