import { useState, useEffect } from "react";
import BeaverCard from "../Components/BeaverCard";
import { axiosInstance } from "../Components/axios_server";
import Cookies from 'js-cookie';


const LikePage = () =>
{
    const [token, setToken] = useState('');
    const [profile, setProfile] = useState();

    useEffect(() => {
        setToken(Cookies.get('token'))
        GetNewBearer();
    }, [])

    // С ГОНКАМИ БРАТ НОРМ ВСЕ ДА?

    function like () {
        
        axiosInstance.post('/like',
        {
            headers: {
                Authorization: `Bearer ${Cookies.get('token')}`,
            },
            LikedUserId: profile.id

        })
        .then(res => {
            GetNewBearer();
        })
    }

    function dislike () {
        axiosInstance.post('/dislike',
        {
            headers: {
                Authorization: `Bearer ${token}`,
            },
            LikedUserId: profile.id
        })
        .then(res => {
            GetNewBearer();
        })

    }

    function GetNewBearer() {
    axiosInstance.get('/beaversearch',
        {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
        .then(res => {
            setProfile(res.data)})
    }

    return (<div>
        {/* <BeaverCard person={{name: 'Arun', url: 'https://cdn.hashnode.com/res/hashnode/image/upload/v1644176959380/tNxVpeCE0.png'}}> </BeaverCard> */}
        {profile? <BeaverCard profile = {profile} like = {like} dislike = {dislike}></BeaverCard>: <h1>Downloading</h1>}
    </div>
    )

}



export default LikePage;