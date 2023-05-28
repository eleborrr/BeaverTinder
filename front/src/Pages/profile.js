import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import jwt from 'jwt-decode'
import Dropzone from 'react-dropzone';
import { YMaps, Map, Placemark } from '@pbe/react-yandex-maps';
import { GeoMap } from "../Components/geolocation_map";
import './../assets/css/profile.css'
import { axiosInstance } from '../Components/axios_server';
import { height } from '@mui/system';

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

  useEffect(() => {
    // Запрос на сервер для получения текущей информации о пользователе
    // axios.get('/api/user').then((response) => {
    //   const { firstName, lastName, email, password, photo, location } =
    //     response.data;
    //   setFirstName(firstName);
    //   setLastName(lastName);
    //   setEmail(email);
    //   setPhoto(photo);
    //   setLocation(location);
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
  }, []);

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

  return (
    <section className="log-reg">
        <div className="container">
            <div className="row">
                <div className="col-lg-7">
                    <div className="log-reg-inner">
                        <div className="section-header">
                            <h2 className="title">Info about subscription? </h2>
                            <p>Info? </p>
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
                                {modelGender == "Man"?
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

                            
                            {!changing? <div></div> : 
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


                            {/* <div className="form-group">
                                <label htmlFor="photo">Photo</label>
                                <Dropzone disabled = {!changing} onDrop={(acceptedFiles) => setPhoto(acceptedFiles[0])}>
                                {({ getRootProps, getInputProps }) => (
                                    <div {...getRootProps()}>
                                    <input {...getInputProps()} />
                                    <img src={photo}></img>
                                    <p>Drag and drop a file here, or click to select a file</p>
                                    </div>
                                )}
                                </Dropzone>
                            </div> */}

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
                                // <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm">
                                //     <span>Save</span>
                                // </button>
                                <input type="submit" value="Save" style={{width:'21%', height: '3%'}} className='default-btn reverse'></input>
                                :
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm" onClick={() => setChanging(true)}>
                                    <span>Change</span>
                                </button>
                            }
                            
                            </form>
                        </div>
                    </div>
                </div>
                <div className='profile-img' style={{width:'42%'}}>
                    <img src={photo}/>
                </div>
            </div>
        </div>
    </section>
  );
};

export default Profile;