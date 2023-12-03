const FileDisplay = ({fileBytes}) => {
    const imageType = require('image-type');

    const isImage = (fileBytes) => {
        const type = imageType(fileBytes);
        if (type && (type.mime === 'image/jpeg' || type.mime === 'image/png' || type.mime === 'image/gif')) {
            return true;
        }
        return false;
      };

    const downloadFile = () => {
        // Симулируем скачивание файла при нажатии
        const blob = new Blob([fileBytes]);
        const link = document.createElement("a");
        link.href = URL.createObjectURL(blob);
        link.download = "file";
        link.click();
      };

    return (
        <div>
          {isImage(fileBytes) ? (
            <img src={`data:image/jpeg;base64,${fileBytes}`} alt="Изображение" />
          ) : (
            <img src="icon-file.png" alt="Файл" onClick={downloadFile} style={{cursor: "pointer"}} />
          )}
        </div>
      );
}

export default FileDisplay;