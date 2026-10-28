import { ipcMain } from "electron";
import { getToken } from "../auth.service";

export default function users() {
    ipcMain.handle('user:getAll', async (_event, data) => {
        try {
            const response = await fetch('http://localhost:5125/api/users', {
                method: "GET",
                headers: {
                    "Content-type": 'application/json',
                    "Authorization": `Bearer ${getToken()}`
                }
            });
            if (!response.ok) {
                const error = await response.json();
                console.log('error al recibir los  datos del serviodr ----> ' + error);
                return false;
            }
            const result = await response.json()
            return result;
        } catch (error) {
            console.log('Error al ejecutar la operacion -----> ' + error);
        }
    })
    ipcMain.handle('user:updateNameRole', async (_event, id, data) => {
        try {
            const response = await fetch(`http://localhost:5125/api/users/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                },
                body: JSON.stringify(data),
            })
            if (!response.ok) {
                const result = await response.json();
                console.log('Error al recibir la consulta ----> ' + result);
                return false
            }
            const result = await response.json();
            return result
        } catch (error) {
            console.log("Error en el try catch user:updateNameRole -----> " + error);
        }
    })

}