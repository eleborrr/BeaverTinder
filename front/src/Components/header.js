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

const HeaderApp = () => {
	const [token, setToken] = useState(Cookies.get('token'));
	const [homeClass, setHomeClass] = useState('')
	const [pagesClass, setPagesClass] = useState('')
	const [communityClass, setCommunityClass] = useState('')
	const [shopsClass, setShopsClass] = useState('')
	const [blogsClass, setBlogsClass] = useState('')
	const [contactClass, setContactClass] = useState('')
	const location = useLocation();

	useEffect(
		function() {if (location.pathname === '/home'){
			setHomeClass("active");
		}else if (location.pathname === '/pages'){
			setPagesClass("active");
		}else if (location.pathname === '/community'){
			setCommunityClass("active");
		}else if (location.pathname === '/shops'){
			setShopsClass("active");
		}else if (location.pathname === '/blogs'){
			setBlogsClass("active");
		}else if (location.pathname === '/contact'){
			setContactClass("active");
		}
	}
	)

	function RemoveCookies() {
		Cookies.remove('token');
		Cookies.remove('.AspNetCore.Identity.Application');
	}
	
    return(
        <header className="header" id="navbar">
		<div className="header__bottom">
			<div className="container">
				<nav className="navbar navbar-expand-lg">
					<a className="navbar-brand" href="http://localhost:3000/main"><img src={logo} alt="logo" /></a>
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
								<li className={pagesClass}>
									<a href="/pages">Pages</a>
								</li>
								<li className={communityClass}>
									<a href="/community">Community</a>
									<ul>
										<li><a href="community.html">Community</a></li>
										<li><a href="group.html">All Group</a></li>
										<li><a href="members.html">All Members</a></li>
										<li><a href="activity.html">Activity</a></li>
									</ul>
								</li>
								<li className={shopsClass}>
									<a href="/shops">Shops</a>
								</li>
								<li className={blogsClass}>
									<a href="/blogs">Blogs</a>
								</li>
								<li className={contactClass}>
									<a href="/contact">contact</a>
								</li>
							</ul>
						</div>
						<div className="header__more">
                            <button className="default-btn dropdown-toggle" type="button" id="moreoption" data-bs-toggle="dropdown" aria-expanded="false">My Account</button>
                            <ul className="dropdown-menu" aria-labelledby="moreoption">
                              {token? <div>
											<li><a className="dropdown-item" href="/like">Like</a></li>
											<li><a className="dropdown-item" onClick={RemoveCookies} href="/login">Log Out</a></li>
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