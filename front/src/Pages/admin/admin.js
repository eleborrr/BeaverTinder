import React, { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import { Input, Button } from "@mui/material";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import { axiosInstance } from "../../Components/axios_server";
import PageNotFound from './../404';
import "../../assets/css/admin.css"

const AdminPage = () => {
    const [searchTerm, setSearchTerm] = useState('');
    const [isAvailable, setIsAvailable] = useState(false);
    const [userId, setUserId] = useState('');
    const [func, setFunc] = useState('');
    const [isReady,setReady] = useState(false);
    const [users, setUsers] = useState([]);
    const [viewUsers, setViewUsers] = useState([]);
    const token = Cookies.get('token');

    const onSubmit = (e, func) => {
        setFunc(func);
        e.preventDefault();
        setReady(true);
    };

    useEffect( () => {
    //    jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].some(element => {
    //         if (element === "Admin")
    //         {
    //             setIsAvailable(true);
    //         }
    //     })
    setIsAvailable(true);
    }, [])

    const handleGiveSubscription = (userId, subscriptionType) => {
        // обработка выбора типа подписки
      };

    const handleDeleteSubscription = (userId) => {
        // обработка нажатия кнопки delete subscription
      };

    const handleSearch = (event) => {
        setSearchTerm(event.target.value);
        setViewUsers(users.filter(u => u.nickname.toLowerCase().includes(searchTerm.trim().toLowerCase())))
      };

    useEffect(() => {
        if(!isReady) {
            return;
        }
        functionOnUser(func);
        setReady(false);
    })

    function functionOnUser(func) {
        axiosInstance
        .post('/' + func, {UserId : userId})
        .then(function (response) {
            console.log(response);
        })
        .catch(function(error) {
            console.log(error);
        })

        
    }

    function getAllUsersAxios(){

    }


    return (
        <div>
        {isAvailable? 
        // <div className="adminInput">
        //     <Input placeholder="User Name" onChange={(e) => setUserId(e.target.value)}/> 
        //     <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e, 'ban')}>Ban</Button>
        //     <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e, 'unban')}>Unban</Button>
        //     <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e,'deactivate')}>Deactivate  Search</Button>
        //     <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e,'activate')}>Activate  Search</Button>
        //     <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e,'add_moderator')}>Add Moderator</Button>
        //     {/* <p/>
        //     <Button color="error" onClick={(e) => onSubmit(e,'add_admin')}>Add Admin</Button> */}

        // </div> 
        <>
            <div className="refr-button-div">
                <button type="submit" className="refr-button" onClick={getAllUsersAxios}>Refresh User List</button>
            </div>
            <div className="form-group">
                <label className="search-label">Search</label>
                <input type="text" className="search-input" value={searchTerm} onChange={handleSearch} />
            </div>
                <table>
                    <thead>
                        <tr>
                        <th>Имя</th>
                        <th>Подписка</th>
                        <th>Роль</th>
                        <th>Бан</th>
                        <th>Поиск</th>
                        <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* {viewUsers.map(user => (
                        <tr key={user.id}>
                            <td>{user.name}</td>
                            <td>{user.subInfo}</td>
                            <td>
                            <Button variant="danger" onClick={(e) => onSubmit(e, user.id, 'ban')}>Ban</Button>{' '}
                            <Button variant="success" onClick={(e) => onSubmit(e, user.id, 'unban')}>Unban</Button>{' '}
                            <Button variant="primary" onClick={(e) => onSubmit(e, user.id, 'activate')}>Activate search</Button>{' '}
                            <Button variant="secondary" onClick={(e) => onSubmit(e, user.id, 'deactivate')}>Deactivate search</Button>{' '}
                            <Button color="error" onClick={(e) => onSubmit(e,'add_moderator')}>Add Moderator</Button>{' '}
                            <Button color="error" onClick={(e) => onSubmit(e,'add_admin')}>Add Admin</Button>{' '}
                            <Dropdown>
                                <Dropdown.Toggle variant="info" id="subscription-dropdown">
                                Give subscription
                                </Dropdown.Toggle>

                                <Dropdown.Menu>
                                <Dropdown.Item onClick={() => handleGiveSubscription(user.id, 'user-more-likes')}>More Likes</Dropdown.Item>
                                <Dropdown.Item onClick={() => handleGiveSubscription(user.id, 'user-more-likes-and-map')}>More Likes and Map</Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown>{' '}
                            <Button variant="warning" onClick={() => handleDeleteSubscription(user.id)}>Delete subscription</Button>
                            </td>
                        </tr>
                        ))} */}
                        <tr key='1'>
                            <td>Глеб</td>
                            <td>Standart User</td>
                            <td>User</td>
                            <td>false</td>
                            <td>true</td>
                            <td className="td-buttons">
                            <Button variant="danger" onClick={(e) => onSubmit(e, 1, 'ban')}>Ban</Button>{' '}
                            <Button variant="success" onClick={(e) => onSubmit(e, 1, 'unban')}>Unban</Button>{' '}
                            <Button variant="primary" onClick={(e) => onSubmit(e, 1, 'activate')}>Activate search</Button>{' '}
                            <Button variant="secondary" onClick={(e) => onSubmit(e, 1, 'deactivate')}>Deactivate search</Button>{' '}
                            <Button color="error" onClick={(e) => onSubmit(e,'add_moderator')}>Add Moderator</Button>{' '}
                            <Button color="error" onClick={(e) => onSubmit(e,'add_admin')}>Add Admin</Button>{' '}
                            <Dropdown>
                                <Dropdown.Toggle variant="info" id="subscription-dropdown">
                                Give subscription
                                </Dropdown.Toggle>

                                <Dropdown.Menu>
                                <Dropdown.Item onClick={() => handleGiveSubscription(1, 'user-more-likes')}>More Likes</Dropdown.Item>
                                <Dropdown.Item onClick={() => handleGiveSubscription(1, 'user-more-likes-and-map')}>More Likes and Map</Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown>{' '}
                            <Button variant="warning" onClick={() => handleDeleteSubscription(1)}>Delete subscription</Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </>
        : 
        <PageNotFound />
        }
        
        
        </div>
    )
}

export default AdminPage;


// _GNk52920