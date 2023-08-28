import axios, { AxiosError } from "axios";
import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import {
  CreateProduct,
  ProductRead,
  ProductUpdate,
  searchQueryOptions,
} from "../../types/NewProduct";

interface ProductReducer {
  loading: boolean;
  error: string;
  products: ProductRead[];
  productbyId: ProductRead;
  currentProductForUpdate: ProductUpdate;
  currentProductIdForUpdate: string;
  singleProductDetail: ProductRead;
  productCreated: boolean;
  productDeleted: boolean;
}

const initialState: ProductReducer = {
  loading: false,
  error: "",
  products: [],
  currentProductForUpdate: {} as ProductUpdate,
  currentProductIdForUpdate: "",
  productbyId: {} as ProductRead,
  singleProductDetail: {} as ProductRead,
  productCreated: false,
  productDeleted: false,
};

export const fetchAllProducts = createAsyncThunk(
  "fetchAllProducts",

  async () => {
    try {
      console.log("came here to fetch");
      const result = await axios.get<ProductRead[]>(
        "https://shop-and-shop.azurewebsites.net/api/v1/products/"
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const getProductById = createAsyncThunk(
  "getProductById",
  async (productId: string) => {
    try {
      const result = await axios.get<ProductRead[]>(
        `https://shop-and-shop.azurewebsites.net/api/v1/products/${productId}`
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const createNewProduct = createAsyncThunk(
  "createNewProduct",
  async (payload: {product: CreateProduct ; token: string | null}) => {
    const updatedPayload = {
      product: {
        ...payload.product,
         quantity: payload.product.quantity,
      },
      token: payload.token,
    };
    try {
      const result = await axios.post<ProductRead>(
        "https://shop-and-shop.azurewebsites.net/api/v1/products/",
        updatedPayload.product,
        {
          headers: {
            Authorization: `Bearer ${updatedPayload.token}`,
            "Content-Type": "application/json",
          },
        }
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const getAllProducts = createAsyncThunk(
  "getAllProducts",
  async (searchQuery: searchQueryOptions) => {
    try {
      console.log("going to fetch producccts");
      const queryParams = {
        SearchQuery: searchQuery.searchQuery,
        SortBy: searchQuery.sortBy,
        SortAscending: searchQuery.sortAscending,
        PageNumber: searchQuery.pageNumber,
        PageSize: searchQuery.pageSize,
      };
      const result = await axios.get<ProductRead>(
        "https://shop-and-shop.azurewebsites.net/api/v1/products/"
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const updateProduct = createAsyncThunk(
  "updateProduct",
  async (payload: { id: string; updateProductData: ProductUpdate; token: string | null}) => {
    const { id, updateProductData,token } = payload;
    try {
      const result = await axios.patch<ProductUpdate>(
        `https://shop-and-shop.azurewebsites.net/api/v1/products/${id}`,
        updateProductData,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const getProductForUpdate = createAsyncThunk(
  "getProductForUpdate",
  async (productId: string) => {
    try {
      console.log("product coming", productId);
      const result = await axios.get<ProductUpdate>(
        `https://shop-and-shop.azurewebsites.net/api/v1/products/${productId}/update`
        
      );
      console.log("product for update is", result.data);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const deleteProduct = createAsyncThunk(
  "deleteProduct",
  async (payload:{productId: string; token: string | null}) => {
    const { productId,token } = payload;
    try {
      const result = await axios.delete<boolean>(
        `https://shop-and-shop.azurewebsites.net/api/v1/products/${productId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      console.log("result after deletion", result.data);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const productsSlice = createSlice({
  name: "products",
  initialState,
  reducers: {
    setProductIdForUpdate: (state, action: PayloadAction<string>) => {
      state.currentProductIdForUpdate = action.payload;
    },
  },
  extraReducers: (build) => {
    build
      .addCase(fetchAllProducts.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchAllProducts.rejected, (state, action) => {
        state.loading = false;
        state.error = "cannot perform this action";
      })
      .addCase(fetchAllProducts.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.products = action.payload;
        }
      })
      .addCase(createNewProduct.fulfilled, (state, action) => {
        state.productCreated = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.productCreated = true;
          state.products.push(action.payload);
        }
      })
      .addCase(deleteProduct.fulfilled, (state, action) => {
        state.productDeleted = false;
        if (action.payload === false) {
          state.error = "product not deleted";
        } else {
          state.productDeleted = true;
        }
      })
      .addCase(getProductById.fulfilled, (state, action) => {
        state.productDeleted = false;
        if (
          typeof action.payload === "object" &&
          "id" in action.payload &&
          "title" in action.payload &&
          "price" in action.payload &&
          "description" in action.payload
        ) {
          state.error = "product retrieved by id";
        } else {
          state.error = "product not retrieved by id";
        }
      })
      .addCase(getProductForUpdate.fulfilled, (state, action) => {
        if (
          typeof action.payload === "object" &&
          "quantity" in action.payload &&
          "title" in action.payload &&
          "price" in action.payload &&
          "description" in action.payload
        ) {
          state.error = "product retrieved by id";
          state.currentProductForUpdate = action.payload;
          console.log("product for update is", state.currentProductForUpdate);
        } else {
          state.error = "product not retrieved by id";
        }
      })
      .addCase(updateProduct.fulfilled, (state, action) => {
        if (
          typeof action.payload === "object" &&
          "quantity" in action.payload &&
          "title" in action.payload &&
          "price" in action.payload &&
          "description" in action.payload
        ) {
          state.error = "product updated";
        } else {
          state.error = "product not retrieved by id";
        }
      });
  },
});
const productsReducer = productsSlice.reducer;
export const { setProductIdForUpdate } = productsSlice.actions;
export default productsReducer;
function useState<T>(): [any, any] {
  throw new Error("Function not implemented.");
}
