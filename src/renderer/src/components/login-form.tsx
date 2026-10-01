"use client"

import { cn } from "cn"

import { Button } from "@/components/ui/button"
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
  FieldSeparator,
} from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import { GalleryVerticalEndIcon } from "lucide-react"
import login from "@renderer/apiService/login"
import { useEffect, useState } from "react"

export function LoginForm({ className, ...props }: React.ComponentProps<"div">) {
  const [login_message, setLoginMessage] = useState('Iniciar Sesión')

  const handdleButton = async (e) => {
    e.preventDefault();
    const form = e.target as HTMLFormElement;
    setLoginMessage("Cargando...")

    const username = (form.elements.namedItem("email") as HTMLInputElement).value;
    const password_hash = (form.elements.namedItem("password") as HTMLInputElement).value

    const loginDto = {
      username,
      password_hash
    }

    try {
      const data = await login(loginDto);
      if (data) {
        setLoginMessage('Completado')
      }
      else {
        setLoginMessage("Error al inicar sesión...")
      }

    } catch (error) {
      console.log('Error al inicar sesion, try catch login-form ----> ' + error);

    }
  }
  return (
    <div className={cn("flex flex-col gap-6", className)} {...props}>
      <form onSubmit={handdleButton}>
        <FieldGroup>
          <div className="flex flex-col items-center gap-2 text-center">
            <a
              href="#"
              className="flex flex-col items-center gap-2 font-medium"
            >
              <div className="flex size-8 items-center justify-center rounded-md">
                <GalleryVerticalEndIcon className="size-6" />
              </div>
              <span className="sr-only">Acme Inc.</span>
            </a>
            <h1 className="text-xl font-bold">Sistema de Automatización Integrados</h1>
            <FieldDescription>
              La Solución de Informática para su Empresa
            </FieldDescription>
          </div>
          <Field>
            <FieldLabel htmlFor="email">Usuario</FieldLabel>
            <Input
              id="email"
              type="text"
              placeholder="Usuario"
              required
            />
          </Field>
          <Field>
            <FieldLabel htmlFor="password">Contraseña</FieldLabel>
            <Input
              id="password"
              type="password"
              placeholder="contraseña"
              required
            />
          </Field>
          <Field></Field>
          <Field>
            <Button type="submit">{login_message}</Button>
          </Field>
          <FieldSeparator></FieldSeparator>

        </FieldGroup>
      </form>
      <FieldDescription className="px-6 text-center">
        El acceso a este sistema está restringido a personal autorizado
      </FieldDescription>
    </div>
  )
}
