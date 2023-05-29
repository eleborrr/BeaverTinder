import { YMaps, Map, Placemark } from '@pbe/react-yandex-maps';
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import Cookies from "js-cookie";
import { axiosInstance } from "../Components/axios_server";
import './../assets/css/register.css'

const RegisterPage = () => {
    const [lName, setLName] = useState('Last name');
    const [fName, setFName] = useState('First name');
    const [nName, setNName] = useState('Nickname');
    const [email, setEmail] = useState('someEmail@gmail.com');
    const [pass, setPass] = useState('');
    const [confPass, setConfPass] = useState('');
    const [gender, setGender] = useState('');
    const [about, setAbout] = useState('Hi! I use BeaverTinder!');
    const [birthdate, setBirthdate] = useState('');
    const [location, setLocation] = useState('');
    const [passError, setPassError] = useState('');
    const [birthError, setBirthError] = useState('');
    const [locationError, setLocationError] = useState('');
    const [fNameError, setFNameError] = useState('');
    const [lNameError, setLNameError] = useState('');
    const [nNameError, setNNameError] = useState('');
    const [genderError, setGenderError] = useState('');
    const [emailError, setEmailError] = useState('');
    const [respStatus, setRespStatus] = useState(false);
    const [errorCode, setErrorCode] = useState('');
    const [respErrData, setRespErrData] = useState('');
    const [long, setLong] = useState();
    const [lant, setLant] = useState();
    const navigate = useNavigate()

    const ValidatePass = (e) => {
        setConfPass(e.target.value)
        if (e.target.value !== pass){
            setPassError("Пароли не совпадают!")
        } else {
            setPassError('')
            setConfPass(e.target.value)
        }
    }  

    const handleFNameChange = (event) => {
        setFNameError('');
        setFName(event.target.value);
      };    

    const handleLNameChange = (event) => {
        setLNameError('');
        setLName(event.target.value);
      };    

    const handleNNameChange = (event) => {
        setNNameError('');
        setNName(event.target.value);
      };    

    const handleEmailChange = (event) => {
        const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        const newEmail = event.target.value;
        if(!emailRegex.test(newEmail)){
            setEmailError('Введен неверный формат почты');
        }
        else{
            setEmailError('');
        }
        setEmail(newEmail);
      }; 
      
    const handleGenderChange = (gender) => {
        setGenderError('');
        setGender(gender);
      };    

    function handleMapClick(event) {
        const coords = event.get('coords');
        setLant(coords[0]);
        setLong(coords[1]);
        setLocationError('');
        setLocation(coords.join(', '));
      };

    const handleBirthdateChange = (event) => {
        const inputDate = event.target.value;
        const birthYear = new Date(inputDate).getFullYear();
        const currentYear = new Date().getFullYear();
        const calculatedAge = currentYear - birthYear;
        if (calculatedAge >= 18 && calculatedAge <=150){
            setBirthdate(inputDate);
            setBirthError('');
        }
        else{
            setBirthdate(inputDate);
            setBirthError("Регистрация разрешена только с 18 лет. Возраст не более 150 лет");
        }
      };

    const onSubmit = (e) => {
        e.preventDefault();
        setRespErrData('');
        if (pass !== confPass){
            setPassError('Пароли не совпадают!');
            return;
        }
        if(birthError !== ''){
            setBirthError('День рождения должен быть указан');
            return;
        }
        if(location === ''){
            setLocationError('Локация обязательно должна быть указана');
            return;
        }
        if(fName === ''){
            setFNameError('Поле First Name обязательно должно быть указано');
            return;
        }
        if(lName === ''){
            setLNameError('Поле Last Name обязательно должно быть указано');
            return;
        }
        if(nName === ''){
            setNNameError('Поле Nickname обязательно должно быть указано');
            return;
        }
        if(pass === ''){
            setPassError('Пароль обязательно должен быть указан');
            return;
        }
        if(gender === ''){
            setGenderError('Пол обязательно должен быть указан');
            return;
        }
        if(email === ''){
            setEmailError('Почта обязательно должна быть указана');
            return;
        }
        if(passError !== '' || nNameError !== '' || fNameError !== '' || lNameError !== '' 
        || birthError !== '' || locationError !== '' || genderError !== '' || emailError !== ''){
            alert('Допущена одна и более ошибок при заполнении формы');
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
                About: about,
                Longitude: long,
                Latitude: lant,
                DateOfBirth: birthdate
            })
            .then(function (res) {
                const data = res.data;
                if(data.successful === true){
                    setRespStatus(true);
                }
                else{
                    setRespErrData(data.message);
                }
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
        }

    };

    useEffect(() => {
        if (Cookies.get('token')){
            navigate('/home');
        }
    }, []);
    
    

    return (
        <>

    <a href="#" className="scrollToTop"><i className="fa-solid fa-angle-up"></i></a>

    <section className="log-reg">
        <div className="top-menu-area">
            <div className="container">
                <div className="row">
                    <div className="col-lg-4 col-5">
                        <a href="/home" className="backto-home"><i className="fas fa-chevron-left"></i> Back to Home</a>
                    </div>
                </div>
            </div>
        </div>
        <div className="container">
            <div className="row">
                <div className="image image-log-reg">
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
                                    <input type="text" className="my-form-control" placeholder="Enter Your Last name" value={lName} onChange={(e) => handleLNameChange(e)}/>
                                    <span>{lNameError}</span>
                                </div>
                                <div className="form-group">
                                    <label>First name</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your First name" value={fName} onChange={(e) => handleFNameChange(e)}/>
                                    <span>{fNameError}</span>
                                </div>
                                <div className="form-group">
                                    <label>Nickname</label>
                                    <input type="text" className="my-form-control" placeholder="Enter Your Nickname" value={nName} onChange={(e) => handleNNameChange(e)}/>
                                    <span>{nNameError}</span>
                                </div>
                                <div className="form-group">
                                    <label>Email</label>
                                    <input type="email" className="my-form-control" placeholder="Enter Your Email" value={email} onChange={(e) => handleEmailChange(e)}/>
                                    <span>{emailError}</span>
                                </div>
                                <div className='form-group'>
                                    <label>Birth date:</label>
                                    <input type="date" className="my-form-control" value={birthdate} onChange={(e) => handleBirthdateChange(e)} />
                                    <span>{birthError}</span>
                                </div>
                                <div className="form-group">
                                    <label>Password</label>
                                    <input type="password" className="my-form-control" placeholder="Enter Your Password" onChange={(e) => {
                                        setPass(e.target.value);
                                       
                                        if (e.target.value !== confPass){
                                            setPassError('Пароли не совпадают!');
                                        } else {
                                            setPassError('');
                                        }
                                     }}/>
                                </div>
                                <div className="form-group">
                                    <label>ConfirmPassword</label>
                                    <input type="password" className="my-form-control" placeholder="Confirm Your Password" onChange={(e) => ValidatePass(e)}/>
                                    <span>{passError}</span>
                                </div>
                                <h4 className="content-title mt-5">Profile Details</h4>
                                <div className="form-group">
                                    <label>Gender</label>
                                    <div className="banner__inputlist">
                                        <div className="s-input me-3">
                                            <input type="radio" name="gender1" id="males1" onClick={() => handleGenderChange('Man')} />
                                            <label htmlFor="males1">Man</label>
                                        </div>
                                        <div className="s-input me-3">
                                            <input type="radio" name="gender1" id="females1" onClick={() => handleGenderChange('Woman')}/>
                                            <label htmlFor="females1">Woman</label>
                                        </div>
                                        <span>{genderError}</span>
                                    </div>
                                </div>
                                <div className="form-group">
                                    <label>Tell about yourself</label>
                                    <input type="text" className="my-form-control" placeholder="Tell about yourself" value={about} onChange={(e) => setAbout(e.target.value)}/>
                                </div>
                                
                                <div className='form-group'>
                                    <label>Geolocation:</label>
                                    <input type="text" className="my-form-control" value={location} disabled={true}/>
                                    <span>{locationError}</span>
                                    <YMaps>
                                        <div>
                                            <Map onClick={handleMapClick} width="100%" height="400px" defaultState={{ center: [55.76, 37.64], zoom: 10 }}>
                                                {location? <Placemark geometry={location.split(", ")} />: <></>}
                                            </Map>
                                        </div>
                                    </YMaps>
                                </div>
                                <span className=''>{respErrData}</span><br />
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