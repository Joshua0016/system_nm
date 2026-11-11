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

} from "@/components/ui/field"
import { Input } from "@/components/ui/input"

const formSchema = z.object({
    newPassword: z
        .string()
        .trim()
        .min(3, "Este campo debe contener al menos un caracter.")
        .max(15, "Este campo solo puede contener 15 caracteres."),

})
export default function PasswordForm({ setPasswordForm, selectedUser }) {
    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            newPassword: ""
        },
    })
    function onSubmit(data: z.infer<typeof formSchema>) {
        async function save() {
            try {
                await window.api.user.updatePassword(selectedUser.id, data);
                alert("Contraseña actualizada");
                setPasswordForm(false);
                return;

            } catch (error) {
                alert('ha ocurrido un error al realizar la operación, favor de comunicarse con soporte');
                console.log('Error en el catch de la operacion ----->' + error)
            }
        }
        save()

    }
    return (
        <>
            <div className="absolute inset-0 backdrop-blur-[4px] bg-black/20">
                <div className="flex min-h-screen items-center justify-center p-4">
                    <Card className="w-full sm:max-w-lg">
                        <CardHeader>
                            <CardTitle>Editar contraseña de {selectedUser.username}</CardTitle>
                            <CardDescription>
                                Inserta los valores adecuados para editar la contraseña
                            </CardDescription>
                        </CardHeader>
                        <CardContent>
                            <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                                <FieldGroup>

                                    <Controller
                                        name="newPassword"
                                        control={form.control}
                                        render={({ field, fieldState }) => (
                                            <Field
                                                data-invalid={fieldState.invalid}
                                            >

                                                <FieldContent>
                                                    <FieldLabel htmlFor="form-rhf-demo-newPassword">
                                                        Contraseña
                                                    </FieldLabel>
                                                    <FieldDescription>
                                                        Actualizar contraseña
                                                    </FieldDescription>
                                                    {fieldState.invalid && (
                                                        <FieldError errors={[fieldState.error]} />
                                                    )}
                                                </FieldContent>

                                                <Input
                                                    {...field}
                                                    id="form-rhf-demo-newPassword"
                                                    aria-invalid={fieldState.invalid}
                                                    placeholder="contraseña"
                                                    autoComplete="off"
                                                    type="password"
                                                />

                                            </Field>
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
                                    <Button type="button" variant={'destructive'} onClick={() => setPasswordForm(false)}>Cancelar</Button>
                                </div>
                            </Field>
                        </CardFooter>
                    </Card>
                </div>

            </div>
        </>
    )
}