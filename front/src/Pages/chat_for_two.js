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
    const { nickname } = useParams();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    const [message, setMessage] = useState('');
    const callbackSignalR = useCallback((roomData) => {

        let connection = new signalR.HubConnectionBuilder().withUrl(`${ServerURL}/chatHub`).build();

        connection.on("ReceivePrivateMessage", function (user, message){
            console.log("normal chat recieved");
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
            connection.invoke("SendPrivateMessage", `${roomData.senderName}`, message, files, `${roomData.recieverName}`, `${roomData.roomName}`).catch(function (err) { 
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

    const createFileElement = (fileData, fileId) => {
        // Create a <div> element to hold the file details and controls
        const fileDiv = document.createElement('div');
        fileDiv.classList.add('file-item');
        fileDiv.id=`file-${fileId}`;
        
        // Create an <img> element for file preview (if applicable)
        const fileImage = document.createElement('img');
        fileImage.src = fileData.dataUrl;
        fileDiv.appendChild(fileImage);
        
        // Create a <span> element for file name
        const fileName = document.createElement('span');
        fileName.textContent = fileData.name;
        fileName.classList.add("file-description");
        fileDiv.appendChild(fileName);
        
        // Create a <span> element for file size
        const fileSize = document.createElement('span');
        fileSize.textContent = `${(fileData.size / 1024).toFixed(2)} KB`;
        fileSize.classList.add("file-description");
        fileDiv.appendChild(fileSize);
        
        // Create a <button> element for removing the file
        const removeButton = document.createElement('button');
        removeButton.innerHTML  = '&times;';
        removeButton.classList.add('remove-btn');
        fileDiv.appendChild(removeButton);
        
        return fileDiv;
    };
      
    const handleFileChange = (e) => {

        setFiles([...files, ...Array.from(e.target.files)]);
        let newFiles = Array.from(e.target.files);
        console.log(newFiles);
        newFiles.forEach((file, index) => {

            const fileData = {
                name: file.name,
                size: file.size,
                type: file.type,
                dataUrl: e.target.result,
            };
            const fileElement = createFileElement(fileData, index);
            
            document.getElementById("files-list").appendChild(fileElement);

            const removeButton = fileElement.querySelector('.remove-btn');
            removeButton.addEventListener('click', () => handleRemoveFile(index));
        });
        console.log(files);
    };
    
    const handleRemoveFile = (id) => {
        const updatedFiles = files.filter((file, index) => index !== id);
        setFiles(updatedFiles);
        
        const fileToRemove = document.getElementById(`file-${id}`);
        console.log(id)
        console.log(document.getElementById(`file-${id}`))
        if (fileToRemove) {
          fileToRemove.remove();
        }
      };
    
      const setOnFileLoad =  useCallback(async (e) => {
        let button = document.getElementById("sendButton")
        
        if (!button || button === null)
            return;

        button.addEventListener("click", async function (event) { 
          
            console.log(files);
           if (files.length === 0)
             return;
            const formData = new FormData();
            files.forEach(file => formData.append("files", file))
            try{
                const res = await axiosInstance.post("/test", formData,
                {
                    headers : {
                        'Content-Type': 'multipart/form-data'
                    }
                });
                console.log(res);
            } catch (e){
                console.log(e);
            }

            setFiles([]);
            let addArea = document.getElementById("files-list")
            while (addArea.firstChild) {
                addArea.removeChild(addArea.firstChild);
            }
            event.preventDefault();
        });
    },[]);

    useEffect(() => {
        setOnFileLoad();
    },[]);

    return(
        <div className='chat'>
            <div className="ref-back">
                <a href="/chats" className="backto-home"><i className="fas fa-chevron-left"></i> Back to chats</a>
            </div>
            <div className='chat-messages'>
                <div id="messagesList" className='chat-messages__content'>
                    
                </div>
            </div>
            <div className="files">
                <div className="files-list" id="files-list">

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
                    />
                    
            </div>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js"></script>
        </div>
    )
}

export default ChatForTwoPage;