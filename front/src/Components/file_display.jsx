import { useEffect, useState } from 'react';

const FileDisplay = ({fileBytes}) => {
    const [fileBA, setFileBA] = useState(fileBytes);

    useEffect(() => {
      setFileBA(fileBytes); // обновляем состояние при изменении пропсов
    }, [fileBytes]);

    const isImage = () => {
      const mimeType = 'image/jpeg'; // Replace with actual detection based on the file
      return mimeType.startsWith('image/');
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
            <img src={`data:image/jpeg;base64,${fileBA}`} alt="Изображение" />
          ) : (
            <img src="icon-file.png" alt="Файл" onClick={downloadFile} style={{cursor: "pointer"}} />
          )}
        </div>
      );
}

export default FileDisplay;