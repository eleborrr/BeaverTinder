import React, { useState, useMemo, useRef } from 'react'
import "../assets/css/beaverCard.css"

const BeaverCard = ({profile, like, dislike, distance}) => {

  console.log(distance);
  const [alreadyLiked, setAlreadyLiked] = useState(false);
  const CorrectWord = (age) => {
    if (age % 10 === 1){
      return 'год';
    }else if(age % 10 === 2 || age % 10 === 3 || age % 10 === 4){
      return 'года';
    }else if(age % 10 === 5 || age % 10 === 6 || age % 10 === 7 || age % 10 === 8 || age % 10 === 9 || age % 10 === 0){
      return 'лет';
    }
  }

return (
  <div className='content'>
    <div className='beaverCard'>
          <img className='beaverCardImg' src={profile.image}></img>
          <div className='profile'>
            <div className='name'>{`${profile.firstName} ${profile.lastName},`} <span>{profile.age} {CorrectWord(profile.age)}</span></div>
            <div className='name from'> {distance}km from you</div>
            <div className='name'>Пол: {profile.gender === 'Woman' ? 'жен' : 'муж'}</div>
            <div className='name'>О себе: {profile.about}</div>
          </div>
          <div className='beaverCardButtons'>
            <button className='dislike' onClick={() => {setAlreadyLiked(true); dislike(); setAlreadyLiked(false)}} disabled = {alreadyLiked}> 
            <i className='fas fa-times'></i>
            </button>
            <button className='like' onClick={() => {setAlreadyLiked(true); like(); setAlreadyLiked(false) }} disabled = {alreadyLiked}>
              <i className='fas fa-heart'> </i>
            </button>
          </div>
    </div>
  </div>
  
)
}

export default BeaverCard;
