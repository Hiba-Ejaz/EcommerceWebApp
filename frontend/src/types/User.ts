export interface User {
    id: number;
    email: string;
    password: string;
    name: string;
    role: "Customer" | "Admin";
    avatar: string;
  }
  export interface UserRead {
    id: string;
    email: string;
    name: string;
    avatar: string;
    role: Role; 
  }
  
  type Role = "Admin" | "User"; 
