import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import { axiosInstance } from "../Components/axios_server";
import Cookies from "js-cookie";

const RegisterPage = () => {
    const [lName, setLName] = useState('Last name')
    const [fName, setFName] = useState('First name')
    const [nName, setNName] = useState('Nickname')
    const [email, setEmail] = useState('someEmail@gmail.com')
    const [pass, setPass] = useState('')
    const [confPass, setConfPass] = useState('')
    const [gender, setGender] = useState('')
    const [about, setAbout] = useState('Hi! I use BeaverTinder!')
    const [errMess, setErrMess] = useState('')
    const [respStatus, setRespStatus] = useState(false)
    const [errorCode, setErrorCode] = useState('') 
    const [respErrData, setRespErrData] = useState('')
    const navigate = useNavigate()

    const ValidatePass = (e) => {
        setConfPass(e.target.value)
        if (e.target.value !== pass){
            setErrMess("Пароли не совпадают!")
        } else {
            setErrMess('')
            setConfPass(e.target.value)
        }
    }

    const onSubmit = (e) => {
        e.preventDefault();
        setRespErrData('');
        if (pass !== confPass){
            setErrMess('Пароли не совпадают!');
            return;
        }
        try {
        axiosInstance
            .post('/registration', {
                LastName: lName,
                FirstName: fName,
                UserName: nName,
                Email: email,
                Password: pass,
                ConfirmPassword: confPass,
                Gender: gender,
                About: about
            })
            .then(function (res) {
                console.log(res);
                const data = res.data;
                if(data.successful === true){
                    setRespStatus(true);
                }
                else{
                    setRespErrData(data.message);
                }
            })
            .catch(function(error) {
                console.log('catch');
                if (error.status){
                    setRespStatus(error.status);
                }else{
                    setErrorCode(error.code);
                }
            })
        }
        catch(error){
            console.log(error);
        }

    };

    useEffect(() => {
        if (Cookies.get('token')){
            navigate('/home');
        }
    }, []);
    
    

    return (
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
                    <div className="col-lg-8 col-7">
                        <div className="logo">
                            <a href="index.html"><img src="assets/images/logo/logo.png" alt="logo" /></a>
                        </div>
                    </div>
                    <div className="col-lg-4 col-5">
                        <a href="/home" className="backto-home"><i className="fas fa-chevron-left"></i> Back to Home</a>
                    </div>
                </div>
            </div>
        </div>
        <div className="container">
            <div className="row">
                <div className="image">
                </div>
                <div className="col-lg-7">
                    <div className="log-reg-inner">
                        <div className="section-header">
                            <h2 className="title">Welcome to BeaverTinder </h2>
                            <p>Let's create your profile! Just fill in the fields below, and we’ll get a new account. </p>
                        </div>
                        <div className="main-content">
                            {errorCode === 'ERR_NETWORK' &&
                                <>
                                    <h1>We have problem with server connection, please try again later =(</h1>
                                    <a href={'/register'}><button className="default-btn reverse">Register</button> </a>
                                </>
                            }
                            {
                                respStatus && 
                                <>
                                    <h1>Congratulation! We created your account, now you need to confirm your email address ;)</h1>
                                </>
                            }
                            { errorCode !== 'ERR_NETWORK' && !respStatus &&
                            <form onSubmit={onSubmit}>
                                <h4 className="content-title">Acount Details</h4>
                                <div className="form-group">
                                    <label>Last name</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Last name" value={lName} onChange={(e) => setLName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>First name</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your First name" value={fName} onChange={(e) => setFName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Nickname</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Nickname" value={nName} onChange={(e) => setNName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Email</label>
                                    <input type="email" className="my-form-control" placeholder="Enter Your Email" value={email} onChange={(e) => setEmail(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Password</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Password" onChange={(e) => {
                                        setPass(e.target.value);
                                       
                                        if (e.target.value !== confPass){
                                            setErrMess('Пароли не совпадают!');
                                        } else {
                                            setErrMess('');
                                        }
                                     }}/>
                                </div>
                                <div className="form-group">
                                    <label>ConfirmPassword</label>
                                    <input type="text" className="my-form-control" placeholder="Confirm Your Password" onChange={(e) => ValidatePass(e)}/>
                                    <span>{errMess}</span>
                                </div>
                                <h4 className="content-title mt-5">Profile Details</h4>
                                <div className="form-group">
                                    <label>Gender</label>
                                    <div className="banner__inputlist">
                                        <div className="s-input me-3">
                                            <input type="radio" name="gender1" id="males1" onClick={() => setGender('Man')} />
                                            <label htmlFor="males1">Man</label>
                                        </div>
                                        <div className="s-input me-3">
                                            <input type="radio" name="gender1" id="females1" onClick={() => setGender('Woman')}/>
                                            <label htmlFor="females1">Woman</label>
                                        </div>
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label>Tell about yourself</label>
                                    <input type="text" className="my-form-control" placeholder="Tell about yourself" value={about} onChange={(e) => setAbout(e.target.value)}/>
                                </div>
                                <span>{respErrData}</span><br />
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm"><span>Create Your Profile</span></button>
                            </form>
                            }
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
	<script src="https://www.google-analytics.com/analytics.js" async></script>
</>
    )
}


export default RegisterPage;