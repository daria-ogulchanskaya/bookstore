export interface User
{
    id: string
    name: string
    surname: string
    username: string
    email: string
    isRemember?: boolean
    isBlocked: boolean
}