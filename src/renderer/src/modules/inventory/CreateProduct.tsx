"use client"

import * as React from "react"
import { zodResolver } from "@hookform/resolvers/zod"
import { Controller, useForm } from "react-hook-form"
import { toast } from "sonner"
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
} from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import {
    InputGroup,
    InputGroupAddon,
    InputGroupText,
    InputGroupTextarea,
} from "@/components/ui/input-group"

const formSchema = z.object({
    reference: z
        .string()
        .trim()
        .min(1, "Este campo debe contener al menos un carácter.")
        .max(50, "Este campo solo puede contener 50 caracteres."),
    description: z
        .string()
        .trim()
        .min(3, "Este campo debe contener al menos 3 caracteres.")
        .max(60, "Este campo solo puede contener 60 caracteres."),
})

export default function CreateProduct() {
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            reference: "",
            description: "",
        },
    })

    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log("hlaa");
    }

    return (
        <div className="flex min-h-screen items-center justify-center p-4">
            <Card className="w-full sm:max-w-md ">
                <CardHeader>
                    <CardTitle>Registrar Producto</CardTitle>
                    <CardDescription>
                        Inserta los valores adecuados para registrar para el producto
                    </CardDescription>
                </CardHeader>
                <CardContent>
                    <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                        <FieldGroup>
                            <Controller
                                name="reference"
                                control={form.control}
                                render={({ field, fieldState }) => (
                                    <Field data-invalid={fieldState.invalid}>
                                        <FieldLabel htmlFor="form-rhf-demo-reference">
                                            Referencia
                                        </FieldLabel>
                                        <Input
                                            {...field}
                                            id="form-rhf-demo-reference"
                                            aria-invalid={fieldState.invalid}
                                            placeholder="Referencia del producto"
                                            autoComplete="off"
                                        />
                                        {fieldState.invalid && (
                                            <FieldError errors={[fieldState.error]} />
                                        )}
                                    </Field>
                                )}
                            />
                            <Controller
                                name="description"
                                control={form.control}
                                render={({ field, fieldState }) => (
                                    <Field data-invalid={fieldState.invalid}>
                                        <FieldLabel htmlFor="form-rhf-demo-description">
                                            Descripción
                                        </FieldLabel>
                                        <Input
                                            {...field}
                                            id="form-rhf-demo-description"
                                            aria-invalid={fieldState.invalid}
                                            placeholder="Descripción breve del producto"
                                            autoComplete="off"
                                        />
                                        <FieldDescription>
                                            Incluye una descripción que puedas identificar de manera rapida el producto
                                        </FieldDescription>
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
        </div>

    )
}
