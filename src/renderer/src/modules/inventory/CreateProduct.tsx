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
    FieldContent,
} from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectSeparator,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"

const measurements = [
    { label: "Unidad", value: "unidad" },
    { label: "Caja", value: "caja" },
    { label: "Botella", value: "botella" },
    { label: "Frasco", value: "frasco" },
    { label: "Lata", value: "lata" },
    { label: "Paquete", value: "paquete" },

] as const

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
    unit_measurement: z
        .string()
        .min(1, "Por favor selecciona la unidad de medida")
        .refine((val) => val !== 'auto', {
            message:
                "Auto-detección no es permitido. Por favor seleccionar una opción válida"
        }),
    cost: z
        .string()
        .transform((val) => (val === "" ? 0 : Number(val))) // Si está vacío es 0, si no, lo convierte
        .pipe(
            z.number({ message: "Solo puedes ingresar números" })
                .nonnegative("El costo no puede ser negativo") // 🛑 Bloquea los negativos
        ),
    markup: z
        .string()
        .transform((val) => (val === "" ? 0 : Number(val)))
        .pipe(
            z.number({ message: "Solo puedes ingresar números" })
                .positive({ message: "La Utilidad S/costo debe ser mayor a 0" })
        ),
    price: z
        .string()
        .transform((val) => (val === "" ? 0 : Number(val)))
        .pipe(
            z.number({ message: "Solo puedes ingresar números" })
                .nonnegative({ message: "El precio no puede ser negativo" })
        )
})

export default function CreateProduct() {
    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            reference: "",
            description: "",
            unit_measurement: "",
            cost: "",
            markup: '',
            price: "",
        },
    })

    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log(data);
    }

    return (
        <div className="flex min-h-screen items-center justify-center p-4">
            <Card className="w-full sm:max-w-lg ">
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
                            <div className="flex justify-between gap-2">
                                <Controller
                                    name="unit_measurement"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field

                                            data-invalid={fieldState.invalid}
                                        >
                                            <FieldContent>
                                                <FieldLabel htmlFor="form-rhf-select-unit_measurement">
                                                    Unidades
                                                </FieldLabel>
                                                <FieldDescription>
                                                    Selecciona la unidad del producto
                                                </FieldDescription>
                                                {fieldState.invalid && (
                                                    <FieldError errors={[fieldState.error]} />
                                                )}
                                            </FieldContent>
                                            <Select
                                                name={field.name}
                                                value={field.value}
                                                onValueChange={field.onChange}
                                            >
                                                <SelectTrigger
                                                    id="form-rhf-select-unit_measurement"
                                                    aria-invalid={fieldState.invalid}
                                                    className="min-w-[120px]"
                                                >
                                                    <SelectValue placeholder="Select" />
                                                </SelectTrigger>
                                                <SelectContent side="left" >
                                                    <SelectItem value="auto">Auto</SelectItem>
                                                    <SelectSeparator />
                                                    {measurements.map((measurements) => (
                                                        <SelectItem key={measurements.value} value={measurements.value}>
                                                            {measurements.label}
                                                        </SelectItem>
                                                    ))}
                                                </SelectContent>
                                            </Select>
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="markup"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field
                                            data-invalid={fieldState.invalid}
                                        >

                                            <FieldContent>
                                                <FieldLabel htmlFor="form-rhf-demo-markup">
                                                    Utilidad S/Costo
                                                </FieldLabel>
                                                <FieldDescription>
                                                    Define el precio final de venta
                                                </FieldDescription>
                                                {fieldState.invalid && (
                                                    <FieldError errors={[fieldState.error]} />
                                                )}
                                            </FieldContent>

                                            <Input
                                                {...field}
                                                id="form-rhf-demo-markup"
                                                aria-invalid={fieldState.invalid}
                                                onFocus={(e) => e.target.select()}
                                                placeholder="0.00"
                                                autoComplete="off"

                                            />

                                        </Field>
                                    )}
                                />
                            </div>
                            <div className="flex ">
                                <Controller
                                    name="cost"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-cost">
                                                Costo
                                            </FieldLabel>
                                            <FieldDescription>
                                                Costo por unidad
                                            </FieldDescription>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-cost"
                                                placeholder="0.00"
                                                aria-invalid={fieldState.invalid}
                                                onFocus={(e) => e.target.select()}
                                                autoComplete="off"
                                                className="sm:max-w-20"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="price"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-price">
                                                Precio
                                            </FieldLabel>
                                            <FieldDescription>
                                                Precio por unidad
                                            </FieldDescription>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-price"

                                                aria-invalid={fieldState.invalid}
                                                placeholder="0.00"
                                                onFocus={(e) => e.target.select()}
                                                autoComplete="off"
                                                className="sm:max-w-20"
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />

                            </div>

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
