import React from 'react';
import './../assets/css/subs_card.css';

const SubscriptionCard = ({info, name, price, onClick}) => {
    return (
    <div className="subscription-card">
        <h1>{name}</h1>
        <h2>{price} ₽</h2>
        <p className="gen-price-duration">/ Per Month</p>
        <h2>Subscription info</h2>
        <p>{info}</p>
        <button onClick={onClick} className="gen-button">
            Purchase now
        </button>
    </div>
    );
};

export default SubscriptionCard;
