import './Game.css';

function Game({ model, bg }) {
    return (
        <div style={{background: bg}}>
            <div>
                Name: {model.name}
            </div>
            <div>
                Price: {model.price}
            </div>
            <div>
               <img src={model.coverUrl} />
            </div>
        </div>
    )
}
export default Game;