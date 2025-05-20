import { useCallback, useMemo, useRef, useState } from "react";
import { useAppContext } from "./App";
import useWindowSize from "./useWindowSize";

function Advanced() {
    const [count, setCount] = useState(0);
    const [otherState, setOtherState] = useState(false);
    const appContext = useAppContext();
    const windowSize = useWindowSize();
    const inputRef = useRef<HTMLInputElement>(null);

    const expensiveCalculation = (num: number) => {
        let result = 0;
        for (let i = 0; i < 1000000000; i++) {
            result += num;
        }
        return result;
    };

    const calculatedValue = useMemo(() => {
        return expensiveCalculation(count)
    }, [count]);

    const increment = useCallback(() => {
        setCount(count + 1)
    }, [count]);

    return (
        <div>
            <p>Count: {count}</p>
            <p>Expensive Result: {calculatedValue}</p>
            <button onClick={increment}>Increment Count</button>
            <button onClick={() => setOtherState(!otherState)}>Toggle Other State</button>
            <p>Root counter: {appContext.rootCounter}</p>
            <p>Window width: {windowSize.width}</p>
            <div>
                <input ref={inputRef} />
                <button onClick={() => {
                    inputRef.current?.focus();
                }}>Set focus</button>
            </div>
        </div>
    )
}

export default Advanced;