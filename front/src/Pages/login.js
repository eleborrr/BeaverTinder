import { useEffect, useState } from "react"; 
import { useNavigate } from "react-router-dom"; 
import Cookies from 'js-cookie'; 
import {axiosInstance} from "./../Components/axios_server"; 
import vk from './../assets/images/login/vk.png' 
import './../assets/css/login.css' 
 
const LoginPage = () => { 
 
    const [userName, setUserName] = useState('') 
    const [password, setPassword] = useState('') 
    const [rememberMe, setRememberMe] = useState(false) 
    const [errMessage, setErrMessage] = useState('') 
    const [spanClass, setSpanClass] = useState('hide') 
    const navigate = useNavigate() 
 
    const handleOauth = () => { 
        document.location.replace('https://oauth.vk.com/authorize?client_id=51656119&redirect_uri=http://localhost:3000/afterCallback&display=page&scope=4195332&state=huipenis');
    } 
    const onSubmit = (e) => { 
        e.preventDefault(); 
 
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
            } 
            else{ 
                console.log(res.data); 
                Cookies.set('token', res.data.message); 
                navigate('/home'); 
            } 
        }) 
        .catch((err) => { 
            console.log(err); 
        }); 
    }; 
 
    useEffect(() => { 
        if(Cookies.get('token')){ 
            navigate('/home'); 
        } 
    }) 
 
    // function login() { 
    //     alert() 
    //     axiosInstance 
    //     .post('/login', { 
    //         UserName: userName, 
    //         Password: password, 
    //         RememberMe: rememberMe 
    //     }) 
    //     .then(function (response) { 
    //         console.log(response); 
    //     }) 
    //     .catch(function(error) { 
    //         console.log(error); 
    //     }) 
 
    // } 
    return( 
        <> 
    {/* <div className="preloader"> 
        <div className="preloader-inner"> 
            <div className="preloader-icon"> 
                <span></span> 
                <span></span> 
            </div> 
        </div> 
    </div> */} 
 
    <a href="#" className="scrollToTop"><i className="fa-solid fa-angle-up"></i></a> 
 
 
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
                                    <label >Имя пользователя</label> 
                                    <input type="text" className="my-form-control" name="UserName" onChange={(e) => setUserName(e.target.value)} placeholder="Enter Your Nickname" /> 
                                </div> 
                                <div className="form-group"> 
                                    <label >Пароль</label> 
                                    <input type="password" className="my-form-control" name="Password" onChange={(e) => setPassword(e.target.value)} placeholder="Enter Your Password" /> 
                                </div> 
                                <div className="checkbox-form"> 
                                    <label >Запомнить?</label> 
                                    <input type="checkbox"
                                className="checkboxRemember" name="RememberMe" onChange={() => setRememberMe(!rememberMe)} /> 
                                </div> 
                                <p className="f-pass">Forgot your password? <a href="#">recover password</a></p> 
                                <span className={spanClass}>{errMessage} Попробуйте ещё раз</span> 
                                <div className="text-center"> 
                                    <button type="submit" className="default-btn" onClick={onSubmit}><span>Sign IN</span></button> 
                                </div> 
                                <div className="or"> 
                                    <p>OR</p> 
                                </div> 
                                <div className="or-content"> 
                                    <p>Sign up with your vk</p> 
                                        <a onClick={() => handleOauth()} className="default-btn reverse"><img src={vk} alt="vk" /> <span>Sign Up with VK</span></a> 
                                    <p className="or-signup"> Don't have an account? <a href="/register">Sign up here</a></p> 
                                </div> 
                            </form> 
                        </div> 
                    </div> 
                </div> 
            </div> 
        </div> 
    </section> 



<script src="assets/js/vendor/jquery-3.6.0.min.js"></script> 
<script src="assets/js/vendor/modernizr-3.11.2.min.js"></script> 
<script src="assets/js/isotope.pkgd.min.js"></script> 
<script src="assets/js/swiper.min.js"></script> 
<script src="assets/js/all.min.js"></script>  
<script src="assets/js/wow.js"></script> 
<script src="assets/js/counterup.js"></script> 
<script src="assets/js/jquery.countdown.min.js"></script> 
<script src="assets/js/lightcase.js"></script> 
<script src="assets/js/waypoints.min.js"></script> 
<script src="assets/js/vendor/bootstrap.bundle.min.js"></script> 
<script src="assets/js/plugins.js"></script> 
<script src="assets/js/main.js"></script> 
</> 
) 
} 

export default LoginPage;
