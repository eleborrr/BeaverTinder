import './../assets/css/animate.css'
import './../assets/css/all.min.css'
import './../assets/css/swiper.min.css'
import './../assets/css/lightcase.css'
import './../assets/css/style.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom'
import logo from './../assets/images/logo/logo.jpg'
import Cookies from 'js-cookie';
import './../assets/css/header.css'

const HeaderApp = () => {
	const token = Cookies.get('token');
	const [homeClass, setHomeClass] = useState('');
	const [chatsClass, setChatsClass] = useState('');
	const [shopsClass, setShopsClass] = useState('');
	const [searchClass, setSearchClass] = useState('');
	const [contactClass, setContactClass] = useState('');
	const [myLikesClass, setMyLikesClass] = useState('');
	const location = useLocation();

	useEffect(
		function() {if (location.pathname === '/home'){
			setHomeClass("active");
		}else if (location.pathname === '/shops'){
			setShopsClass("active");
		}else if (location.pathname === '/contact'){
			setContactClass("active");
		}else if (location.pathname === '/chats'){
			setChatsClass("active");
		}else if (location.pathname === '/search'){
			setSearchClass("active");
		}else if (location.pathname === '/myLikes'){
			setMyLikesClass("active");
		}
	},[location.pathname])

	function RemoveCookies() {
		Cookies.remove('token');
		Cookies.remove('.AspNetCore.Identity.Application');
	}
	
    return(
        <header className="header" id="navbar">
		<div className="header__bottom">
			<div className="container">
				<nav className="navbar navbar-expand-lg">
					<a className="navbar-brand" href="http://localhost:3000/home"><img src={logo} alt="logo" className='logo-img'/></a>
					<button className="navbar-toggler collapsed" type="button" data-bs-toggle="collapse"
						data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false"
						aria-label="Toggle navigation">
						<span className="navbar-toggler--icon"></span>
					</button>
					<div className="collapse navbar-collapse justify-content-end" id="navbarNavAltMarkup">
						<div className="navbar-nav mainmenu">
							<ul>
								<li className={homeClass}>
									<a href="/home">Home</a>
								</li>
								<li className={chatsClass}>
									<a href="/chats">Chats</a>
								</li>
								<li className={shopsClass}>
									<a href="/shops">Shops</a>
								</li>
								<li className={contactClass}>
									<a href="/contact">Contact</a>
								</li>
								{
									token? 
									<>
										<li className={searchClass}>
											<a href="/search">search</a>
										</li>
										<li className={myLikesClass}>
											<a href="/myLikes">My Likes</a>
										</li>
									</>
									:
									<></>
								}
								
							</ul>
						</div>
						<div className="header__more">
                            <button className="default-btn dropdown-toggle" type="button" id="moreoption" data-bs-toggle="dropdown" aria-expanded="false">My Account</button>
                            <ul className="dropdown-menu" aria-labelledby="moreoption">
                              {token? <div>
											<li><a className="dropdown-item" onClick={RemoveCookies} href="/login">Log Out</a></li>
											<li><a className="dropdown-item" href="/profile">Profile</a></li>
									  </div>
							   : <div>
							  		<li><a className="dropdown-item" href="/login">Log In</a></li>
                            		<li><a className="dropdown-item" href="/register">Sign Up</a></li>
								</div>}
                            </ul>
						</div>
					</div>
				</nav>
			</div>
		</div>
    </header>
    )
}

export default HeaderApp;