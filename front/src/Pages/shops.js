import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../Components/axios_server";
import { PaymentForm } from "../Components/payment_form";
import './../assets/css/shops.css';
import SubscriptionCard from "../Components/subscription_card";

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
            return;
        }
        try{
            axiosInstance.get('/subscription/all',
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(res => {
                console.log(res.data)
                setPaymentArr(res.data)});
            console.log(paymentArr);

        }
        catch(e){
            alert("Network error, try again later");
        }
    }, [navigate, token])
    return(
        <>
            { disable ? 
            <section className="gen-section-padding-3">
            
            <div className="container container-2">
                <div className="row_shop">
                    {paymentArr? paymentArr.map((p) => 
                    <div className="col-xl-4 col-lg-4 col-md-4" key={p.roleId}>
                        <div className="gen-price-block text-center">
                            <SubscriptionCard 
                                info={p.description} 
                                name={p.name} 
                                price={p.pricePerMonth}
                                onClick={() => handleClick(p.id, p.pricePerMonth)}/>
                        </div>
                    </div>
                    ): <p></p>}
                </div>
            </div>
        </section>
         :
            <PaymentForm onClose={onClose} userId={jwtDecode(token).Id} subsId={subsId} amount={amount}/> 
        }
    </>
    )
}

export default ShopsPage;