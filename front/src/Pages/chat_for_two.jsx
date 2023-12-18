import { axiosInstance } from "../Components/axios_server";
import { useNavigate, useParams } from "react-router-dom";
import { useCallback, useEffect, useState, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import FileDisplay from "../Components/file_display";
import ServerURL from "../Components/server_url";
import './../assets/css/chat_for_two.css';
import "../assets/css/file_uploader.css";
import FileMetadataForm from "../Components/metadata-files";

const ChatForTwoPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');
    const uid = jwtDecode(token).Id;
    const [files, setFiles] = useState([]);
    const [roomData, setRoomData] = useState([]);
    const [connection, setConnection] = useState(null);
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);
    const [filenames, setFileNames] = useState([]);
    const { nickname } = useParams();
    
    // константы для метаданных о файле
    const [openForm, setOpenForm] = useState(false);
    const [fileType, setFileType] = useState('audio');
    const [duration, setDuration] = useState('');
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [studio, setStudio] = useState('');
    const [fileSize, setFileSize] = useState('');
    const [charCount, setCharCount] = useState('');
    const [creationDate, setCreationDate] = useState('');
    const [description, setDescription] = useState('');

    const messageEl = useRef(null);
 
    useEffect(() => {
      if (messageEl) {
        messageEl.current.addEventListener('DOMNodeInserted', event => {
          const { currentTarget: target } = event;
          target.scroll({ top: target.scrollHeight, behavior: 'smooth' });
        });
      }
    }, [])
    
    // отправка неавторизованного пользователя на страницу авторизации
    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    const handleSend = (event) => {
        
        if (message === "")
                return;

        callSendMessageSignalR();
        setMessage("");

        event.preventDefault();
    }
    
    // отправка сообщения
    const callSendMessageSignalR = () =>{
        console.log(`${roomData.senderName}`);
        console.log(message);
        console.log(filenames);
        console.log(`${roomData.receiverName}`);
        console.log(`${roomData.roomName}`);
        connection.invoke("SendPrivateMessage",
            `${roomData.senderName}`,
            message,
            filenames,
            `${roomData.receiverName}`,
            `${roomData.roomName}`)
            .catch(function (err) {
                console.log("error sending message:");
                console.log(`${roomData.senderName}`);
                console.log(message);
                console.log(filenames);
                console.log(`${roomData.receiverName}`);
                console.log(`${roomData.roomName}`);
                return console.error(err.toString());
            });
    }

    // инициализация соединения с Chat Hub-ом
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
        
        connection.on("ReceivePrivateMessage", function (user, message, listFiles){
            let newMessage = 
            {
                belongsToSender : user === nickname,
                message : message,
                senderName : user,
                files: listFiles
            };
            setMessages(prev => [...prev, newMessage])
        });
        setConnection(connection);
            
    }, [nickname])

    // получаем данные о комнате signalaR для чата
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
      
    // обработка прикрепления файла(ов)
    const handleFileChange = async (e) => { 
        if (e.target.files)
        {
            let file = e.target.files[0];
            setFiles((prev) => [...prev, file]);
            setFileType(file.type);
            setFileSize(file.size)
            setOpenForm(true);
        }
            
    };
    
    // удаление файла
    const handleRemoveFile = (id) => { 
        const updatedFiles = files.filter((file, index) => index !== id);
        setFiles(updatedFiles);
        setOpenForm(false);
      };

    const metadata = {
        fileType: fileType,
        duration: duration,
        title: title,
        author: author,
        studio: studio,
        fileSize: fileSize,
        charCount: charCount,
        creationDate: creationDate,
        description: description
    };

    const onSubmitFileMetadata = () =>{
        setOpenForm(false);
        SendFiles();
        setFiles([]);
    }
    
    // отправка файлов
    const SendFiles = async () => {  
        const formData = new FormData();
        Object.keys(metadata).forEach(key => {
            if (metadata[key] != '')
                formData.append(`Metadata[${key}]`, metadata[key]);
        });
        setFileType('');
        setDuration('');
        setTitle('');
        setAuthor('');
        setStudio('');
        setFileSize('');
        setCharCount('');
        setCreationDate('');
        setDescription('');
        for (let i = 0; i < files.length; i++) {
            formData.append(`Files`, files[i]);
          }
        try{
            axiosInstance.post("/uploadFile", formData, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        'Content-Type': 'multipart/form-data'
                    }
                })
                .then(res => {
                    console.log('файл отправлен успешно')
                    console.log(res.data);
                    setFileNames(prev => [...prev, res.data]);
              })
              .catch(err => {
                console.log("ошибка в отправлении")
                console.log(err)});
            console.log('АЛО НАХУЙ ТЫ РАБОТАЕШЬ ИЛИ НЕТ?!!!!!')
        } catch (e){
            console.log(e);
        }
    } 

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>
                <div id="messagesList" className='chat-messages__content' ref={messageEl}>
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
                                    Array.from(mes.files).length !== 0 
                                    ? 
                                        mes.files.map((fileN, index) => (
                                            <FileDisplay fileName={fileN} belongsToSender={mes.belongsToSender}/>
                                        ))
                                    :
                                    <></>
                                }
                            </>
                        ))
                    }
                </div>
            </div>
            <FileMetadataForm 
                fileType={fileType}
                duration={duration}
                setDuration={setDuration}
                title={title}
                setTitle={setTitle}
                author={author}
                setAuthor={setAuthor}
                studio={studio}
                setStudio={setStudio}
                fileSize={fileSize}
                charCount={charCount}
                setCharCount={setCharCount}
                creationDate={creationDate}
                setCreationDate={setCreationDate}
                description={description}
                setDescription={setDescription}
                handleSubmit={onSubmitFileMetadata}
                isOpen={openForm}/>
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
                            onChange={handleFileChange}
                            disabled={files.length >= 1}/>
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