export interface CreateUser{
 
    email: string;
    password: string;
    name: string;
    //role?: "customer" | "admin";
    avatar: string;
  }
  export interface ReadUser{
  
     email: string;
     //password: string;
     name: string;
     role?: "Customer" | "Admin";
     avatar: string;
   }