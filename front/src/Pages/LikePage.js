import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from 'js-cookie';
import jwtDecode from "jwt-decode";
import BeaverCard from "../Components/BeaverCard";
import { axiosInstance } from "../Components/axios_server";
import { GeoMap } from "../Components/geolocation_map";
import './../assets/css/map_style.css'
import { useCallback } from "react";

const LikePage = () =>
{
    const token = Cookies.get('token');
    const [likeLimit, setLikeLimit] = useState(false);
    const [userLimit, setUserLimit] = useState(false);
    const [geolocationAvailable, setGeolocationAvailable] = useState(false);
    const [profile, setProfile] = useState();
    const [long, setLong] = useState();
    const [lant, setLant] = useState();
    const [distance, setDistance] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [navigate, token])

    const GetGeolocation = useCallback((prof) => {
        if (!CheckGeolocationAvailable())
            return;
        axiosInstance.post("/geolocation",{
            userId : prof.id
        },
         {
            headers: {
                Authorization: `Bearer ${token}`,
                Accept: 'application/json'
            },
        })
        .then(res => {
            if (res.data){
                if (res.data.longtitude){
                    setLong(res.data.longtitude);
                }
                if (res.data.latitude){
                    setLant(res.data.latitude);
                }
            }
        })
        .catch()
    }, [token])

    const GetNewBearer = useCallback(() => {
        axiosInstance.get('/beaversearch',
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(res => {
                if(res.data.message === "Beaver queue error")
                {
                    setUserLimit(true);
                }
                else
                {
                setProfile(res.data);
    
                if (res.data){
                    setGeolocationAvailable(false);
                    CheckGeolocationAvailable();
                    GetGeolocation(res.data);
                    setDistance(res.data.distance);
                }
            }
            })
            .catch();
        }, [GetGeolocation, token])

    useEffect(() => {
        GetNewBearer();
    }, [GetNewBearer])


    function like () {
        
        axiosInstance.post('/like', { 
            LikedUserId: profile.id 
        }, {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {
            if(res.data.message === "Like limit!")
            {
                setLikeLimit(true);
            }
            else
            {
                GetNewBearer();
            }
            
        })
        .catch();
    }

    function dislike () {
        axiosInstance.post('/dislike', { 
            LikedUserId: profile.id 
        }, {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {
            if(res.data.message === "Like limit!")
            {
                setLikeLimit(true);
            }
            else
            {
                GetNewBearer();
            }
        })
        .catch();

    }

    function CheckGeolocationAvailable()
    {
        let array = jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        if (Array.isArray(array)) {
            array.some(element => {
                if (element === "UserMoreLikesAndMap" || element === "Admin" || element === "Moderator")
                {
                    setGeolocationAvailable(true);
                }
                return element;
            })
        }
        
    }

    return (
    <div>
        { userLimit? 
        <div> 
            <h1>Wait for new Users. </h1>
        </div>
        :
        <div>
        {likeLimit? <h1>You{`&apos`}r days limit ends</h1>:
        <div>
            {profile ? 
            <div>
                <BeaverCard profile = {profile} like = {like} dislike = {dislike} distance={distance}></BeaverCard>
                {geolocationAvailable?
                <div className="div_map">
                    <GeoMap latitude={lant ? lant : 55.81441} longitude={long ? long : 49.12068} />
                </div>
                :
                <div>
                    <h1>Buy subscription to get access to geolocation</h1>
                </div>
                }
            
            </div>:
            <h1>Downloading</h1>
            }

        </div>
        }
        </div>
    }

    </div>
    )
    
}



export default LikePage;