import { User } from "./User"

export interface UserUpdate{
    email: string;
    password: string;
    name: string;
    role: "customer" | "Admin";
    avatar: string;
}