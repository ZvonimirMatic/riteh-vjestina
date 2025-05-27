import { Stack } from "@fluentui/react";
import { NavLink, Outlet } from "react-router-dom";

export default function Layout() {
    return (
        <Stack>
            <Stack horizontal horizontalAlign="space-between">
                <Stack horizontal>
                    <NavLink to="/">Home</NavLink>
                    <NavLink to="/authors">Auhtors</NavLink>
                    <NavLink to="/books">Books</NavLink>
                </Stack>
                
                {/* TODO login dio */}
            </Stack>

            <Outlet />
        </Stack>
    )
}