import React, { useEffect, useState } from 'react';
import Game from './gameComponent/game';
import { gamesApi } from '../services/gamesApi'

function Games() {
    const [games, setGames] = useState([]);

    const [count, setCounter] = useState(3);
  
    const { GetReactGames } = gamesApi;
  
    useEffect(function () {
      GetReactGames(1, count)
        .then(response => {
            console.log(response.data)
            setGames(response.data)
        });
    }, [count])
  
    const addOne = () => {
      setCounter((oldValue) => oldValue + 1);
    }
  
    const minusOne = () => {
      setCounter((oldValue) => oldValue - 1);
    }
  
    return (
        <>
            <div>
                Counter: {count}
                <input type='button' value='-' onClick={minusOne} />
                <input type='button' value='+' onClick={addOne} />
            </div>
            <div>
                Games:
            </div>
            <div className='games-block'>
                {
                    games.map(game => {
                        return (<Game model={game}></Game>)
                    })
                }
            </div>
        </>
    )
}
export default Games;