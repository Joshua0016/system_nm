import { ipcMain } from "electron"
interface LoginDto {
    username: string,
    password: string
}
let userToken: string | null = null

export function getToken() {
    return userToken;
}

export default function login() {
    ipcMain.handle("auth:login", async (_event, dto: LoginDto) => {
        try {
            let response = await fetch('http://localhost:5125/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(dto),
            });
            if (!response.ok) {
                let error = await response.json();
                console.log("Error al validar los datos ---> " + error);
                return false;
            }
            const result = await response.json()
            userToken = result.token;
            return result;

        } catch (error) {
            console.log('Error en el trycatch ---> ' + error);
            return false
        }
    })
}


