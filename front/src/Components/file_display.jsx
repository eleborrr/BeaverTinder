import { useEffect, useState } from 'react';
import FilesServerURL from './files_server_url';
import { axiosInstance } from "../Components/axios_server";
import defaultImage from './../assets/images/allmedia/default_file_image.png'
import './../assets/css/file_display.css';

const FileDisplay = ({fileName, belongsToSender}) => {
    const [imgBytes, setImgBytes] = useState();
    const [metadata, setMetadata] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        axiosInstance.get(`${FilesServerURL}/api/files/main-bucket?filename=${fileName}`)
            .then(response => {
                setImgBytes(response.data.bytesArray);
                setMetadata(response.data.metadata);
                console.log(response.data)
                setLoading(false);
            })
    }, []);

    const isImage = () => {
        return metadata.data.fileType == 'image/jpeg';
    };


    const downloadFile = () => {
        // Симулируем скачивание файла при нажатии
        const blob = new Blob([imgBytes], { type: metadata.data.fileType });
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        // задаём имя файла
        link.download = metadata.data.fileName;
        document.body.appendChild(link);
        link.click();
        // Чистим ссылку после скачивания
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
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