import React from 'react';
import './../assets/css/subs_card_profile.css';

const SubscriptionCardProfile = ({info, name, onClick}) => {
    return (
    <div className="subscription-card">
        <h1>{name}</h1>
        <h2>Subscription info</h2>
        <p>{info}</p>
        <button onClick={onClick} className="gen-button">
            Extend now
        </button>
    </div>
    );
};

export default SubscriptionCardProfile;
