import React from 'react';
import './../assets/css/subs_card_profile.css';

const SubscriptionCardProfile = ({info, name, onClick}) => {
    return (
    <div className="subscription-card-profile">
        <h4>Subscription info</h4>
        <h5>{name}</h5>
        
        <p>{info}</p>
        <button onClick={onClick} className="gen-button">
            Extend now
        </button>
    </div>
    );
};

export default SubscriptionCardProfile;
