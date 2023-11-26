import { React, useCallback, useEffect, useState } from "react";
import { axiosInstance } from "./axios_server";
import "../assets/css/file_uploader.css"

export const FileUpload = ({sendButtonId, addFilesArea, idForDiv}) => {
    const [files, setFiles] = useState([]);
    const [addFilesAreaI, setAddFilesAreaI] = useState(addFilesArea);
    const setOnFileLoad =  useCallback(async (e) => {
        let button = document.getElementById(`${sendButtonId}`)
        
        if (!button || button === null)
            return;

        button.addEventListener("click", async function (event) { 
          
            console.log(files);
           if (files.length == 0)
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
            let addArea = document.getElementById(`${addFilesAreaI}`)
            console.log(`delete from ${addFilesAreaI}`);
            while (addArea.firstChild) {
                addArea.removeChild(addArea.firstChild);
            }
            event.preventDefault();
        });
    },[]);

    useEffect(() => {
        console.log(addFilesAreaI)
        console.log(sendButtonId)
        setOnFileLoad()
        const element = document.getElementById(`${idForDiv}`); // Замените 'yourElementId' на фактический ID вашего элемента

        element.addEventListener('mouseover', () => {
        console.log(`${addFilesArea}`);
});

    }
    , [setOnFileLoad]);

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
        console.log(addFilesAreaI);
        console.log(sendButtonId);
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
            
            document.getElementById(`${addFilesAreaI}`).appendChild(fileElement);

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

    

    return (
        <div className="uploader-container" id={`${idForDiv}`}>
            <label htmlFor="fileInput" className="file-inputer">
                <i className="fas fa-paperclip"></i>
            </label>
            <input type="file" multiple id="fileInput" style={{display: "none"}} onChange={handleFileChange}/>
        </div>
    );
}