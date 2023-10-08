import './../assets/css/chat_with_admin.css'
import React, { useState } from 'react';

const ChatWindow = () => {
  const [messages, setMessages] = useState([]);
  const [newMessage, setNewMessage] = useState('');
  const [isOpen, setIsOpen] = useState(false);

  const handleSendMessage = () => {
    if (newMessage.trim() !== '') {
        const mess = { 
            message : newMessage,
            author: 'me' 
        }
        const m2 = {
            message : newMessage,
            author: 'to'
        }
        setMessages([...messages, mess, m2]);
        setNewMessage('');                        
    }
  };
  const togglePopup = () => {
    setIsOpen(!isOpen);
  }

  return (
    <div>
      <button className='open-chat-button' onClick={togglePopup}>Связаться с администратором</button>
      {isOpen && (
        <div className="popup">
          <div className="popup-content">
            <div className='header-window'>
                <h5>Свяжитесь с администратором </h5>
                <button className='close-button' onClick={togglePopup}>&times;</button>
            </div>
                {messages.map((message, index) => (
                    <div className = "chatMessage">
                        {message.author === 'me' ?
                        <div key={index} className="chat-message-from">
                            {message.message}
                        </div>                  
                    :
                    <div className='adminBlock'>
                        <img src='https://fikiwiki.com/uploads/posts/2022-02/1644852415_12-fikiwiki-com-p-kartinki-admina-12.png' className='pngAdmin'></img>
                        <div key={index} className="chat-message-to">
                            {message.message}
                        </div>
                    </div>
                                                            
                    }                        
                    </div>
                    ))}
            <div>
                <input
                type="text"
                value={newMessage}
                onChange={(e) => setNewMessage(e.target.value)}
                />
                <button onClick={handleSendMessage}>Send</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ChatWindow;