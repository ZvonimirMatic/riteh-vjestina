import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./Home";
import Layout from "./Layout";
import Authors from "./Authors";
import Books from "./Books";

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<Layout />}>
                    <Route index element={<Home />} />

                    <Route path="authors" element={<Authors />} />

                    <Route path="books" element={<Books />} />
                </Route>
            </Routes>
        </BrowserRouter>
    )
}