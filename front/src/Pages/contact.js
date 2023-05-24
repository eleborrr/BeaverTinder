import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";

const ContactPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    return(
        <>
        <h1>It's contact page!</h1>
        </>
    )
}

export default ContactPage;