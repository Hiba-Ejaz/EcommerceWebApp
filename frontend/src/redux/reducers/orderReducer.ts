import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";
import { OrderRead, OrderWithDetailsRead } from "../../types/Order";

interface OrderState {
  orderList: OrderWithDetailsRead[];
  order: OrderRead[];
  totalQuantity: number;
  total: number;
  loading: boolean;
  error: string | null;
}

const initialState: OrderState = {
  orderList: [],
  order: [],
  totalQuantity: 0,
  total: 0,
  loading: false,
  error: null,
};

export const addOrder = createAsyncThunk(
  "addOrder",
  async (token: string | null) => {
    try {
      const orderCreated = await axios.patch<string>(
        "https://shop-and-shop.azurewebsites.net/api/v1/order",
        null,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      console.log("order data", orderCreated.data);
      return orderCreated.data;
    } catch (e) {}
  }
);
export const displayOrder = createAsyncThunk(
  "displayOrder",
  async (token: string | null) => {
    try {
      console.log("going to get order items");
      const response = await axios.get<OrderRead[]>(
        "https://shop-and-shop.azurewebsites.net/api/v1/order/my-orders",
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      console.log("order response", response.data);
      return response.data;
    } catch (e) {
      console.log("error in display my order",e);
      const error = e as AxiosError;
      throw error;
    }
  }
);
export const fetchAllOrders = createAsyncThunk("fetchAllOrders", async (token: string | null) => {
  try {
    console.log("going to get order items");
    const response = await axios.get<OrderWithDetailsRead[]>(
      "https://shop-and-shop.azurewebsites.net/api/v1/admin/orders",
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );
    console.log("order with details response", response.data);
    return response.data;
  } catch (e) {
    console.log("error", e); 
    const error = e as AxiosError;
    throw error;
  }
});

export const orderSlice = createSlice({
  name: "order",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(displayOrder.pending, (state) => {
        state.loading = true;
      })
      .addCase(displayOrder.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || null;
      })
      .addCase(
        displayOrder.fulfilled,
        (state, action: PayloadAction<OrderRead[]>) => {
          state.order = action.payload as OrderRead[];
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
      .addCase(fetchAllOrders.pending, (state) => {
        state.loading = true;
        state.error = "it is loading";
      })
      .addCase(fetchAllOrders.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || null;
      })
      .addCase(
        fetchAllOrders.fulfilled,
        (state, action: PayloadAction<OrderWithDetailsRead[]>) => {
          state.orderList = action.payload as OrderWithDetailsRead[];
          state.loading = false;
          state.error = null;
        }
      );
  },
});
const orderReducer = orderSlice.reducer;
export default orderReducer;
