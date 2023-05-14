import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";

const HomePage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    return(
        <>
        <h1>It's home page!</h1>
        </>
    )
}

export default HomePage;