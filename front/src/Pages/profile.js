import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import jwt from 'jwt-decode'
import { YMaps, Map, Placemark } from '@pbe/react-yandex-maps';
import { GeoMap } from "../Components/geolocation_map";
import './../assets/css/profile.css'
import { axiosInstance } from '../Components/axios_server';
import SubscriptionCardProfile from '../Components/subscription_card_profile';
import { useNavigate } from 'react-router-dom';

const Profile = () => {
const token = Cookies.get('token');
  const [changing, setChanging] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [username, setUsername] = useState('');
  const [about, setAbout] = useState('');
  const [modelGender, setModelGender] = useState('');
  const [gender, setGender] = useState('');
  const [password, setPassword] = useState('');
  const [confPass, setConfPass] = useState('');
  const [photo, setPhoto] = useState('');
  const [location, setLocation] = useState('');
  const [latitude, setLatitude] = useState(null);
  const [longitude, setLongitude] = useState(null);
  const [subName, setSubName] = useState('');
  const [subExpires, setSubExpires] = useState('');
  const [passChange, setPassChange] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const decodedToken = jwt(token);
    const userId = decodedToken.Id;
    axiosInstance.get('/userinfo?id='+userId,
        {
            headers: {
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {
            console.log(res.data);
            setSubName(res.data.subName);
            setSubExpires(res.data.subExpiresDateTime);
            setFirstName(res.data.firstName);
            setLastName(res.data.lastName);
            setUsername(res.data.userName);
            setLongitude(res.data.longitude);
            setLatitude(res.data.latitude);
            setPhoto(res.data.image);
            setAbout(res.data.about);
            setGender(res.data.gender);
            setModelGender(res.data.gender);
        })
  }, [token]);

  function handleMapClick(event) {
    const coords = event.get('coords');
    setLatitude(coords[0]);
    setLongitude(coords[1]);
    setLocation(coords.join(', '));
  };

  const handleSubmit = (e) => {
    axiosInstance.post('/edit', {
        FirstName: firstName,
        LastName: lastName,
        UserName: username,
        Gender: gender,
        About: about,
        Image: photo,
        Password: password,
        ConfirmPassword: confPass,
        Longitude: longitude,
        Latitude: latitude,
        SubName: 'subName',
        subExpiresDateTime: '2023-06-28T13:44:00.7989673',
    }, 
    {
        headers:{
            Authorization: `Bearer ${token}`,
            Accept : "application/json"
        },
    })
    .then(res => setChanging(false))
    
    e.preventDefault();
  };

  const expirationDate = `\t${subExpires.substring(8,10)}/${subExpires.substring(5,7)}/${subExpires.substring(0,4)}`

  return (
    <section className="log-reg">
        <div className="container">
            <div className="row">
                <div className="col-lg-7">
                    <div className="log-reg-inner">
                        <div className="section-header">
                            <SubscriptionCardProfile 
                                name={subName}
                                info={`Expires at ${expirationDate}`}
                                onClick={() => navigate("/shops")}
                            />
                        </div>
                        <div className="main-content">
                            
                            <form onSubmit={handleSubmit}>
                            <div className="form-group">
                                <label htmlFor="firstName">First Name</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="firstName"
                                value={firstName}
                                onChange={(e) => setFirstName(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="lastName">Last Name</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="lastName"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="userName">UserName</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="userName"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                className="my-form-control"
                                />
                            </div>
                            {
                                !changing? 
                                <div className="form-group">
                                    <label htmlFor="gender">Gender</label>
                                    <input
                                    disabled = {!changing}
                                    type="text"
                                    name="gender"
                                    value={modelGender}
                                    onChange={(e) => setGender(e.target.value)}
                                    className="my-form-control"
                                />
                            </div> : 
                            
                            <div>
                                {modelGender === "Man"?
                                 <div className='form-group'>
                                    <select name= "gender" onChange={(e) => setGender(e.target.value)}> 
                                         <option value="Man">Man</option> 
                                         <option value="Woman">Woman</option>
                                     </select>
                                </div>
                            :
                                <div className='form-group'>
                                    <select name= "gender" onChange={(e) => setGender(e.target.value)}> 
                                         <option value="Woman">Woman</option> 
                                        <option value="Man">Man</option>
                                    </select>
                                </div>}
                            </div>
                            
                            }
                            
                            <div className="form-group">
                                <label htmlFor="about">About</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="about"
                                value={about}
                                onChange={(e) => setAbout(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            
                            {!passChange? <div></div> : 
                            <div>

                                <div className="form-group">
                                <label htmlFor="password">Password</label>
                                <input
                                disabled = {!changing}
                                type="password"
                                name="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                className="my-form-control"
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="confPass">Confirm Password</label>
                                <input
                                disabled = {!changing}
                                type="password"
                                name="confPass"
                                value={confPass}
                                onChange={(e) => setConfPass(e.target.value)}
                                className="my-form-control"
                                />
                            </div>
                            </div>
                            }
                            

                            <div className="form-group">
                                <label htmlFor="image">Image</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="image"
                                value={photo}
                                onChange={(e) => setPhoto(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="location">Location</label>
                                <input type="text" className="my-form-control" value={location} disabled={true}/>
                                    {
                                        changing?
                                        <YMaps>
                                            <div>
                                                <Map onClick={handleMapClick} width="100%" height="400px" defaultState={{ center: [55.76, 37.64], zoom: 10 }}>
                                                    {location? <Placemark geometry={location.split(", ")} />: <></>}
                                                </Map>
                                            </div>
                                        </YMaps>
                                        :
                                        <div>
                                            <GeoMap latitude={latitude ? latitude : 55.81441} longitude={longitude ? longitude : 49.12068} />
                                        </div>
                                    }
                                    
                            </div>
                            {
                                changing?
                                <input type="submit" value="Save" style={{width:'21%', height: '3%'}} className='default-btn reverse'></input>
                                :
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm" onClick={() => setChanging(true)}>
                                    <span>Change</span>
                                </button>
                                
                            }
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm" onClick={(e) => setPassChange(!passChange)}>
                                    <span>ChangePassoword</span>
                                </button>
                            
                            </form>
                        </div>
                    </div>
                </div>
                <div className='profile-img' style={{width:'40%'}}>
                    <img src={photo} alt='user'/>
                </div>
            </div>
        </div>
    </section>
  );
};

export default Profile;