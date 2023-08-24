export interface NewProduct{
  title: string;
  price: number;
  description: string;
 // categoryId: string; // Use string for CategoryId since it's a Guid (UUID)
  quantity: number;
 // imagesIds: string[];
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
   // categoryId: string; // Use string for CategoryId since it's a Guid (UUID)
   // images: string[]; // Use the Image interface defined earlier
    quantity: number;
  }
// public class ProductCreateDto
    
// {

//     public string Title { get; set; }
//     public decimal Price { get; set; }
//     public string Description { get; set; }
//     public Guid CategoryId { get; set; } // Assuming this is used to associate the product with a category
//     public int Quantity { get; set; }
//     public List<Image> Images { get; set; }
// }
// public class ProductReadDto
// {
    
//     public string Title { get; set; }
//     public decimal Price { get; set; }
//     public string Description { get; set;}
//      public List<Image> Images { get; set; }
// }
//   public class ProductUpdateDto
// {
    
//     public string Title { get; set; }
//     public decimal Price { get; set; }
//     public string Description { get; set;}  
//     public Guid CategoryId { get; set; }
//      public List<Image> Images { get; set; }
//     public int Quantity { get; set; }
// }