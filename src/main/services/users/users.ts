import { ipcMain } from "electron";
import { getToken } from "../auth.service";
import axios from 'axios'
export default function users() {
    ipcMain.handle('user:getAll', async (_event, data) => {
        try {
            const response = axios.get('http://localhost:5125/api/users', {
                headers: {
                    'Content-type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                }
            })
            const result = (await response).data;
            return result
        } catch (error) {
            console.log('Error al ejecutar la operacion -----> ' + error);
        }
    })
    ipcMain.handle('user:updateNameRole', async (_event, id, data) => {
        try {
            const response = axios.put(`http://localhost:5125/api/users/${id}`, data, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                },
            });
            const result = (await response).data;
            return result;
        } catch (error) {
            console.log("Error en el try catch user:updateNameRole -----> " + error);
        }
    })
    ipcMain.handle('user:updatePassword', async (_event, id, data) => {
        try {
            axios.put(`http://localhost:5125/api/users/${id}/password`, data, {
                headers: {
                    'Content-Type': "application/json",
                    'Authorization': `Bearer ${getToken()}`
                }
            });

        } catch (error) {
            console.log('Error catch, main process user, -----> ' + error);
        }
    })

}