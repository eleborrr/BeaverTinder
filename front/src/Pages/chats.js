import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import './../assets/css/chats.css';

const ChatsPage = () => {

    const navigate = useNavigate();
    const token = Cookies.get('token');
    
    const [searchInput, setSearchInput] = useState('');
    const [searchNone, setSearchNone] = useState(true);

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    const onPressEnter = (e) => {
        if (e.key === 'Enter'){
            console.log('activate search');
            setSearchInput('');
        }
    }

    return(
        <>
            <div className='chat'>
                <header>
                    <h2 className='title'>
                        <span className="text">Your Chats:</span>
                    </h2>
                    <ul className='tools'>
                        <li>
                            <a className={searchNone? 'fa fa-search' : 'fa fa-search fa-close'} onClick={() => setSearchNone(!searchNone)}></a>
                        </li>
                    </ul>
                </header>
                <div className='body'>
                    <div className="search" style={{display: searchNone? "none" : "block"}}>
                        <input placeholder='Search...' type='text' value={searchInput} onChange={(e) => setSearchInput(e.target.value)} onKeyPress={(e) => onPressEnter(e)}/>
                    </div>
                    <ul>
                    <li onClick={() => navigate('nickname')}>
                        <a className='thumbnail'>
                        NR
                        </a>
                        <div className='content'>
                        <h3>Nick Roach</h3>
                        <span className='preview'>hey how are things going on the...</span>
                        <span className='meta'>
                            2h ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    <li>
                        <a className='thumbnail' href='#'>
                        KS
                        </a>
                        <div className='content'>
                        <h3>Kenny Sing</h3>
                        <span className='preview'>make sure you take a look at the...</span>
                        <span className='meta'>
                            3h ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    <li>
                        <a className='thumbnail' href='#'>
                        MS
                        </a>
                        <div className='content'>
                        <h3>Mitch Skolnik</h3>
                        <span className='preview'>i love wood grain things!</span>
                        <span className='meta'>
                            6h ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    <li>
                        <a className='thumbnail' href='#'>
                        YP
                        </a>
                        <div className='content'>
                        <h3>Yuriy Portnykh</h3>
                        <span className='preview'>check issues for the latest version...</span>
                        <span className='meta'>
                            10h ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    <li>
                        <a className='thumbnail' href='#'>
                        JR
                        </a>
                        <div className='content'>
                        <h3>Josh Ronk</h3>
                        <span className='preview'>make sure to do the following by...</span>
                        <span className='meta'>
                            2d ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    <li>
                        <a className='thumbnail' href='#'>
                        BM
                        </a>
                        <div className='content'>
                        <h3>Benjamin Mueller</h3>
                        <span className='preview'>Hi nice to meet you!</span>
                        <span className='meta'>
                            1w ago &middot;
                            <a href='#'>Category</a>
                            &middot;
                            <a href='#'>Reply</a>
                        </span>
                        </div>
                    </li>
                    </ul>
                </div>
            </div>
        </>

    )
}

export default ChatsPage;