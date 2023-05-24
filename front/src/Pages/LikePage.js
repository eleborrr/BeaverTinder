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
    const [profile, setProfile] = useState();
    const [long, setLong] = useState();
    const [lant, setLant] = useState();

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
    }

    function dislike () {
        console.log(token);
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
                    setLong(res.data.latitude);
                }
            }
        })
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
            console.log(jwtDecode(token));
        });
   
    }

    return (<div>
        {/* <BeaverCard person={{name: 'Arun', url: 'https://cdn.hashnode.com/res/hashnode/image/upload/v1644176959380/tNxVpeCE0.png'}}> </BeaverCard> */}
        {profile
        ? 
        <div>
            <BeaverCard profile = {profile} like = {like} dislike = {dislike}></BeaverCard>
            <div className="div_map">
                <GeoMap latitude={lant ? lant : 55.81441} longitude={long ? long : 49.12068} />
            </div>
        </div>
        : 
        <h1>Downloading</h1>}
    </div>
    )

}



export default LikePage;