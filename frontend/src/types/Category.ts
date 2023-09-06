export interface Category {
    id: number;
    name: string;
    image:  Array<string>;
    creationAt: string,
    updatedAt: string
  };
  export interface CategoryCreate {
    name: string;
  };
  export interface ReadCategory {
    id:string;
    name: string;
  };