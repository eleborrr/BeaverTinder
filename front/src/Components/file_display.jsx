import { useEffect, useState } from 'react';
import FilesServerURL from './files_server_url';
import { axiosInstance } from "../Components/axios_server";
import defaultImage from './../assets/images/allmedia/default_file_image.png'
import './../assets/css/file_display.css';

const FileDisplay = ({fileName, belongsToSender}) => {
    const [imgBytes, setImgBytes] = useState();
    const [metadata, setMetadata] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect( () => {
        const fetchData = async () => {
            let trying = 0; 
            let success = false; 
            while (!success && trying < 5) { 
                try {
                    // Отправляем запрос и ждем ответ
                    const response = await axiosInstance.get(`${FilesServerURL}/api/files/main-bucket?filename=${fileName}`);
                    setImgBytes(response.data.bytesArray); 
                    setMetadata(response.data.metadata); 
                    console.log(response.data); 
                    success = true; 
                    setLoading(false); 
                } catch (error) {
                    console.log("Не удалось получить файл"); 
                    trying++; 
                }
                if (!success)
                    await new Promise(resolve => setTimeout(resolve, 500)); 
            }
        };
    
        fetchData();
    }, []);

    const isImage = () => {
        const imageTypeRegex = /^image\/*/;

        return imageTypeRegex.test(metadata.data.fileType);
    };


    const downloadFile = () => {
        const binaryString = atob(imgBytes);

        if (binaryString.length === 0) {
            console.error('Массив байтов PDF пуст');
        } else {
            const byteArray = new Uint8Array(binaryString.length);
            for (let i = 0; i < binaryString.length; i++) {
                byteArray[i] = binaryString.charCodeAt(i);
            }

            // Создание Blob из Uint8Array
            const blob = new Blob([byteArray], { type: metadata.data.fileType });

            // Использование FileReader для чтения blob и создания ссылки для скачивания
            const reader = new FileReader();
            reader.onload = () => {
                const url = reader.result;
                if (!url) {
                    console.error('Не удалось создать URL-объект');
                } else {
                    const link = document.createElement('a');
                    link.href = url;
                    link.download = metadata.data.fileName || 'документ.pdf';
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                }
            };
            reader.onerror = (error) => {
                console.error('Ошибка при чтении blob:', error);
            };
            reader.readAsDataURL(blob);
        }
    };

    if (loading) {
        return <div>Loading...</div>;
    } else {
        return (
            <div className={belongsToSender ? "message-from" : "message-to"}>
                {isImage() ? (
                    <img className={belongsToSender ? "message-from send_image" : "message-to send_image"} src={`data:image/jpg;base64,${imgBytes}`}  alt="File" />
                ) : (
                    <img className='default_file_image' src={defaultImage} alt="Image" onClick={downloadFile} style={{cursor: "pointer"}} />
                )}
                <div className="file-metadata">
                    <ul>
                        {Object.keys(metadata.data).map(key => (
                        <li key={key}>
                            <strong>{key}:</strong> {metadata.data[key]}
                        </li>
                        ))}
                    </ul>
                </div>  
            </div>
        );
    }
    
}

export default FileDisplay;