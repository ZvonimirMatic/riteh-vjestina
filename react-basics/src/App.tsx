import React, { createContext, useContext, useEffect, useState } from 'react';
import Counter from './Counter';
import ControlledCounter from './ControlledCounter';
import Advanced from './Advanced';
import Pokemon from './Pokemon';
import PokemonTanstack from './PokemonTanstack';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const queryClient = new QueryClient();

function App() {
    const [arr, setArr] = useState<number[]>([1, 2, 3]);
    const [counter1, setCounter1] = useState(0);

    useEffect(() => {
        console.log("EFFECT")
        document.title = "Counter " + counter1
    }, [counter1])

    // console.log("RERENDER APP")

    return (
        <AppContext.Provider value={{rootCounter: counter1}}>
            <QueryClientProvider client={queryClient}>
                <div>
                    <h1>Hello, world!</h1>

                    <hr />

                    <Pokemon />
                    
                    <hr />

                    <PokemonTanstack />

                    <hr />

                    <Advanced />

                    <hr />

                    {arr.map(x => (
                        <div key={x}>{x}</div>
                    ))}

                    <button 
                        onClick={() => {
                            setArr((prevArr) => {
                                return [...prevArr, prevArr.length + 1];
                            })
                        }}
                    >
                        Add
                    </button>

                    <hr />

                    {counter1 <= 10 && (
                        <ControlledCounter 
                            counter={counter1}
                            minus={() => {
                                setCounter1(prevCounter1 => prevCounter1 - 1);
                            }}
                            plus={() => {
                                setCounter1(prevCounter1 => prevCounter1 + 1);
                            }}
                        />
                    )}            

                    <hr />

                    <Counter />
                    
                    {/* <Counter />
                    
                    <Counter /> */}
                </div>
            </QueryClientProvider>
        </AppContext.Provider>
    )
}

export default App;

export interface IAppContext {
    rootCounter: number;
}

export const AppContext = createContext<IAppContext>({
    rootCounter: 0
});

export const useAppContext = () => useContext(AppContext);
