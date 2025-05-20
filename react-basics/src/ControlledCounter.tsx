interface IControlledCounterProps {
    counter: number;
    minus: () => void;
    plus: () => void;
}

function ControlledCounter({counter, minus, plus}: IControlledCounterProps) {
    // console.log("RERENDER CONTROLLED COUNTER")
    
    return (
        <div>
            <button onClick={minus}>-</button>
            <span>{counter}</span>
            <button onClick={plus}>+</button>
        </div>
    )
}

export default ControlledCounter;