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

    FieldError,
    FieldGroup,
    FieldLabel,

} from "@/components/ui/field"
import { Input } from "@/components/ui/input"

const formSchema = z.object({
    name: z
        .string()
        .trim()
        .min(1, "Este campo debe contener al menos un carácter.")
        .max(100, "Este campo solo puede contener 100 caracteres."),
    address: z
        .string()
        .trim()
        .min(3, "Este campo debe contener al menos 3 caracteres.")
        .max(100, "Este campo solo puede contener 100 caracteres."),
    phone_number: z
        .string()
        .regex(/^(809|829|849)\d{7}$/, { message: 'Debe ser un número válido: (809-829-849)0000000' }),
    email: z

        .email('Debes insertar un correo electrónico válido')
        .optional()
        .or(z.literal(""))
})
export default function CreateSuppliers() {
    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: "",
            address: "",
            phone_number: "",
            email: "",
        }
    })
    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log(data);
    }
    return (
        <>

            <div className="flex min-h-screen justify-center items-center p-4">
                <Card className="w-full sm:max-w-lg">
                    <CardHeader>
                        <CardTitle>Registrar Proveedor</CardTitle>
                        <CardDescription>
                            Inserta los valores adecuados para el proveedor
                        </CardDescription>
                    </CardHeader>

                    <CardContent>
                        <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                            <FieldGroup>
                                <Controller
                                    name="name"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-name">
                                                Nombre
                                            </FieldLabel>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-name"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Nombre proveedor"
                                                autoComplete="off"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="address"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-address">
                                                Dirección
                                            </FieldLabel>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-address"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Dirección local"
                                                autoComplete="off"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="phone_number"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-phone_number">
                                                Número de teléfono
                                            </FieldLabel>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-phone_number"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Número de teléfono"
                                                autoComplete="off"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="email"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-email">
                                                email
                                            </FieldLabel>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-email"
                                                type="email"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Correo electrónico"
                                                autoComplete="off"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                            </FieldGroup>
                        </form>

                    </CardContent>
                    <CardFooter>
                        <Field orientation="horizontal">
                            <Button type="button" variant="outline" onClick={() => form.reset()}>
                                Reiniciar
                            </Button>
                            <Button type="submit" form="form-rhf-demo">
                                Guardar
                            </Button>
                        </Field>
                    </CardFooter>
                </Card>
            </div >
        </>
    )
}