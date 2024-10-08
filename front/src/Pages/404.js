import './../assets/css/bootstrap.min.css';
import './../assets/css/animate.css';
import './../assets/css/all.min.css';
import './../assets/css/swiper.min.css';
import './../assets/css/lightcase.css';
import './../assets/css/style.css';
import './../assets/css/404.css'
import E404 from './../assets/images/404.png'

const PageNotFound = () => {
    return(
    <>

<div>
    
    <a href="/" className="scrollToTop"><i className="fa-solid fa-angle-up"></i></a>


    <section className="log-reg forezero">
        <div className="container">
            <div className="row justify-content-end">
                <div className="image-404-my image"></div>
                <div className="col-lg-7 ">
                    <div className="log-reg-inner">
                        <div className="main-thumb mb-5">
                            <img src={E404} alt="datting thumb" />
                        </div>
                        <div className="main-content inloginp">
                            <div className="text-content text-center">
                                <h2>Ops! This Page Not Found</h2>
                                <a href="/home" className="default-btn reverse"><span>Back to Home</span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>
</>
    )
}

export default PageNotFound;