import Cookies from 'js-cookie'
import { useState } from 'react'
import { axiosInstance } from './axios_server'
import './../assets/css/payment_form.css'

export const PaymentForm = ({onClose, userId, subsId, amount}) => {

    const token = Cookies.get('token');
    const [cardNumber, setCardNumber] = useState();
    const [month, setMonth] = useState();
    const [year, setYear] = useState();
    const [code, setCode] = useState();
    const [err, setErr] = useState();

    function handleClick() {
        if(!Validate()){
            if (err){
                alert('Форма заполнена неверно - ' + err);
            } else{
                alert('Форма заполнена неверно, попробуйте ещё раз');
            }
            
        } else {
            axiosInstance.post('/payment/pay',{ 
                userId: userId,
                cardNumber: cardNumber,
                month: month,
                amount: amount,
                year: year,
                code: code,
                subsId: subsId
            }, {
                headers:{
                    Authorization: `Bearer ${token}`,
                    Accept : "application/json"
                }
            })
            .then(res => {
                alert("Для получения дополнительных свойств подписки просим перезайти на аккаунт")
            })
        }
    }

    function Validate() {
        if (!/[0-9]{13,16}/.test(cardNumber)){
            setErr('неверный номер карты: пишите без пробелов, номер карты от 13 до 16 символов');
            return false;
        }
        if (!/[0-9]{3}/.test(code)){
            setErr('неверный код карты: код карты с обратной стороны, 3 символа');
            return false;
        }
        if (month < 1 || month > 12){
            setErr('неверный указан месяц: от 1 до 12');
            return false;
        }
        const endDate = new Date(`${year}-${month}-01`);
        const now = new Date(); 
        if (endDate <= now || now.getFullYear() - year > 5){
            setErr('срок действия карты истёк');
            return false;
        }
        
        return true;
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
                        <input className="form-control mb-3" type="number" placeholder="мм" onChange={(e) => setMonth(e.target.value)}/> 
                        <p className="big-slash">/</p>
                        <input className="form-control mb-3" type="number" placeholder="гггг" onChange={(e) => setYear(e.target.value)}/>
                    </div>
                </div>
                <div className="col-6">
                    <div className="d-flex flex-column">
                        <p className="text mb-1">CVV/CVC</p>
                        <input className="form-control mb-3 pt-2 " type="password" placeholder="***" onChange={(e) => setCode(e.target.value)} />
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