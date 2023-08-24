import { Category } from "./Category";

export interface Product {
    id: number;
    title: string;
    price: number;
    description: string;
    //category: Category;
    //imagesIds: string[];
  }
  export interface newOrder {
   productId : string;
   quantity : number;
  }
  