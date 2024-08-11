const FileMetadataForm = (
    {fileType,
    duration,
    setDuration,
    title,
    setTitle,
    author,
    setAuthor,
    studio,
    setStudio,
    fileSize,
    charCount,
    setCharCount,
    creationDate,
    setCreationDate,
    description,
    setDescription,
    handleSubmit,
    isOpen}) => {

  return (
    <div className={isOpen ? "popup" : "displayChat"}>
        <div className="popup-content">
        {fileType === 'audio/mpeg' && (
            <div>
            <label>Duration:</label>
            <input type="text" value={duration} onChange={(e) => setDuration(e.target.value)} />
            <label>Title:</label>
            <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
            <label>Author:</label>
            <input type="text" value={author} onChange={(e) => setAuthor(e.target.value)} />
            </div>
        )}
        {fileType === 'video' && (
            <div>
            <label>Duration:</label>
            <input type="text" value={duration} onChange={(e) => setDuration(e.target.value)} />
            <label>Title:</label>
            <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
            <label>Studio:</label>
            <input type="text" value={studio} onChange={(e) => setStudio(e.target.value)} />
            </div>
        )}
        {fileType === 'document' && (
            <div>
            <label>File Size:</label>
            <input type="text" value={fileSize} onChange={(e) => setFileSize(e.target.value)} />
            <label>Character Count:</label>
            <input type="text" value={charCount} onChange={(e) => setCharCount(e.target.value)} />
            {/* Add inputs for other document metadata */}
            </div>
        )}
        {fileType === 'archive' && (
            <div>
            <label>Creation Date:</label>
            <input type="text" value={creationDate} onChange={(e) => setCreationDate(e.target.value)} />
            {/* Add inputs for archive metadata */}
            </div>
        )}
        <div>
            <label>Description:</label>
            <input type="text" value={description} onChange={(e) => setDescription(e.target.value)} />
            {/* Add inputs for archive metadata */}
            </div>
        <button onClick={handleSubmit}>Submit</button>
        </div>
    </div>
  );
};

export default FileMetadataForm;
