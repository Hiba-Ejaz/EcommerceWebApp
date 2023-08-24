import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import axios from "axios";
import { User } from "../../types/User";
import { CreateUser, ReadUser } from "../../types/UserCreate";

interface AuthState {
  user:ReadUser;
  accessToken: string | null;
}
const initialState: AuthState = {
  user: {
    name: "",
    role: "Customer",
    avatar: "",
    email: "",
  },
  accessToken: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<ReadUser>) => {
      state.user = action.payload;
    },
    setAccessToken: (state, action: PayloadAction<string | null>) => {
      state.accessToken = action.payload;
    },
    logout: (state) => {
      return (state = initialState);
    },
  },
});

export const { setUser, setAccessToken, logout } = authSlice.actions;

export const authReducer = authSlice.reducer;
