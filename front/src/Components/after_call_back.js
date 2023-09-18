import { useEffect } from 'react';
import { axiosInstance } from './axios_server';
import Cookies from 'js-cookie';


export const OAuthAfterCallback = () => {
    const query = window.location.search;
    const queryParams = new URLSearchParams(query);
    const code = queryParams.get('code');

    useEffect(() => {
        axiosInstance.get(`/login/getAccessToken?code=${code}`)
        .then(res => {
            if(res.data){
                Cookies.set('token', res.data);
            }
            })
            setTimeout(() => {
                document.location.replace(`/home`);
              }, 1000);
    }, [code])
    return(
        <>
        </>
    )
}