import axios, { AxiosError } from "axios";
import { CategoryCreate, ReadCategory } from "../../types/Category";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

interface CategoryReducer {
    loading: boolean;
    error: string;
    categories: ReadCategory[];
  }
  const initialState: CategoryReducer = {
    loading: false,
    error: "",
    categories: [],
  };

  export const createNewCategory = createAsyncThunk(
    "createNewCategory",
    async (payload: {category: CategoryCreate ; token: string | null}) => {
        const { category, token } = payload;
      try {
        const result = await axios.post<ReadCategory>(
        //   "https://shop-and-shop.azurewebsites.net/api/v1/categories/",
        "http://localhost:3000/api/v1/categories",
          payload.category,
          {
            headers: {
              Authorization: `Bearer ${payload.token}`,
              "Content-Type": "application/json",
            },
          }
        );
        console.log(result.data,"categoryData");
        return result.data;
      } catch (e) {
        const error = e as AxiosError;
        return error.message;
      }
    }
  );
  export const categorySlice = createSlice({
    name: "categories",
    initialState,
    reducers: {
    },
    extraReducers: (build) => {
      build
      .addCase(createNewCategory.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(createNewCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = "cannot perform this action";
      })
      .addCase(createNewCategory.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
            state.categories.push(action.payload);
        }}
      )}});
      const categoryReducer = categorySlice.reducer;
      export default categoryReducer;