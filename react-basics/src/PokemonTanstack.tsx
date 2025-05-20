import { useQuery } from "@tanstack/react-query"
import { useState } from "react";

export default function PokemonTanstack() {
    const [page, setPage] = useState(1);
    const {data: pokemon, isLoading} = useQuery({
        queryKey: ['pokemon', page],
        queryFn: async () => {
            const response = await fetch("https://pokeapi.co/api/v2/pokemon?limit=20&offset=" + ((page - 1) * 20).toString())
            const jsonResponse = await response.json();
            return jsonResponse.results.map((x: any) => x.name) as string[];
        },
        retry: false,
    })

    if (isLoading) {
        return <div>Loading pokemon...</div>
    }

    return (
        <div>
            {pokemon?.map(x => (
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