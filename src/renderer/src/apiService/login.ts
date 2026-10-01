interface LoginDto {
    username: string,
    password_hash: string
}
export default async function login(dto: LoginDto): Promise<boolean> {

    return true
}