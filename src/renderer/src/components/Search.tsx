"use client"
import * as z from "zod"
import { zodResolver } from "@hookform/resolvers/zod"
import { Controller, useForm } from "react-hook-form"

import { motion } from "motion/react";
import { useState, useRef, useEffect } from "react";

import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
} from "./ui/card";
import {
    Field,
    FieldError,
} from "./ui/field";
import { Button } from "./ui/button";
import { Input } from './ui/input';

import DataTable, { TableColumn, TableStyles } from "react-data-table-component";

const formSchema = z.object({
    search: z.
        string()
        .trim()
})

interface Product {
    code: string,
    reference: string,
    description: string,
    stock: number,
    price: number,

}
export default function Search({ setSearch, setProduct }) {
    const [dataProduct, setDataProduct] = useState([]);
    const searchInputRef = useRef<HTMLInputElement>(null);

    const form = useForm({
        resolver: zodResolver(formSchema),
        defaultValues: {
            search: ''
        }
    })
    useEffect(() => {
        searchInputRef.current?.focus()
    })



    const columsn: TableColumn<Product>[] = [
        { name: 'Codigo', selector: (row) => row.code, sortable: true },
        { name: 'Referencia', selector: (row) => row.reference, sortable: true },
        { name: 'Descripción', selector: (row) => row.description, width: '400px', sortable: true },
        { name: 'Cantidad', selector: (row) => row.stock },
        { name: 'Precio', selector: (row) => row.price },
    ];
    const customStyles: TableStyles = {
        headRow: {
            style: {
                backgroundColor: '#f0f4ff',
                fontWeight: 700,
                fontSize: '14px'

            },
        },
        rows: {
            style: {
                fontSize: '15px',

            },
            selectedHighlightStyle: {
                backgroundColor: '#e0eaff',
            },
        },
        cells: {
            style: {
                paddingLeft: '16px',
                paddingRight: '16px',
            },
        },
    };

    const handlerClick = (product) => {
        setProduct(product)
        setSearch(false)
    }
    async function onSubmit(data: z.infer<typeof formSchema>) {
        try {
            const product = await window.api.product.get(data.search);
            if (!product) {
                throw new Error('Error al recibir los datos del servidor');
            }
            setDataProduct(product);
        } catch (error) {
            console.log('Error catch:frontend------> ' + error);
        }
    }
    return (<>
        <motion.div
            initial={{ opacity: 0, scale: 0.95 }}
            animate={{ opacity: 1, scale: 1 }}
            exit={{ opacity: 0, scale: 0.95 }}
            transition={{ duration: 0.1 }}
            className="absolute inset-0 backdrop-blur-[4px] bg-black/20">

            <div className="sm:min-h-screen flex justify-center items-center p-4">
                <div className="sm:min-w-[70%] ">

                    <Card className="">
                        <CardHeader>

                            <form id="form-rhf-demo" onSubmit={form.handleSubmit(onSubmit)}>
                                <Controller
                                    name="search"
                                    control={form.control}
                                    render={({ field, fieldState }) => (
                                        <Field data-invalid={fieldState.invalid}>
                                            <Input className="sm:max-w-40 m-2"
                                                {...field}
                                                id="form-rhf-demo-search"
                                                aria-invalid={fieldState.invalid}
                                                placeholder="Buscar"
                                                autoComplete="off"
                                                ref={searchInputRef}
                                            />
                                            {fieldState.invalid && (
                                                <FieldError errors={[fieldState.error]} />
                                            )}
                                        </Field>
                                    )}
                                />
                            </form>
                        </CardHeader>

                        <CardContent className="sm:max-h-112.5 overflow-y-auto">

                            <DataTable
                                columns={columsn}
                                data={dataProduct}
                                customStyles={customStyles}
                                highlightOnHover
                                onRowClicked={handlerClick}
                                pointerOnHover
                            ></DataTable>

                        </CardContent>

                        <CardFooter>
                            <Field orientation="horizontal">
                                <Button type="button" variant={'outline'} onClick={() => setSearch(false)}>Cancelar</Button>

                            </Field>
                        </CardFooter>
                    </Card>

                </div>


            </div>

        </motion.div>
    </>)
}