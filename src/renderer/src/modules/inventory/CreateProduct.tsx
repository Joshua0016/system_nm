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
    FieldSeparator,

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
import { FileQuestionMark } from "lucide-react"
import { toast } from "sonner"
import { Toaster } from "@/components/ui/sonner"
import { useState, useEffect } from "react"
import getProductByCode from "@renderer/api/product"
import Search from "@renderer/components/Search"
import { AnimatePresence } from "motion/react"

const measurements = [
    { label: "Unidad", value: "Unidad" },
    { label: "Pulgada", value: "Pulgada" },
    { label: "Centimetros", value: "Centimetros" },
    { label: "Milimetros", value: "Milimetros" },
    { label: "Metros", value: "Metros" },

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
    unitMeasurement: z
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
        ),
    stock: z
        .string()
        .transform((val) => (val === "" ? 0 : Number(val)))
        .pipe(
            z.number({ message: "Solo puedes ingresar números" })
                .nonnegative({ message: "La cantidad no puede ser negativa" })
        ),


}).superRefine((values, ctx) => {
    const isDecimal = !Number.isInteger(values.stock);
    const isUnit = values.unitMeasurement === 'Unidad';

    if (isDecimal && isUnit) {
        ctx.addIssue({
            code: 'custom',
            path: ['stock'],
            message: 'Los productos por unidad solo admiten una Cantidad con número entero'
        })
    }
})
const codeSchema = z.object({
    code: z
        .string()
        .trim()
        .min(10, 'Este campo debe contener 10 caracteres')
        .max(10, 'Este campo debe contener 10 caracteres'),
})
interface Product {
    id: number,
    code: string,
    reference: string,
    description: string,
    unitMeasurement: string,
    cost: string,
    markup: string,
    price: string,
    stock: string
}
export default function CreateProduct() {
    const [product, setProduct] = useState<Product>();
    const [search, setSearch] = useState(false);

    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            reference: '',
            description: "",
            unitMeasurement: "",
            cost: "",
            markup: '',
            price: "",
            stock: ''
        },
    });
    const formCodeInput = useForm({
        resolver: zodResolver(codeSchema),
        defaultValues: {
            code: '',
        }
    })

    useEffect(() => {
        if (product) {
            form.reset({
                reference: product.reference,
                description: product.description,
                unitMeasurement: product.unitMeasurement,
                cost: String(product.cost),
                markup: String(product.markup),
                price: String(product.price),
                stock: String(product.stock)
            });
            formCodeInput.reset({
                code: product.code
            })
        } else {
            form.reset({
                reference: '',
                description: '',
                unitMeasurement: '',
                cost: '',
                markup: '',
                price: '',
                stock: ''
            });
            formCodeInput.reset({
                code: ''
            })
        }

    }, [product, form]);

    function onSubmit(data: z.infer<typeof formSchema>) {

        if (product?.id) {
            //logica de actualizacion

            toast.promise(
                async () => {
                    const result = await window.api.product.update(product.id, data);
                    if (!result) {

                        throw new Error('Error al recibir la respuesta del servidor')
                    }
                    return result
                },
                {
                    position: 'top-center',
                    loading: 'Cargando...',
                    success: () => { setProduct(undefined); form.reset(); formCodeInput.reset(); return 'El producto ha sido actualizado' },
                    error: (err) => { console.log('Error al realizar la operación--->' + err); return 'Error al actualizar el producto'; }
                }
            )

        }
        else {//CREAACION
            toast.promise(
                async () => {
                    const result = await window.api.product.create(data);
                    if (!result) {
                        throw new Error('Error al realizar la petición')
                    }
                    return result;
                }
                ,
                {
                    position: 'top-center',
                    loading: 'Cargando...',
                    success: () => { setProduct(undefined); form.reset(); return 'El producto ha sido creado' },
                    error: (err) => { console.log('Error al realizar la operación--> ' + err); return 'Error al crear el producto' }
                });
        }
    }
    //evento para buscar productos por codigo
    async function onSubmitCode(data: z.infer<typeof codeSchema>) {
        try {
            const result = await getProductByCode(data.code.toUpperCase());
            if (!result) {
                toast.error('No se ha podido encontrar el producto');
                setProduct(undefined);
            } else {
                setProduct(result);
            }
        } catch (error) {
            console.log('Error al recibir los datos frontend createProduct----> ' + error);
        }

    }
    //evento para buscar productos en un componente search


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
                    <form id="form-rhf-code" onSubmit={formCodeInput.handleSubmit(onSubmitCode)}>
                        <Controller
                            name="code"
                            control={formCodeInput.control}
                            render={({ field, fieldState }) => (
                                <FieldContent data-invalid={fieldState.invalid} className="">

                                    <div className="flex flex-row gap-2 mb-4">
                                        <FieldLabel htmlFor="form-rhf-demo-code">
                                            Codigo
                                        </FieldLabel>

                                        <Input
                                            {...field}
                                            id="form-rhf-demo-code"
                                            aria-invalid={fieldState.invalid}
                                            placeholder="PRD-000000"
                                            autoComplete="off"
                                            className="sm:max-w-25"

                                        />
                                        <Button className='sm:max-w-8 cursor-pointer' onClick={() => setSearch(true)}>
                                            <FileQuestionMark />
                                        </Button>
                                    </div>

                                    {fieldState.invalid && (
                                        <FieldError errors={[fieldState.error]} />
                                    )}
                                </FieldContent>

                            )}
                        />
                    </form>

                    <FieldSeparator className="mb-2"></FieldSeparator>
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
                                    name="unitMeasurement"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field

                                            data-invalid={fieldState.invalid}
                                        >
                                            <FieldContent>
                                                <FieldLabel htmlFor="form-rhf-select-unitMeasurement">
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
                                                    id="form-rhf-select-unitMeasurement"
                                                    aria-invalid={fieldState.invalid}
                                                    className="min-w-30"
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
                                <Controller
                                    name="stock"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-stock">
                                                Cantidad
                                            </FieldLabel>
                                            <FieldDescription>
                                                Cantidad actual
                                            </FieldDescription>
                                            <Input
                                                {...field}
                                                id="form-rhf-demo-stock"

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
                        <Button type="button" variant="outline" onClick={() => {
                            setProduct(undefined);
                            form.reset();
                            formCodeInput.reset();
                        }}>
                            Reiniciar
                        </Button>
                        <Button type="submit" form="form-rhf-demo">
                            Guardar
                        </Button>
                    </Field>
                </CardFooter>
            </Card>
            <Toaster />
            <AnimatePresence>
                {search && <Search setSearch={setSearch} setProduct={setProduct} />}

            </AnimatePresence>
        </div>

    )
}
