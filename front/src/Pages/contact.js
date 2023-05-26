import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import '../assets/css/contacts.css'
import placemark from '../assets/images/contact/01.png'
import phone from '../assets/images/contact/02.png'
import mail from '../assets/images/contact/03.png'

const ContactPage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    return(
    <div className="info-section padding-top padding-bottom contact-div">
        <div className="container">
			<div className="section__header style-2 text-center">
				<h2>Contact Info</h2>
				<p>Let us know your opinions. Also you can write us if you have any questions.</p>
			</div>
            <div className="section-wrapper">
                <div className="row justify-content-center g-4">
                    <div className="col-lg-4 col-sm-6 col-12">
                        <div className="contact-item text-center">
                            <div className="contact-thumb mb-4">
                                <img src={placemark} alt="contact-thumb" />
                            </div>
                            <div className="contact-content">
                                <h6 className="title">Office Address</h6>
                                <p>University Village, 18</p>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-4 col-sm-6 col-12">
                        <div className="contact-item text-center">
                            <div className="contact-thumb mb-4">
                                <img src={phone} alt="contact-thumb" />
                            </div>
                            <div className="contact-content">
                                <h6 className="title">Phone number</h6>
                                <p>+7 800 555 35 35</p>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-4 col-sm-6 col-12">
                        <div className="contact-item text-center">
                            <div className="contact-thumb mb-4">
                                <img src={mail} alt="contact-thumb" />
                            </div>
                            <div className="contact-content">
                                <h6 className="title">Send Email</h6>
                                <p>beavertinder@gmail.com</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    )
}

export default ContactPage;