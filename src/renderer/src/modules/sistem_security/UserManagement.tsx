
import { useEffect, useState } from 'react'
import DataTable, { TableColumn, type TableStyles } from 'react-data-table-component'
import { Plus, Pencil } from 'lucide-react';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuGroup,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Button } from "@/components/ui/button"

import UserIdentityForm from './forms/UserIdentityForm';
import PasswordForm from './forms/PasswordForm';
import CreateNewUser from './forms/CreateNewUser';

interface User {
    username: string,
    fullName: string,
    role: string,
    isActive: boolean
}
export default function UserManagement() {
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState({});
    const [identityForm, setIdentityForm] = useState(false);
    const [passwordForm, setPasswordForm] = useState(false);
    const [newUserForm, setNewUserForm] = useState(false);

    useEffect(() => {
        async function loadUser() {
            try {
                const data = await window.api.user.getAll();
                if (data) {
                    setUsers(data);
                }
            } catch (error) {
                console.log('Error al realizar la petición --->' + error);
            }
        }
        loadUser();
    }, []);


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


    const columns: TableColumn<User>[] = [
        { name: 'Nombre usuario', selector: (row) => row.username, sortable: true },
        { name: 'Nombre completo', selector: (row) => row.fullName, sortable: true },
        { name: 'Rol', selector: (row) => row.role, sortable: true },
        {
            name: 'Editar',
            cell: (row) => (
                <DropdownMenu>
                    <DropdownMenuTrigger>
                        <Button className={'cursor-pointer'}>
                            <Pencil className='w-5'></Pencil>
                        </Button>
                    </DropdownMenuTrigger>

                    <DropdownMenuContent>
                        <DropdownMenuGroup>
                            <DropdownMenuItem onClick={() => { setSelectedUser(row), setIdentityForm(true) }} className={'cursor-pointer'}>Editar usuario</DropdownMenuItem>
                            <DropdownMenuItem onClick={() => { setSelectedUser(row), setPasswordForm(true) }} className={'cursor-pointer'}>Editar contraseña</DropdownMenuItem>
                        </DropdownMenuGroup>
                    </DropdownMenuContent>
                </DropdownMenu>
            )
        }

    ]
    return (
        <>

            <div className='min-h-screen flex justify-center items-center'>
                <div className='w-[70%] p-3.5'>
                    <div className='flex m-1'>
                        <div className='ml-auto'>
                            <Button variant="outline" className={'cursor-pointer'} onClick={() => setNewUserForm(true)}><Plus className='w-5' /></Button>
                        </div>

                    </div>
                    <DataTable
                        columns={columns}
                        data={users}
                        pagination
                        paginationPerPage={3}
                        paginationRowsPerPageOptions={[3, 5]}
                        customStyles={customStyles}
                        highlightOnHover
                    ></DataTable>
                </div>
            </div>
            {identityForm && <UserIdentityForm setIdentityForm={setIdentityForm} selectedUser={selectedUser} setUpdateUsers={setUsers}></UserIdentityForm >}
            {passwordForm && <PasswordForm setPasswordForm={setPasswordForm} selectedUser={selectedUser}></PasswordForm>}
            {newUserForm && <CreateNewUser setNewUserForm={setNewUserForm} setUpdateUser={setUsers}></CreateNewUser>}

        </>
    )
}