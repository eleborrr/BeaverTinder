import './../assets/css/animate.css'
import './../assets/css/all.min.css'
import './../assets/css/swiper.min.css'
import './../assets/css/lightcase.css'
import './../assets/css/style.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import logo from './../assets/images/logo/logo.jpg'

const HeaderApp = () => {
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
								<li className="active">
									<a href="#0">Home</a>
									<ul>
										<li><a href="index.html" className="active">Home Page One</a></li>
										<li><a href="index-2.html">Home Page Two</a></li>
										<li><a href="index-3.html">Home Page Three</a></li>
									</ul>
								</li>
								<li>
									<a href="#0">Pages</a>
									<ul>
										<li><a href="about.html">About Us</a></li>
                                        <li><a href="membership.html">Membership</a></li>
                                        <li><a href="comingsoon.html">comingsoon</a></li>
                                        <li><a href="404.html">404</a></li>
									</ul>
								</li>
								<li>
									<a href="#0">Community</a>
									<ul>
										<li><a href="community.html">Community</a></li>
										<li><a href="group.html">All Group</a></li>
										<li><a href="members.html">All Members</a></li>
										<li><a href="activity.html">Activity</a></li>

									</ul>
								</li>
								<li>
									<a href="#0">Shops</a>
									<ul>
										<li><a href="shop.html">Product</a></li>
										<li><a href="shop-single.html">Product Details</a></li>
										<li><a href="shop-cart.html">Product Cart</a></li>
									</ul>
								</li>
								<li>
									<a href="#0">Blogs</a>
									<ul>
										<li><a href="blog.html">Blog</a></li>
										<li><a href="blog-2.html">Blog Style Two</a></li>
										<li><a href="blog-single.html">Blog Details</a></li>
									</ul>
								</li>
								<li><a href="contact.html">contact</a></li>
							</ul>
						</div>
						<div className="header__more">
                            <button className="default-btn dropdown-toggle" type="button" id="moreoption" data-bs-toggle="dropdown" aria-expanded="false">My Account</button>
                            <ul className="dropdown-menu" aria-labelledby="moreoption">
                                <li><a className="dropdown-item" href="login.html">Log In</a></li>
                                <li><a className="dropdown-item" href="register.html">Sign Up</a></li>
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