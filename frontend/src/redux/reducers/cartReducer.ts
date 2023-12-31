import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Product, newOrder } from "../../types/Product";
import axios, { AxiosError } from "axios";
import { ProductRead } from "../../types/NewProduct";
import { CartRead } from "../../types/Cart";
import { act } from "@testing-library/react";

interface CartState {
  cart: CartRead[];
  totalQuantity: number;
  total: number;
  loading: boolean;
  isCartLoading: boolean;
  error: string | null;
}

const initialState: CartState = {
  cart: [],
  totalQuantity: 0,
  total: 0,
  loading: false,
  isCartLoading: false,
  error: null,
};

export const displayCart = createAsyncThunk(
  "displayCart",
  async (token: string | null) => {
    try {
      console.log("going to get cart items");
      const response = await axios.get<CartRead[]>(
        "https://shop-and-shop.azurewebsites.net/api/v1/carts/items",
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      return response.data;
    } catch (e) {
      console.log("error displaying cart");
      const error = e as AxiosError;
      throw error;
    }
  }
);

export const deleteItemFromCart = createAsyncThunk(
  "deleteItemFromCart",
  async ({ productId, token }: { productId: string; token: string | null }) => {
    try {
      const orderItemDeleted = await axios.delete<string>(
        `https://shop-and-shop.azurewebsites.net/api/v1/carts/items/${productId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`, // Include the token in the Authorization header
            "Content-Type": "application/json",
          },
        }
      );
      return orderItemDeleted.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const deleteCart = createAsyncThunk(
  "deleteCart",
  async (token: string | null) => {
    try {
      const response = await axios.delete<string>(
        "https://shop-and-shop.azurewebsites.net/api/v1/carts",
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      console.log("delete cart response",response.data);
      return response.data;
    } catch (e) {
      console.log("erro is",e);
      const error = e as AxiosError;
      throw error;
    }
  }
);

export const addToCart = createAsyncThunk(
  "addToCart",
  async ({
    newOrderr,
    token,
  }: {
    newOrderr: newOrder;
    token: string | null;
  }, { rejectWithValue }) => {
    console.log("inside reducer");
    try {
      console.log("product quantity request going", newOrderr.quantity);
      const orderCreated = await axios.patch<string>(
        "https://shop-and-shop.azurewebsites.net/api/v1/carts",
        newOrderr,
        {
          headers: {
            Authorization: `Bearer ${token}`, 
            "Content-Type": "application/json",
          },
        }
      );
      return orderCreated.data;
    } catch (e:any) {
      console.log("error here");
      return rejectWithValue(e.message);
    }
  }
);

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(displayCart.pending, (state) => {
        state.loading = true;
        state.isCartLoading = true; 
      })
      .addCase(displayCart.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || null;
      })
      .addCase(
        displayCart.fulfilled,
        (state, action: PayloadAction<CartRead[]>) => {
          state.cart = action.payload as CartRead[];
          state.isCartLoading = false;
          state.totalQuantity = action.payload.reduce(
            (total, product) => total + product.quantity,
            0
          );
          if (action.payload.length > 0) {
            state.total = action.payload[0]?.totalAmount ?? 0;
          } else state.total = 0;
          state.loading = false;
          state.error = null;
        }
      )
      .addCase(addToCart.pending, (state) => {
        state.loading = true;
      })
      .addCase(
        addToCart.fulfilled,
        (state, action: PayloadAction<boolean | string>) => {
          state.loading = false;
          // if (typeof action.payload === "string") {
          //   state.error = action.payload;     
          // } else {
          //   console.log("actionpayload fulfilled not string");
          // }
          if (!state.error) {
            return state;
          }
        }
      )
      .addCase(addToCart.rejected, (state, action) => {
        state.loading = false; 
        state.error = action.payload as string;
      });
  },
});
const cartReducer = cartSlice.reducer;
export const {} = cartSlice.actions;
export default cartReducer;
function useState<T>(): [any, any] {
  throw new Error("Function not implemented.");
}
