import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";

const BlogsPage = () => {

    const navigate = useNavigate();
    const token = Cookies.get('token');

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])
    
    return(
        <>
        <h1>It's blogs page!</h1>
        </>
    )
}

export default BlogsPage;