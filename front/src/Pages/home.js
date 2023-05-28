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
    <div class="about padding-top padding-bottom main-container">
		<div class="container">
			<div class="section__header style-2 text-center wow fadeInUp" data-wow-duration="1.5s">
				<h2>Beaver Tinder Super Powers</h2>
				<p>Our dating platform is like a breath of fresh air. Clean and trendy design with ready to use features we are sure you will love.</p>
			</div>
			<div class="section__wrapper">
				<div class="row g-4 justify-content-center row-cols-xl-4 row-cols-lg-3 row-cols-sm-2 row-cols-1">
					<div class="col wow fadeInUp" data-wow-duration="1.5s">
						<div class="about__item text-center">
							<div class="about__inner">
								<div class="about__thumb">
									<img src={firstPicture} alt="dating thumb" />
								</div>
								<div class="about__content">
									<h4>Simple To Use</h4>
									<p>Nothing useless.</p>
								</div>
							</div>
						</div>
					</div>
					<div class="col wow fadeInUp" data-wow-duration="1.5s">
						<div class="about__item text-center">
							<div class="about__inner">
								<div class="about__thumb">
									<img src={secondPicture} alt="dating thumb" />
								</div>
								<div class="about__content">
									<h4>Without VPN</h4>
									<p>Use our site and don't mind about VPN ;).</p>
								</div>
							</div>
						</div>
					</div>
					<div class="col wow fadeInUp" data-wow-duration="1.5s">
						<div class="about__item text-center">
							<div class="about__inner">
								<div class="about__thumb">
									<img src={thirdPicture} alt="dating thumb" />
								</div>
								<div class="about__content">
									<h4>Very Fast</h4>
									<p>Don’t waste your time! Begin communicate right now!</p>
								</div>
							</div>
						</div>
					</div>
					<div class="col wow fadeInUp" data-wow-duration="1.5.5s">
						<div class="about__item text-center">
							<div class="about__inner">
								<div class="about__thumb">
									<img src={fourthPicture} alt="dating thumb" />
								</div>
								<div class="about__content">
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