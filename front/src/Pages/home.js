import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";
import '../assets/css/home.css'
import firstPicture from '../assets/images/about/01.jpg'
import secondPicture from '../assets/images/about/02.jpg'
import thirdPicture from '../assets/images/about/03.jpg'
import fourthPicture from '../assets/images/about/04.jpg'

const HomePage = () => {
    const navigate = useNavigate();
    const token = Cookies.get('token');

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
    }, [])

    return(
    <div className="about padding-top padding-bottom main-container">
		<div className="container">
			<div className="section__header style-2 text-center wow fadeInUp" data-wow-duration="1.5s">
				<h2>Beaver Tinder Super Powers</h2>
				<p>Our dating platform is like a breath of fresh air. Clean and trendy design with ready to use features we are sure you will love.</p>
			</div>
			<div className="section__wrapper">
				<div className="row g-4 justify-content-center row-cols-xl-4 row-cols-lg-3 row-cols-sm-2 row-cols-1">
					<div className="col wow fadeInUp" data-wow-duration="1.5s">
						<div className="about__item text-center">
							<div className="about__inner">
								<div className="about__thumb">
									<img src={firstPicture} alt="dating thumb" />
								</div>
								<div className="about__content">
									<h4>Simple To Use</h4>
									<p>Nothing useless.</p>
								</div>
							</div>
						</div>
					</div>
					<div className="col wow fadeInUp" data-wow-duration="1.5s">
						<div className="about__item text-center">
							<div className="about__inner">
								<div className="about__thumb">
									<img src={secondPicture} alt="dating thumb" />
								</div>
								<div className="about__content">
									<h4>Without VPN</h4>
									<p>Use our site and don't mind about VPN ;).</p>
								</div>
							</div>
						</div>
					</div>
					<div className="col wow fadeInUp" data-wow-duration="1.5s">
						<div className="about__item text-center">
							<div className="about__inner">
								<div className="about__thumb">
									<img src={thirdPicture} alt="dating thumb" />
								</div>
								<div className="about__content">
									<h4>Very Fast</h4>
									<p>Don’t waste your time! Begin communicate right now!</p>
								</div>
							</div>
						</div>
					</div>
					<div className="col wow fadeInUp" data-wow-duration="1.5.5s">
						<div className="about__item text-center">
							<div className="about__inner">
								<div className="about__thumb">
									<img src={fourthPicture} alt="dating thumb" />
								</div>
								<div className="about__content">
									<h4>Find your love</h4>
									<p>There is nothing irreplaceable!</p>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
    )
}

export default HomePage;