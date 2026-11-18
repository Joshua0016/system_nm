"use client"
import { zodResolver } from "@hookform/resolvers/zod"
import { Controller, useForm } from "react-hook-form"
import * as z from "zod"
import { Button } from "@/components/ui/button"
import { FileQuestionMark } from "lucide-react"
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
import { useRef } from "react"

const formSchema = z.object({
    invoice_number: z.
        string()
        .trim()
        .min(1, 'Debe contener al menos un caracter'),
    supplier_id: z.
        string()
})


export default function CreatePurchases() {
    const input1Ref = useRef<HTMLInputElement>(null);
    const input2Ref = useRef<HTMLInputElement>(null);

    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            invoice_number: '',
        },
    });

    function onSubmit(data: z.infer<typeof formSchema>) {
        console.log(data);
    }

    //handle invoice_number
    const handleInvoiceNumber = (e) => {


    }
    //hadle supplier event
    const handleKeyDown = (e) => {


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
                                        <Field data-invalid={fieldState.invalid} className="flex flex-row sm:max-w-60">
                                            <FieldLabel htmlFor="form-rhf-demo-invoice_number" className="">
                                                Número Fact.
                                            </FieldLabel>
                                            <Input className="sm:max-w-40"
                                                {...field}
                                                id="form-rhf-demo-invoice_number"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Número factura"
                                                autoComplete="off"
                                                ref={input1Ref}
                                                onKeyDown={handleInvoiceNumber}
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                                <Controller
                                    name="supplier_id"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid} className="flex flex-row sm:max-w-75">
                                            <FieldLabel htmlFor="form-rhf-demo-supplier_id">
                                                Codigo Suplidor
                                            </FieldLabel>
                                            <Input className="sm:max-w-40"
                                                {...field}
                                                id="form-rhf-demo-supplier_id"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Codigo suplidor"
                                                autoComplete="off"
                                                ref={input2Ref}
                                                onKeyDown={handleKeyDown}
                                            />
                                            <Button className='sm:max-w-8 cursor-pointer'>
                                                <FileQuestionMark />
                                            </Button>
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