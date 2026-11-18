"use client"
import { motion } from 'motion/react'
import { Button } from "@/components/ui/button"
import { Checkbox } from "@/components/ui/checkbox"
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {
    Field,
    FieldDescription,
    FieldLabel,
    FieldContent,
    FieldTitle
} from "@/components/ui/field"
import { useState } from "react"
export default function UserStatusSwitch({ setUserStatusSwitch, selectedUser, setUpdateUsers }) {
    const [status, setStatus] = useState(selectedUser.isActive)
    const handdleButton = async () => {
        try {
            if (status) {
                await window.api.user.activate(selectedUser.id);
                alert('El usuario ha sido activado ! ');
                const updateUser = await window.api.user.getAll();
                setUpdateUsers(updateUser);
                setUserStatusSwitch(false);
                return;
            }
            else {
                await window.api.user.desactivate(selectedUser.id);
                alert('El usuario ha sido desactivado !');
                const updateUser = await window.api.user.getAll();
                setUpdateUsers(updateUser);
                setUserStatusSwitch(false);
            }
        } catch (error) {
            console.log("Ha ocurrido un error, try-catch frontend----->" + error);
        }
    }
    return (
        <>
            <motion.div
                initial={{ opacity: 0, scale: 0.95 }} animate={{ opacity: 1, scale: 1 }} exit={{ opacity: 0, scale: 0.95 }} transition={{ duration: 0.1 }}
                className='absolute inset-0 backdrop-blur-[4px] bg-black/20'>
                <div className="min-h-screen flex items-center justify-center p-4">
                    <Card className="w-full sm:max-w-lg">
                        <CardHeader>
                            <CardTitle>Activar o Desactivar usuario: {selectedUser.username} </CardTitle>
                            <CardDescription>
                                Para activar <span className="text-blue-600">Marcar</span>, para desactivar <span className="text-red-600">Desmarcar</span> la casilla de verificación.
                            </CardDescription>
                        </CardHeader>
                        <CardContent>

                            <FieldLabel>
                                <Field orientation="horizontal">
                                    <Checkbox id="toggle-checkbox-2" name="toggle-checkbox-2" checked={status} onCheckedChange={(e) => setStatus(e)} />
                                    <FieldContent>
                                        <FieldTitle>Activar/Desactivar</FieldTitle>
                                        <FieldDescription>
                                            Puedes volver activar tus usuarios en cualquier momento!
                                        </FieldDescription>
                                    </FieldContent>
                                </Field>
                            </FieldLabel>

                        </CardContent>
                        <CardFooter>
                            <Field orientation="horizontal" className="flex">
                                <Button type="submit" form="form-rhf-demo" onClick={handdleButton}>
                                    Guardar
                                </Button>
                                <div className="ml-auto">
                                    <Button type="button" variant={'destructive'} onClick={() => setUserStatusSwitch(false)} >Cancelar</Button>
                                </div>
                            </Field>
                        </CardFooter>
                    </Card>
                </div>
            </motion.div>
        </>
    )
}