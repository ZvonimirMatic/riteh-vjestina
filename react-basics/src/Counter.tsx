import { useState } from "react";

function Counter() {
    const [counter, setCounter] = useState(0);

    function minus() {
        setCounter((prevCounter) => {
            return prevCounter - 1;
        });
    }

    function plus() {
        setCounter((prevCounter) => {
            return prevCounter + 1;
        });
    }

    // console.log("RERENDER COUNTER")

    return (
        <div>
            <button onClick={minus}>-</button>
            <span>{counter}</span>
            <button onClick={plus}>+</button>
        </div>
    )
}

export default Counter;
