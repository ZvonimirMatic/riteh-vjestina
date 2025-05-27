import { useQuery } from "@tanstack/react-query"
import type BookDto from "./Dtos/BookDto";

export default function Books() {
    const api = useQuery({
        queryKey: ["books"],
        queryFn: async () => {
            const response = await fetch(
                "https://localhost:7103/api/books", 
                { credentials: "include" });
            return await response.json() as BookDto[];
        } 
    })

    return (
        <div>
            {api.data?.map(x => <div key={x.id}>{x.title}</div>)}
        </div>
    )
}