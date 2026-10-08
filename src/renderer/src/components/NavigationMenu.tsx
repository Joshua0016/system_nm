import { Button } from "@/components/ui/button"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuGroup,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
    DropdownMenuSub,
    DropdownMenuSubTrigger,
    DropdownMenuPortal,
    DropdownMenuSubContent,
    DropdownMenuShortcut
} from "@/components/ui/dropdown-menu"
import { useState } from "react"
import { NavLink } from "react-router"

export default function NavigationLinks() {
    const [IsActive, setIsActive] = useState('')
    return (
        <>
            <div className="m-1 w-screen flex gap-1.5">
                <DropdownMenu>
                    {/*Cuenta Corriente */}
                    <DropdownMenuTrigger render={<Button variant={IsActive === 'btn-1' ? 'blue' : 'outline'} className={'cursor-pointer'} onClick={() => setIsActive('btn-1')} />}>
                        Cuenta CxC Corriente
                    </DropdownMenuTrigger>
                    <DropdownMenuContent>
                        <DropdownMenuGroup>
                            <DropdownMenuLabel>My account</DropdownMenuLabel>
                            <DropdownMenuItem>Profile</DropdownMenuItem>
                            <DropdownMenuItem>Billing</DropdownMenuItem>
                        </DropdownMenuGroup>
                        <DropdownMenuSeparator />
                        <DropdownMenuGroup>
                            <DropdownMenuItem>Team</DropdownMenuItem>
                            <DropdownMenuItem>Subscription</DropdownMenuItem>
                        </DropdownMenuGroup>
                    </DropdownMenuContent>
                </DropdownMenu>
                <DropdownMenu>
                    {/*Caja y recaudaciones */}
                    <DropdownMenuTrigger render={<Button variant={IsActive === 'btn-2' ? 'blue' : 'outline'} className={'cursor-pointer'} onClick={() => setIsActive('btn-2')} />}>
                        Caja y recaudaciones
                    </DropdownMenuTrigger>
                    <DropdownMenuContent>
                        <DropdownMenuGroup>
                            <DropdownMenuLabel>my account</DropdownMenuLabel>
                            <DropdownMenuItem>Profile</DropdownMenuItem>
                            <DropdownMenuItem>Billing</DropdownMenuItem>
                        </DropdownMenuGroup>
                        <DropdownMenuSeparator />
                        <DropdownMenuGroup>
                            <DropdownMenuItem>Team</DropdownMenuItem>
                            <DropdownMenuItem>Subscription</DropdownMenuItem>
                        </DropdownMenuGroup>
                    </DropdownMenuContent>
                </DropdownMenu>
                <DropdownMenu>
                    {/*Inventario */}
                    <DropdownMenuTrigger render={<Button variant={IsActive === 'btn-3' ? 'blue' : 'outline'} className={'cursor-pointer'} onClick={() => setIsActive('btn-3')} />}>
                        Inventario
                    </DropdownMenuTrigger>
                    <DropdownMenuContent>
                        <DropdownMenuGroup>
                            <DropdownMenuLabel>Productos</DropdownMenuLabel>
                            <DropdownMenuItem className={'cursor-pointer'}><NavLink to={'createProduct'}>Registrar</NavLink></DropdownMenuItem>
                            <DropdownMenuItem className={'cursor-pointer'}>Billing</DropdownMenuItem>

                            <DropdownMenuSeparator />

                            <DropdownMenuGroup>
                                <DropdownMenuSub>
                                    <DropdownMenuSubTrigger>Consultas</DropdownMenuSubTrigger>
                                    <DropdownMenuPortal>
                                        <DropdownMenuSubContent>
                                            <DropdownMenuItem className={'cursor-pointer'}>Movimientos</DropdownMenuItem>
                                            <DropdownMenuItem>Message</DropdownMenuItem>

                                        </DropdownMenuSubContent>
                                    </DropdownMenuPortal>
                                </DropdownMenuSub>
                            </DropdownMenuGroup>
                        </DropdownMenuGroup>
                    </DropdownMenuContent>
                </DropdownMenu>
                <DropdownMenu>
                    {/*Sistema de seguridad */}
                    <DropdownMenuTrigger render={<Button variant={IsActive === 'btn-4' ? 'blue' : "outline"} className={'cursor-pointer'} onClick={() => setIsActive('btn-4')} />}>
                        Sistema de seguridad
                    </DropdownMenuTrigger>
                    <DropdownMenuContent>
                        <DropdownMenuGroup>
                            <DropdownMenuLabel>nada</DropdownMenuLabel>
                            <DropdownMenuItem>Profile</DropdownMenuItem>
                            <DropdownMenuItem>Billing</DropdownMenuItem>
                        </DropdownMenuGroup>
                        <DropdownMenuSeparator />
                        <DropdownMenuGroup>
                            <DropdownMenuItem>Team</DropdownMenuItem>
                            <DropdownMenuItem>Subscription</DropdownMenuItem>
                        </DropdownMenuGroup>
                    </DropdownMenuContent>
                </DropdownMenu>
            </div>


        </>
    )
}