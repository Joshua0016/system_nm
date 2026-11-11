"use client"
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
    password: z
        .string()
        .trim()
        .min(6, 'Este campo debe contener al menos 6 caracteres')
        .max(10, 'Este campo solo puede contener 10 caracteres'),
    role: z
        .string()
        .min(1, 'Debes seleccioanr al menos un rol'),


})
export default function CreateNewUser({ setNewUserForm, setUpdateUser }) {
    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            fullName: "",
            username: "",
            password: "",
            role: "",
        },
    })
    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log(data)
        async function save() {
            try {
                const result = await window.api.user.create(data);
                if (result) {
                    setUpdateUser((prevUser) => [...prevUser, result]);
                    alert('Usuario creado exitosamente ! ');
                    setNewUserForm(false);
                    return;
                }
            } catch (error) {
                alert('Ha ocurrido un error, favor de comunicarse con soporte');
                console.log('Error trycatch de la operacion, crear usuario ----> ' + error);
            }
        }
        save();
    }

    return (
        <>
            <div className='absolute inset-0 backdrop-blur-[4px] bg-black/20'>
                <div className="min-h-screen flex items-center justify-center p-4">
                    <Card className="w-full sm:max-w-lg">
                        <CardHeader>
                            <CardTitle>Crear nuevo usuario </CardTitle>
                            <CardDescription>
                                Inserta los valores adecuados para crear el usuario
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
                                                        Nombre y apellido del nuevo usuario
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
                                                        Un nombre facil de identificar
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
                                        name="password"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field
                                                data-invalid={fieldState.invalid}
                                            >

                                                <FieldContent>
                                                    <FieldLabel htmlFor="form-rhf-demo-password">
                                                        Contraseña
                                                    </FieldLabel>
                                                    <FieldDescription>
                                                        Una clave que puedas recordar con facilidad
                                                    </FieldDescription>
                                                    {fieldState.invalid && (
                                                        <FieldError errors={[fieldState.error]} />
                                                    )}
                                                </FieldContent>

                                                <Input
                                                    {...field}
                                                    id="form-rhf-demo-password"
                                                    aria-invalid={fieldState.invalid}
                                                    placeholder="Contraseña"
                                                    autoComplete="off"
                                                    type="password"

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
                                                    Asignar el rol del nuevo usuario
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
                                    <Button type="button" variant={'destructive'} onClick={() => setNewUserForm(false)} >Cancelar</Button>
                                </div>
                            </Field>
                        </CardFooter>
                    </Card>

                </div>

            </div>
        </>
    )
}