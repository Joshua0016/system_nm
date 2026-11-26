"use client"
import { motion } from 'motion/react'
import { zodResolver } from "@hookform/resolvers/zod"
import { Controller, useForm } from "react-hook-form"
import * as z from "zod"
import { Button } from "@/components/ui/button"
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
    FieldError,
    FieldGroup,
    FieldLabel,
    FieldContent,
    FieldSet,
    FieldLegend,

    FieldTitle
} from "@/components/ui/field"
import {
    RadioGroup,
    RadioGroupItem
} from '@/components/ui/radio-group'
import { Input } from "@/components/ui/input"
import { toast } from 'sonner'

const role = [
    {
        id: 'Admin',
        title: 'Administrador',
        description: 'Acceso a todo el funcionamiento del sistema'
    },
    {
        id: 'Cashier',
        title: 'Cajero',
        description: 'Acceso a modulos de inventario y cuentas por cobrar'

    },
    {
        id: 'Supervisor',
        title: 'Supervisor',
        description: 'Acceso a los módulos del negocio del sistema'
    }
] as const

const formSchema = z.object({
    fullName: z
        .string()
        .trim()
        .min(3, "Este campo debe contener al menos un caracteres.")
        .max(50, "Este campo solo puede contener 50 caracteres."),
    username: z
        .string()
        .trim()
        .min(3, 'Este campo debe contener al menos 3 caracteres')
        .max(20, 'Este campo solo puede contener 20 caracteres'),
    role: z
        .string()
        .min(1, 'Debes seleccioanr al menos un rol'),


})


export default function UserIdentityForm({ setIdentityForm, selectedUser, setUpdateUsers }) {


    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            fullName: selectedUser.fullName,
            username: selectedUser.username,
            role: selectedUser.role,
        },
    })

    function onSubmit(data: z.infer<typeof formSchema>) {
        toast.promise(
            async () => {
                const result = await window.api.user.updateNameRole(selectedUser.id, data);
                if (!result) {
                    throw new Error('Error al actualizar datos...');
                }
                return result;
            },
            {
                position: 'top-center',
                duration: 4000,
                loading: 'Cargando...',
                success: (result) => {
                    setUpdateUsers((prevUser) => prevUser.map((user) => user.id === result.id ? result : user));
                    setIdentityForm(false);
                    return 'Usuario actualizado correctamente';
                },
                error: "Error..."
            }
        )
        // async function save(data) {
        //     try {
        //         const result = await window.api.user.updateNameRole(selectedUser.id, data)
        //         if (result) {

        //             alert('Usuario actualizado correctamente');

        //             return;
        //         }
        //         alert('Ha ocurrido un problema al actualizar los datos, Por favor comunicarse con el soporte')
        //     } catch (error) {
        //         console.log('Error en el catch al guardar los datos- ---> ' + error);
        //     }
        // }
        // save(data);

    }
    return (
        <>
            <motion.div
                initial={{ opacity: 0, scale: 0.95 }} animate={{ opacity: 1, scale: 1 }} exit={{ opacity: 0, scale: 0.95 }} transition={{ duration: 0.1 }}
                className='absolute inset-0 backdrop-blur-[4px] bg-black/20'>
                <div className="min-h-screen flex items-center justify-center p-4">
                    <Card className="w-full sm:max-w-lg">
                        <CardHeader>
                            <CardTitle>Editar usuario {selectedUser.username}</CardTitle>
                            <CardDescription>
                                Inserta los valores adecuados para editar el usuario
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                                <FieldGroup>

                                    <Controller
                                        name="fullName"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field
                                                data-invalid={fieldState.invalid}
                                            >

                                                <FieldContent>
                                                    <FieldLabel htmlFor="form-rhf-demo-fullName">
                                                        Nomre y Apellido
                                                    </FieldLabel>
                                                    <FieldDescription>
                                                        Actualizar nombre y apellido
                                                    </FieldDescription>
                                                    {fieldState.invalid && (
                                                        <FieldError errors={[fieldState.error]} />
                                                    )}
                                                </FieldContent>

                                                <Input
                                                    {...field}
                                                    id="form-rhf-demo-fullName"
                                                    aria-invalid={fieldState.invalid}
                                                    placeholder="Nombre y Apellido"
                                                    autoComplete="off"

                                                />

                                            </Field>
                                        )}
                                    />
                                    <Controller
                                        name="username"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field
                                                data-invalid={fieldState.invalid}
                                            >
                                                <FieldContent>
                                                    <FieldLabel htmlFor="form-rhf-demo-username">
                                                        Nomre de usuario
                                                    </FieldLabel>
                                                    <FieldDescription>
                                                        Actualizar nombre de usuario
                                                    </FieldDescription>
                                                    {fieldState.invalid && (
                                                        <FieldError errors={[fieldState.error]} />
                                                    )}
                                                </FieldContent>

                                                <Input
                                                    {...field}
                                                    id="form-rhf-demo-username"
                                                    aria-invalid={fieldState.invalid}
                                                    placeholder="Nombre de usuario"
                                                    autoComplete="off"

                                                />

                                            </Field>
                                        )}
                                    />
                                    <Controller
                                        name="role"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <FieldSet data-invalid={fieldState.invalid}>
                                                <FieldLegend>Rol</FieldLegend>
                                                <FieldDescription>
                                                    Puedes cambair el rol de tu usuario en cualquier momento
                                                </FieldDescription>
                                                <RadioGroup
                                                    name={field.name}
                                                    value={field.value}
                                                    onValueChange={field.onChange}
                                                    aria-invalid={fieldState.invalid}
                                                >
                                                    {role.map((role) => (
                                                        <FieldLabel
                                                            key={role.id}
                                                            htmlFor={`form-rhf-radiogroup-${role.id}`}
                                                        >
                                                            <Field
                                                                orientation="horizontal"
                                                                data-invalid={fieldState.invalid}
                                                            >
                                                                <FieldContent>
                                                                    <FieldTitle>{role.title}</FieldTitle>
                                                                    <FieldDescription>
                                                                        {role.description}
                                                                    </FieldDescription>
                                                                </FieldContent>
                                                                <RadioGroupItem
                                                                    value={role.id}
                                                                    id={`form-rhf-radiogroup-${role.id}`}
                                                                    aria-invalid={fieldState.invalid}
                                                                />
                                                            </Field>
                                                        </FieldLabel>
                                                    ))}
                                                </RadioGroup>
                                                {fieldState.invalid && (
                                                    <FieldError errors={[fieldState.error]} />
                                                )}
                                            </FieldSet>
                                        )}
                                    />


                                </FieldGroup>
                            </form>
                        </CardContent>
                        <CardFooter>
                            <Field orientation="horizontal" className="flex">
                                <Button type="button" variant="outline" onClick={() => form.reset()}>
                                    Reiniciar
                                </Button>
                                <Button type="submit" form="form-rhf-demo">
                                    Guardar
                                </Button>
                                <div className="ml-auto">
                                    <Button type="button" variant={'destructive'} onClick={() => setIdentityForm(false)}>Cancelar</Button>
                                </div>
                            </Field>
                        </CardFooter>
                    </Card>

                </div>

            </motion.div>
        </>
    )
}