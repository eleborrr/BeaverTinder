import { useState, useEffect } from "react";
import Cookies from 'js-cookie';
import jwtDecode from "jwt-decode";
import BeaverCard from "../Components/BeaverCard";
import { axiosInstance } from "../Components/axios_server";
import { GeoMap } from "../Components/geolocation_map";
import './../assets/css/map_style.css'

const LikePage = () =>
{
    const token = Cookies.get('token');
    const [likeLimit, setLikeLimit] = useState(false);
    const [userLimit, setUserLimit] = useState(false);
    const [geolocationAvailable, setGeolocationAvailable] = useState(false);
    const [profile, setProfile] = useState();
    const [long, setLong] = useState();
    const [lant, setLant] = useState();

    useEffect(() => {
        GetNewBearer();
    }, [])


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

    function GetGeolocation(prof) {
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
                console.log(res.data.latitude);
                if (res.data.longitude){
                    console.log(res.data.longitude)
                    setLong(res.data.longtitude);
                }
                if (res.data.latitude){
                    setLant(res.data.latitude);
                }
            }
        })
        .catch()
    }
    
    function GetNewBearer() {
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
                CheckGeolocationAvailable(jwtDecode(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
                GetGeolocation(res.data);
            }
        }
        })
        .catch();
   
    }

    function CheckGeolocationAvailable(array)
    {
        if (Array.isArray(array)) {
            array.some(element => {
                if (element === "UserMoreLikesAndMap" || element === "Admin" || element === "Moderator")
                {
                    setGeolocationAvailable(true);
                }
            })
        }
        
    }

    return (
    <div>
        { userLimit? 
        <div> 
            Пользователи закончились, ждите новых
        </div>
        :
        <div>
        {likeLimit? <p>У вас закончились лайки</p>:
        <div>
            {profile ? 
            <div>
                <BeaverCard profile = {profile} like = {like} dislike = {dislike}></BeaverCard>
                {geolocationAvailable?
                <div className="div_map">
                    <GeoMap latitude={lant ? lant : 55.81441} longitude={long ? long : 49.12068} />
                </div>
                :
                <div>
                    Купите подписку чтобы увидеть геолокацию
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