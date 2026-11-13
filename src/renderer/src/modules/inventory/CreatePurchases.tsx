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
    invoice_number: z.
        string()
        .trim()
})


export default function CreatePurchases() {
    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            invoice_number: '',
        },
    });

    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log(data);
    }

    return (
        <>

            <div className="min-h-screen flex justify-center items-center p-4">
                <Card className="sm:min-w-[80%]">
                    <CardHeader>
                        <CardTitle>
                            Registrar Compra
                        </CardTitle>

                    </CardHeader>
                    <CardContent>
                        <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                            <FieldGroup>
                                <Controller
                                    name="invoice_number"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <FieldLabel htmlFor="form-rhf-demo-invoice_number">
                                                Número Fact.
                                            </FieldLabel>
                                            <Input className="sm:max-w-40"
                                                {...field}
                                                id="form-rhf-demo-invoice_number"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Número factura"
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

                </Card>

            </div>
        </>
    )
}