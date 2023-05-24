import React, { useEffect, useState } from "react";
import ReactDOM from "react-dom";
import { Admin, Resource } from "react-admin";
import { axiosInstance } from "../../Components/axios_server";
import { Input, Button } from "@mui/material";

import users from "../../Components/users"

import "../../assets/css/admin.css"
import axios from "axios";


const AdminPage = () => {
     const [isAvailable, setIsAvailable] = useState(false);
    const [userId, setUserId] = useState('');
    const [func, setFunc] = useState('');
    const [isReady,setReady] = useState(false);

    const onSubmit = (e, func) => {
        setFunc(func);
        e.preventDefault();
        setReady(true);
    };

    useEffect( () => {
        axiosInstance
        .get('/ban')
        .then(function(response) {
            setIsAvailable(true);
        })
        .catch(function(err) {
            setIsAvailable(false);
        })
    }, []
    )

    useEffect(() => {
        if(!isReady) {
            return;
        }
        functionOnUser(func);
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


    return (
        <div>
        {isAvailable? <div className="adminInput">
            <Input placeholder="User Name" onChange={(e) => setUserId(e.target.value)}/> 
            <p/>
            <Button color="error" onClick={(e) => onSubmit(e, 'ban')}>Ban</Button>
            <p/>
            <Button color="error" onClick={(e) => onSubmit(e, 'unban')}>Unban</Button>
            <p/>
            <Button color="error" onClick={(e) => onSubmit(e,'deactivate')}>Deactivate  Search</Button>
            <p/>
            <Button color="error" onClick={(e) => onSubmit(e,'activate')}>Activate  Search</Button>
            <p/>
            <Button color="error" onClick={(e) => onSubmit(e,'add_moderator')}>Add Moderator</Button>
            {/* <p/>
            <Button color="error" onClick={(e) => onSubmit(e,'add_admin')}>Add Admin</Button> */}

        </div> : <div> not available</div>}
        
        
        </div>
    )
}

export default AdminPage;


// _GNk52920