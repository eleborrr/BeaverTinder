import React, { useState, useEffect } from 'react';
import Cookies from 'js-cookie';
import jwt from 'jwt-decode'
import Dropzone from 'react-dropzone';
import { YMaps, Map, Placemark } from '@pbe/react-yandex-maps';
import './../assets/css/profile.css'
import { axiosInstance } from '../Components/axios_server';

const Profile = () => {
const token = Cookies.get('token');
  const [changing, setChanging] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [username, setUsername] = useState('');
  const [photo, setPhoto] = useState(null);
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
    console.log(decodedToken);
    const userId = decodedToken.Id;
    console.log(userId);
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
            setUsername(res.data.userName)
            console.log(res.data);
        })
  }, []);

  function handleMapClick(event) {
    const coords = event.get('coords');
    setLatitude(coords[0]);
    setLongitude(coords[1]);
    setLocation(coords.join(', '));
  };

  function Save() {
    axiosInstance.post('/save', {
        FirstName: firstName,
        Lastname: lastName,
        UserName: username,
        Photo: photo,
        Longitude: longitude,
        Latitude: latitude,
    }, {
        headers:{
            Authorization: `Bearer ${token}`,
            Accept : "application/json"
        }
    })
}

  const handleSubmit = (e) => {
    e.preventDefault();
    // Отправка данных на сервер
    // const formData = new FormData();
    // formData.append('firstName', firstName);
    // formData.append('lastName', lastName);
    // formData.append('email', email);
    // if (photo) {
    //   formData.append('photo', photo);
    // }
    // formData.append('location', location);
    // formData.append('latitude', latitude);
    // formData.append('longitude', longitude);
    // axios.post('/api/user', formData).then((response) => {
    //   console.log(response.data);
    // });
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
                                <label htmlFor="email">UserName</label>
                                <input
                                disabled = {!changing}
                                type="text"
                                name="username"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="photo">Photo</label>
                                <Dropzone disabled = {!changing} onDrop={(acceptedFiles) => setPhoto(acceptedFiles[0])}>
                                {({ getRootProps, getInputProps }) => (
                                    <div {...getRootProps()}>
                                    <input {...getInputProps()} />
                                    <p>Drag and drop a file here, or click to select a file</p>
                                    </div>
                                )}
                                </Dropzone>
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
                                        <></>
                                    }
                                    
                            </div>
                            {
                                changing?
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm" onClick={Save}>
                                    <span>Save</span>
                                </button>
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
                    <img href=''/>
                </div>
            </div>
        </div>
    </section>
  );
};

export default Profile;