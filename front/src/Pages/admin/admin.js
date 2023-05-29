import React, { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import { Input, Button } from "@mui/material";
import jwtDecode from "jwt-decode";
import Cookies from "js-cookie";
import { axiosInstance } from "../../Components/axios_server";
import PageNotFound from './../404';
import "../../assets/css/admin.css"

const AdminPage = () => {
    const [isAvailable, setIsAvailable] = useState(false);
    const [func, setFunc] = useState('');
    const [users, setUsers] = useState([]);
    const [viewUsers, setViewUsers] = useState([]);
    const [isAdmin, setIsAdmin] = useState(false);
    const token = Cookies.get('token');

    const onSubmit = (e, userid, func) => {
        setFunc(func);
        e.preventDefault();
        functionOnUser(func, userid);
    };

    useEffect( () => {   
        const roles = jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];    
        if(Array.isArray(roles))
        {
        roles.some(element => {
            if (element === "Moderator")
            {
                setIsAvailable(true);
            }
            if(element === "Admin")
            {
                setIsAvailable(true);
                setIsAdmin(true);
            }
        })
        }
        else
        {
            setIsAvailable(roles == "Moderator" || roles == "Admin");
            setIsAdmin(roles == "Admin");
        }

        getAllUsersAxios();

    }, [])

    const handleGiveSubscription = (userId, subscriptionType) => {
        // обработка выбора типа подписки
      };

    const handleDeleteSubscription = (userId) => {
        // обработка нажатия кнопки delete subscription
      };

    const handleSearch = (event) => {
        setViewUsers(users.filter(u => u.userName.toLowerCase().includes(event.target.value.trim().toLowerCase())))
      };


    function functionOnUser(func, userId) {
        axiosInstance.post('/' +func, 
        {
            UserId : userId,
        }, 
        {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            },
        })
        .then(function (response) {
            setUsers(response.data)
            setViewUsers(response.data)
        })
        .catch(function(error) {
        })
    }

    function getAllUsersAxios(){
        axiosInstance.get('/all',
        {
            headers: {
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {
            console.log(res.data);
            setUsers(res.data)
            setViewUsers(res.data)
        })
    }


    return (
        <div>
        {isAvailable? 
        <>
            <div className="refr-button-div">
                <button type="submit" className="refr-button" onClick={getAllUsersAxios}>Refresh User List</button>
            </div>
            <div className="form-group">
                <label className="search-label">Search</label>
                <input type="text" className="search-input" onChange={handleSearch} />
            </div>
                <table>
                    <thead>
                        <tr>
                        <th>Имя</th>
                        <th>Роль</th>
                        <th>Бан</th>
                        <th>Поиск</th>
                        <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody>
                        {viewUsers.map(user => (
                        <tr key={user.id}>
                            <td>{user.userName}</td>
                            {/* <td>{user.subInfo}</td> */}
                            <td>{user.subName}</td>
                            <td>{user.isBlocked.toString()}</td>
                            <td>{user.isSearching.toString()}</td>
                            <td>
                            {isAdmin? <div>
                                <Button variant="danger" onClick={(e) => onSubmit(e, user.id, 'ban')}>Ban</Button>{' '}
                                <Button variant="success" onClick={(e) => onSubmit(e, user.id, 'unban')}>Unban</Button>{' '}
                            </div>: <div></div>}
                            <Button variant="primary" onClick={(e) => onSubmit(e, user.id, 'activate')}>Activate search</Button>{' '}
                            <Button variant="secondary" onClick={(e) => onSubmit(e, user.id, 'deactivate')}>Deactivate search</Button>{' '}
                            <Button color="error" onClick={(e) => onSubmit(e, user.id, 'add_moderator')}>Add Moderator</Button>{' '}
                            {isAdmin? <div><Button color="error" onClick={(e) => onSubmit(e, user.id, 'add_admin')}>Add Admin</Button>{' '}</div>: <div></div>}
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
                        ))}
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

