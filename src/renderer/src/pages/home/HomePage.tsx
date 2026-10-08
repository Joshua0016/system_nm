import NavigationLinks from "@renderer/components/NavigationMenu"
import { Outlet } from "react-router"

export default function HomePage() {
    return (
        <>
            <NavigationLinks></NavigationLinks>
            <Outlet></Outlet>
        </>
    )
}