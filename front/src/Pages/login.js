import { useEffect, useState } from "react"; 
import { useNavigate } from "react-router-dom"; 
import Cookies from 'js-cookie'; 
import {axiosInstance} from "./../Components/axios_server"; 
import vk from './../assets/images/login/vk.png' 
import './../assets/css/login.css' 
import TokenName from "../Components/token_constant_name";
 
const LoginPage = () => { 
 
    const [userName, setUserName] = useState('') 
    const [password, setPassword] = useState('') 
    const [errorsLogin, setErrorsLogin] = useState([]) 
    const [rememberMe, setRememberMe] = useState(false) 
    const [errMessage, setErrMessage] = useState('') 
    const [spanClass, setSpanClass] = useState('hide') 
    const navigate = useNavigate() 
 
    const handleOauth = () => { 

        document.location.replace('https://oauth.vk.com/authorize?client_id=51656119&redirect_uri=http://localhost:3000/afterCallback&display=page&scope=4195332&state=huipenis');
    } 
    const onSubmit = (e) => { 
        e.preventDefault(); 
        setErrMessage(""); 
        setSpanClass('hide'); 
 
        axiosInstance.post('/login', { 
            userName: userName, 
            password: password, 
            rememberMe: rememberMe, 
            returnUrl: "" 
        }) 
        .then((res) => { 
            if (!res.data.successful){ 
                setSpanClass('errorMessage'); 
                setErrMessage(res.data.message); 
                setErrorsLogin('')
            } 
            else{ 
                Cookies.set(TokenName, res.data.message, {expires: 1}); 
                document.location.replace(`/home`);
            } 
        }) 
        .catch((err) => { 
            setErrorsLogin(err["response"]["data"]["errors"]);
        }); 
    }; 
 
    useEffect(() => { 
        if(Cookies.get(TokenName)){ 
            navigate('/home'); 
            Cookies.set(TokenName, "error");
        } 
        Cookies.set(TokenName, "error");
        Cookies.remove(TokenName);
    }) 

    const HandleEnterPress = (event) => {
        if (event.key === 'Enter')
            onSubmit(event);
    }
    
    return( 
        <> 
 
    <a href="/" className="scrollToTop"><i className="fa-solid fa-angle-up"></i></a> 
 
 
    <section className="log-reg"> 
        <div className="top-menu-area"> 
            <div className="container"> 
                <div className="row"> 
                    <div className="col-lg-4 col-5"> 
                        <a href="index.html" className="backto-home"><i className="fas fa-chevron-left"></i> Back to Home</a> 
                    </div> 
                </div> 
            </div> 
        </div> 
        <div className="container"> 
            <div className="row"> 
                <div className="image image-log"></div> 
                <div className="col-lg-7"> 
                    <div className="log-reg-inner"> 
                        <div className="section-header inloginp"> 
                            <h3 className="title">Welcome to BeaverTinder!</h3> 
                        </div> 
                        <div className="main-content inloginp"> 
                            <form> 
                                <div className="form-group"> 
                                    <label >Nickname</label> 
                                    <input type="text" 
                                        className="my-form-control" 
                                        name="UserName" 
                                        onChange={(e) => setUserName(e.target.value)} 
                                        placeholder="Enter Your Nickname" 
                                        onKeyDown={(event) => HandleEnterPress(event)}/> 
                                </div> 
                                <div className="form-group"> 
                                    <label >Password</label> 
                                    <input type="password" 
                                        className="my-form-control" 
                                        name="Password" 
                                        onChange={(e) => setPassword(e.target.value)} 
                                        placeholder="Enter Your Password" 
                                        onKeyDown={(event) => HandleEnterPress(event)}/> 
                                </div> 
                                <div className="checkbox-form"> 
                                    <label >Remember me?</label> 
                                    <input type="checkbox"
                                className="checkboxRemember" name="RememberMe" onChange={() => setRememberMe(!rememberMe)} /> 
                                </div> 
                                <p className="f-pass">Forgot your password? <a href="/">recover password</a></p> 
                                <span className={spanClass}>{errMessage} Try again!</span> 
                                {errorsLogin === null || errorsLogin === undefined ? 
                                    <></>
                                    :
                                    <>
                                        {errorsLogin["Password"] ?
                                                errorsLogin["Password"].map(err => (
                                                    <><span>{err}</span><br/></>
                                                ))
                                            :
                                            <></>
                                        }
                                        {errorsLogin["UserName"] ?
                                                errorsLogin["UserName"].map(err => (
                                                    <><span>{err}</span><br/></>
                                                ))
                                            :
                                            <></>
                                        }
                                    </>
                                }
                                <div className="text-center"> 
                                    <button type="submit" className="default-btn" onClick={onSubmit}><span>Sign IN</span></button> 
                                </div> 
                                <div className="or"> 
                                    <p>OR</p> 
                                </div> 
                                <div className="or-content"> 
                                    <p>Sign up with your vk</p> 
                                        <a href="/" onClick={() => handleOauth()} className="default-btn reverse"><img src={vk} alt="vk" className="vk-logo"/> <span>Sign Up with VK</span></a> 
                                    <p className="or-signup"> Don't have an account? <a href="/register">Sign up here</a></p> 
                                </div> 
                            </form> 
                        </div> 
                    </div> 
                </div> 
            </div> 
        </div> 
    </section> 
</> 
) 
} 

export default LoginPage;
