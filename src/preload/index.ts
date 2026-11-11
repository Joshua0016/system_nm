import { contextBridge, ipcRenderer } from 'electron'
import { electronAPI } from '@electron-toolkit/preload'

// Custom APIs for renderer
export const api = {
  auth: {
    login: (dto) => ipcRenderer.invoke('auth:login', dto)
  },
  user: {
    get: (id: number) => ipcRenderer.invoke('user:get', id),
    getAll: () => ipcRenderer.invoke('user:getAll'),
    create: (dto) => ipcRenderer.invoke('user:create', dto),
    updateNameRole: (id: number, dto) => ipcRenderer.invoke('user:updateNameRole', id, dto),
    updatePassword: (id: number, dto) => ipcRenderer.invoke('user:updatePassword', id, dto),
    activate: (id: number) => ipcRenderer.invoke('user:activate', id),
    desactivate: (id: number) => ipcRenderer.invoke('user:desactivate', id)
  }
}
export const CustomApi = typeof api;

// Use `contextBridge` APIs to expose Electron APIs to
// renderer only if context isolation is enabled, otherwise
// just add to the DOM global.
if (process.contextIsolated) {
  try {
    contextBridge.exposeInMainWorld('electron', electronAPI)
    contextBridge.exposeInMainWorld('api', api)
  } catch (error) {
    console.error(error)
  }
} else {
  // @ts-ignore (define in dts)
  window.electron = electronAPI
  // @ts-ignore (define in dts)
  window.api = api
}
