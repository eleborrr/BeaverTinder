import { useState } from 'react'
import { axiosInstance } from './axios_server'
import './../assets/css/payment_form.css'

export const PaymentForm = ({onClose, userId, subsId, amount}) => {

    const [cardNumber, setCardNumber] = useState();
    const [month, setMonth] = useState();
    const [year, setYear] = useState();
    const [code, setCode] = useState();

    function handleClick() {
        Validate();
    }

    function Validate() {
        if (!/[0-9]{13,16}/.test(cardNumber)){
            console.log('Card is not valid!');
        }
        else{
            console.log('Card is Valid))))');
        }
        console.log(cardNumber);
    }
    return (
    <div className="container p-0">
        <div className="card px-4">
            <p className="h8 py-3">Детали оплаты</p>
            <p onClick={() => onClose()} className="close" />
            <div className="row gx-3">
                <div className="col-12">
                    <div className="d-flex flex-column">
                        <p className="text mb-1">Номер карты</p>
                        <input className="form-control mb-3" type="text" placeholder="1234 5678 435678" onChange={(e) => setCardNumber(e.target.value)} />
                    </div>
                </div>
                <div className="col-6">
                    <p className="text mb-1">Действует до</p>
                    <div className="d-flex">
                        <input className="form-control mb-3" type="number" placeholder="мм" /> 
                        <p className="big-slash">/</p>
                        <input className="form-control mb-3" type="number" placeholder="гггг" />
                    </div>
                </div>
                <div className="col-6">
                    <div className="d-flex flex-column">
                        <p className="text mb-1">CVV/CVC</p>
                        <input className="form-control mb-3 pt-2 " type="password" placeholder="***" />
                    </div>
                </div>
                <div className="col-12">
                    <div className="btn btn-primary mb-3">
                        <button className="ps-3" onClick={handleClick}>Оплатить </button>
                        <span className="fas fa-arrow-right"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    )
}