export interface NewProduct{
  title: string;
  price: number;
  description: string;
 // categoryId: string; // Use string for CategoryId since it's a Guid (UUID)
  quantity: number;
  images: string[];
}
export interface searchQueryOptions {
  searchQuery: string;      // Corresponds to SearchQuery property
  sortBy: Date;  // Corresponds to SortBy property
  sortAscending: boolean,  // Corresponds to SortAscending property
  pageNumber: number,        // Corresponds to PageNumber property
  pageSize: number          // Corresponds to PageSize property
};
export interface CreateProduct {
    title: string;
    price: number;
    description: string;
    //categoryId: string; // Use string for CategoryId since it's a Guid (UUID)
    quantity: number;
    //imagesIds: string[];
  }
  export interface ProductRead {
    id:string;
    title: string;
    price: number;
    description: string;
    images: string[];
    //imagesIds: string[]; // Use the Image interface defined earlier
  }
  export interface ImageType {
    link: string; // Match the property name in the C# class
    productId: number; // Match the property name in the C# class
  }
  export interface ProductUpdate {
    title: string;
    price: number;
    description: string;
    images: string[];
   // categoryId: string; // Use string for CategoryId since it's a Guid (UUID)
   // images: string[]; // Use the Image interface defined earlier
    quantity: number;
  }

