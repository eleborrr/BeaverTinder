import {axiosInstance} from "../Components/axios_server";
import { useEffect, useState } from "react";

const LoginPage = () => {

    const [userName, setUserName] = useState('')
    const [password, setPassword] = useState('')
    const [rememberMe, setRememberMe] = useState(false)
    const [isReady, setIsReady] = useState(false);


    const onSubmit = (e) => {
        e.preventDefault();
        setIsReady(true);
    };

    useEffect(() => {
        if(!isReady) {
            return;
        }

        axiosInstance.post('/login', {
            UserName: userName,
            Password: password,
            RememberMe: rememberMe,
            withCredentials: true
        })
        .then((res) => {
            console.log(res);
            setIsReady(false)
        })
        .catch((err) => {
            console.log(err);
            setIsReady(false);
        });
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


    <section className="log-reg">d
        <div className="top-menu-area">
            <div className="container">
                <div className="row">
                    <div className="col-lg-8 col-7">
                        <div className="logo">
                            <a href="index.html"><img src="assets/images/logo/logo.png" alt="logo" /></a>
                        </div>
                    </div>
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
                            <h2 className="title">Welcome to Ollya</h2>
                        </div>
                        <div className="main-content inloginp">
                            <form>
                                <div className="form-group">
                                    <label >Your Address</label>
                                    <input type="text" className="my-form-control" name="UserName" onChange={(e) => setUserName(e.target.value)} placeholder="Enter Your Email" />
                                </div>
                                <div className="form-group">
                                    <label >Password</label>
                                    <input type="password" className="my-form-control" name="Password" onChange={(e) => setPassword(e.target.value)} placeholder="Enter Your Password" />
                                </div>
                                <p className="f-pass">Forgot your password? <a href="#">recover password</a></p>
                                <div className="text-center">
                                    <button type="submit" className="default-btn" onClick={onSubmit}><span>Sign IN</span></button>
                                </div>
                                <div className="or">
                                    <p>OR</p>
                                </div>
                                <div className="or-content">
                                    <p>Sign up with your email</p>
                                    <a href="#" className="default-btn reverse"><img src="assets/images/login/google.png" alt="google" /> <span>Sign Up with Google</span></a>
                                    <p className="or-signup"> Don't have an account? <a href="register.html">Sign up here</a></p>
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