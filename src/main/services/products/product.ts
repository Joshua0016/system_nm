import axios from "axios";
import { ipcMain } from "electron";
import { getToken } from "../auth.service";

export default function products() {
    ipcMain.handle('product:create', async (_event, data) => {
        try {
            const response = axios.post('http://localhost:5125/api/products', data, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                }
            });
            return ((await response).data);

        } catch (error) {
            console.log('Error main process: create -----> ' + error);
            return false;
        }

    })
    ipcMain.handle('product:getCode', async (_event, data) => {
        try {
            const response = axios.get(`http://localhost:5125/api/products/by-code/${data}`, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                }

            });
            return (await response).data
        } catch (error) {
            console.log('Error main:process---->' + error);
        }

    })
    ipcMain.handle('product:update', async (_event, id, data) => {
        try {
            const result = axios.put(`http://localhost:5125/api/products/${id}`, data, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                }
            });

            return (await result).data
        } catch (error) {

            console.log('Error catch main:process3 -----> ' + error);

        }
    })
    ipcMain.handle('product:get', async (_event, dto) => {
        try {
            const result = axios.get('http://localhost:5125/api/products/', {
                params: {
                    search: dto
                },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${getToken()}`
                }
            });
            return (await result).data;
        } catch (error) {
            console.log('Error main:process---->' + error);
        }
    })
}