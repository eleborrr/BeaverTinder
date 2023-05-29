import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../Components/axios_server";
import { PaymentForm } from "../Components/payment_form";
import './../assets/css/shops.css';

const ShopsPage = () => {

    const token = Cookies.get('token');
    const [paymentArr, setPaymentArr] = useState(null);
    const [disable, setDisable] = useState(true);
    const [subsId, setSubsId] = useState();
    const [amount, setAmount] = useState();
    const navigate = useNavigate()

    function handleClick(subsId, amount) {
        setDisable(false);
        setSubsId(subsId);
        setAmount(amount);
    }

    function onClose() {
        setDisable(true);
    }

    useEffect(() => {
        if (!token){
            navigate("/login");
        }
        try{
            axiosInstance.get('/subscription/all',
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(res => setPaymentArr(res.data));
        }
        catch(e){
        }
    }, [])
    return(
        <>
            { disable ? 
            <section className="gen-section-padding-3">
            
            <div className="container container-2">
                <div className="row_shop">
                    {paymentArr? paymentArr.map((p) => 
                    <div className="col-xl-4 col-lg-4 col-md-4" key={p.roleId}>
                        <div className="gen-price-block text-center">
                            <div className="gen-price-detail">
                                <span className="gen-price-title"> {p.name} </span>
                                <h2 className="price">{p.pricePerMonth} ₽</h2>
                                <p className="gen-price-duration">/ Per Month</p>
                            </div>
                            <ul className="gen-list-info">
                                <li>
                                    {p.description}
                                </li>
                            </ul>
                            <div className="gen-btn-container button-1">
                                <button onClick={() => handleClick(p.id, p.pricePerMonth)} className="gen-button">
                                    <span className="text">Purchase now</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    ): <p></p>}
                </div>
            </div>
        </section>
         :
            <PaymentForm onClose={onClose} userId={jwtDecode(token).Id[0]} subsId={subsId} amount={amount}/> 
        }
    </>
    )
}

export default ShopsPage;