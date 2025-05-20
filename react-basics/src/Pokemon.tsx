import { useEffect, useState } from "react"

export default function Pokemon() {
    const [pokemon, setPokemon] = useState<string[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [page, setPage] = useState(1);

    useEffect(() => {
        const fetchPokemon = async () => {
            setIsLoading(true);
            const response = await fetch("https://pokeapi.co/api/v2/pokemon?limit=20&offset=" + ((page - 1) * 20).toString())
            const jsonResponse = await response.json();
            setPokemon(jsonResponse.results.map((x: any) => x.name))
            setIsLoading(false);
        }

        fetchPokemon();  
    }, [page]);

    if (isLoading) {
        return <div>Loading pokemon...</div>
    }

    return (
        <div>
            {pokemon.map(x => (
                <div key={x}>{x}</div>
            ))}

            <div>
                <button onClick={() => setPage(1)}>1</button>
                <button onClick={() => setPage(2)}>2</button>
                <button onClick={() => setPage(3)}>3</button>
            </div>
        </div>
    )
}