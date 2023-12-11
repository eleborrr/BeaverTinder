import { useEffect, useState } from 'react';
import FilesServerURL from './files_server_url';
import { axiosInstance } from "../Components/axios_server";

const FileDisplay = ({fileName, belongsToSender}) => {
    const [imgBytes, setImgBytes] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        axiosInstance.get(`${FilesServerURL}/api/files/my-bucket?filename=${fileName}`)
            .then(response => {
                setImgBytes(response.data);
                setLoading(false);
            })
            console.log(!belongsToSender);
    }, [imgBytes]);

    const isImage = () => {
        return true;
    };


    const downloadFile = () => {
        // Симулируем скачивание файла при нажатии
        const blob = new Blob([fileBA]);
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'file';
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
                    <img className={belongsToSender ? "message-from" : "message-to"} src={`data:image/jpg;base64,${imgBytes}`}  alt="Изображение" />
                ) : (
                    <img src="icon-file.png" alt="Файл" onClick={downloadFile} style={{cursor: "pointer"}} />
                )}
            </div>
        );
    }
    
}

export default FileDisplay;