import { useEffect, useState } from "react";
import { Link, useNavigate } from 'react-router-dom';
import { axiosInstance } from "../Components/axios_server";

const RegisterPage = () => {
    const [lName, setLName] = useState('')
    const [fName, setFName] = useState('')
    const [nName, setNName] = useState('')
    const [email, setEmail] = useState('')
    const [pass, setPass] = useState('')
    const [confPass, setConfPass] = useState('')
    const [gender, setGender] = useState('')
    const [about, setAbout] = useState('')
    const [errMess, setErrMess] = useState('')
    const [respStatus, setRespStatus] = useState(0)
    const [errorCode, setErrorCode] = useState('') 
    const navigate = useNavigate()

    const ValidatePass = (e) => {
        if (e.target.value !== pass){
            setErrMess("Password doesn't match")
        } else {
            setErrMess('')
            setConfPass(e.target.value)
        }
    }

    const onSubmit = (e) => {
        e.preventDefault();
        console.log('start');
        try {
        axiosInstance
            .post('/Registration', {
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
                const token = res.data;
                localStorage.setItem('token', token);
                setRespStatus(res.status);
            })
            .catch(function(error) {
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
        if(respStatus === 200 ){
            navigate('/home')
        }
    };
    useEffect(() => {
        if (localStorage.getItem('token')){
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
                            { errorCode !== 'ERR_NETWORK' &&
                            <form onSubmit={onSubmit}>
                                <h4 className="content-title">Acount Details</h4>
                                <div className="form-group">
                                    <label>Last name</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Last name" value="Last name" onChange={(e) => setLName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>First name</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your First name" value="First name" onChange={(e) => setFName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Nickname</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Nickname" value="Nickname" onChange={(e) => setNName(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Email</label>
                                    <input type="email" className="my-form-control" placeholder="Enter Your Email" value="someEmail@gmail.com" onChange={(e) => setEmail(e.target.value)}/>
                                </div>
                                <div className="form-group">
                                    <label>Password</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Password" onChange={(e) => setPass(e.target.value)}/>
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
                                            <input type="radio" name="gender1" id="males1" onChange={() => setGender('Man')} checked={true}/>
                                            <label htmlFor="males1">Man</label>
                                        </div>
                                        <div className="s-input">
                                            <input type="radio" name="gender1" id="females1" onChange={() => setLName('Woman')}/>
                                            <label htmlFor="females1">Woman</label>
                                        </div>
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label>Tell about yourself</label>
                                    <input type="text" className="my-form-control" placeholder="Tell about yourself" value="Hi! I use BeaverTinder!" onChange={(e) => setAbout(e.target.value)}/>
                                </div>
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