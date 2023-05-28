import React, { useState, useEffect } from 'react';
import Dropzone from 'react-dropzone';
import { YMaps, Map, Placemark } from '@pbe/react-yandex-maps';
import { axiosInstance } from "../Components/axios_server";
import './../assets/css/profile.css'

const Profile = () => {
  const [changing, setChanging] = useState(false);
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [photo, setPhoto] = useState(null);
  const [location, setLocation] = useState('');
  const [latitude, setLatitude] = useState(null);
  const [longitude, setLongitude] = useState(null);

  useEffect(() => {
    // Запрос на сервер для получения текущей информации о пользователе
    axiosInstance.get('/api/user').then((response) => {
       const { firstName, lastName, email, password, photo, location } =
         response.data;
       setFirstName(firstName);
       setLastName(lastName);
       setEmail(email);
       setPhoto(photo);
       setLocation(location);
     });
  }, []);

  function handleMapClick(event) {
    const coords = event.get('coords');
    setLatitude(coords[0]);
    setLongitude(coords[1]);
    setLocation(coords.join(', '));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Отправка данных на сервер
    const formData = new FormData();
    formData.append('firstName', firstName);
    formData.append('lastName', lastName);
    formData.append('email', email);
    if (photo) {
       formData.append('photo', photo);
     }
    formData.append('location', location);
    formData.append('latitude', latitude);
    formData.append('longitude', longitude);
    axiosInstance.post('/api/user', formData).then((response) => {
        console.log(response.data);
    });
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
                                type="text"
                                name="lastName"
                                value={lastName}
                                onChange={(e) => setLastName(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <input
                                type="email"
                                name="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                className="my-form-control"
                                />
                            </div>

                            <div className="form-group">
                                <label htmlFor="photo">Photo</label>
                                <Dropzone onDrop={(acceptedFiles) => setPhoto(acceptedFiles[0])}>
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
                                <button className="default-btn reverse" data-toggle="modal" data-target="#email-confirm">
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