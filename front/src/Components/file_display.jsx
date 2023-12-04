import { useEffect, useState } from 'react';
import FilesServerURL from './files_server_url';

const FileDisplay = ({fileName}) => {
    const [fileN, setFileN] = useState(fileName);

    useEffect(() => {
        setFileN(fileName); // обновляем состояние при изменении пропсов
    }, []);

    const isImage = () => {
        return true;
    };



    function GetImageJPGFromByteArray(fileN) {
        fetch(`${FilesServerURL}/api/files/my-bucket?filename=${fileN}`, {
        })
    .then(response => {
            const blob = new Blob([new Uint8Array(response.data)], {type: 'image/jpg'});
            const imageUrl = URL.createObjectURL(blob);
            return imageUrl;
        })
    }

    return (
        <div>
            { (
                <img src={GetImageJPGFromByteArray(fileN)} alt="Изображение" />
            )}
        </div>
    );
}

export default FileDisplay;