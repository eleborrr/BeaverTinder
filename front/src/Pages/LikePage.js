import { useState, useEffect } from "react";
import Cookies from 'js-cookie';
import jwtDecode from "jwt-decode";
import BeaverCard from "../Components/BeaverCard";
import { axiosInstance } from "../Components/axios_server";
import { GeoMap } from "../Components/geolocation_map";
import './../assets/css/map_style.css'
import { to } from "@react-spring/web";

const LikePage = () =>
{
    const token = Cookies.get('token');
    const [error, setError] = useState('');
    const [profile, setProfile] = useState();
    const [long, setLong] = useState();
    const [lant, setLant] = useState();
    const [geolocationNotAvailable, setGeolocationNotAvailable] = useState(true);

    useEffect(() => {
        GetNewBearer();
    }, [])

    // С ГОНКАМИ БРАТ НОРМ ВСЕ ДА?

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
            GetNewBearer();
        })
        .catch(error => setError(error));
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
            GetNewBearer();
        })
        .catch(error => setError(error));

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
                if (res.data.longitude){
                    setLong(res.data.longitude);
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
            setProfile(res.data);
            if (res.data){
                GetGeolocation(res.data);
            }
        })
        .catch(error => setError(error));
   
    }

    function CheckGeolocation(){
        console.log(jwtDecode(token))
    }

    return (<div>
        {/* <BeaverCard person={{name: 'Arun', url: 'https://cdn.hashnode.com/res/hashnode/image/upload/v1644176959380/tNxVpeCE0.png'}}> </BeaverCard> */}
        {error == ''
        ? 
        <div>
            {profile ? 
            <div>
                <BeaverCard profile = {profile} like = {like} dislike = {dislike}></BeaverCard>
                {geolocationNotAvailable? 
                <div> 

                </div>
                : 
                <div className="div_map">
                    <GeoMap latitude={lant ? lant : 55.81441} longitude={long ? long : 49.12068} />
                </div>
                }
            
            </div>:
            <h1>Downloading</h1>
            }
            
        </div>
        : 
        <div>
            <p>У Вас закончились лайки или же вы проставили лайки всем</p>
        </div>
        }
        <button onClick={CheckGeolocation}>Check geolocation</button>
    </div>
    )

}



export default LikePage;