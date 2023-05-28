import React, { useEffect, useState } from "react";
import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { Input, Button } from "@mui/material";
import { axiosInstance } from "../../Components/axios_server";
import PageNotFound from './../404';
import "../../assets/css/admin.css"

const AdminPage = () => {
    const [isAvailable, setIsAvailable] = useState(false);
    const [userId, setUserId] = useState('');
    const [func, setFunc] = useState('');
    const [isReady,setReady] = useState(false);
    const token = Cookies.get('token');

    const onSubmit = (e, func) => {
        setFunc(func);
        e.preventDefault();
        setReady(true);
    };

    useEffect( () => {
       jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].some(element => {
            if (element === "Admin")
            {
                setIsAvailable(true);
            }
        })
    }, [])

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

        </div> : <PageNotFound />}
        
        
        </div>
    )
}

export default AdminPage;


// _GNk52920