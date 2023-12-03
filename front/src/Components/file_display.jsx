import { useEffect, useState } from 'react';
import FilesServerURL from './files_server_url';

const FileDisplay = ({fileName}) => {
    const [fileN, setFileN] = useState(fileName);

    useEffect(() => {
      setFileN(fileName); // обновляем состояние при изменении пропсов
    }, [fileBytes]);

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

    return (
        <div>
          {isImage() ? (
            <img src={`${FilesServerURL}/api/files/my-bucket/${fileN}`} alt="Изображение" />
          ) : (
            <img src="icon-file.png" alt="Файл" onClick={downloadFile} style={{cursor: "pointer"}} />
          )}
        </div>
      );
}

export default FileDisplay;