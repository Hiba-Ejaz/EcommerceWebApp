import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, {  AxiosError } from "axios";
import {  UserRead } from "../../types/User";
import { CreateUser } from "../../types/UserCreate";

interface usersReducer {
  loading: boolean;
  error: string | null;
  usersList: UserRead[];
  users: CreateUser[]; //backedn cahna
  userCreated: boolean;
}

const initialState: usersReducer = {
  loading: false,
  error: "",
  usersList: [],
  users: [],
  userCreated: false,
};
const API_URL = "http://localhost:5145/api/v1/users"; //doing for backend trial
export const createNewUser = createAsyncThunk(
  "createNewUser",
  async (userData: CreateUser) => {
    try {
      const userCreated = await axios.post<CreateUser>(API_URL, userData);
      return userCreated.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const deleteUser = createAsyncThunk(
  "deleteUser",
  async (userId: string) => {
    try {
      const result = await axios.delete<string>(
        `http://localhost:5145/api/v1/users/${userId}`
      );
      console.log("result after deletion", result.data);
      return result.data;
    } catch (e) {
      const error = e as AxiosError;
      return error.message;
    }
  }
);

export const updateUserPassword = createAsyncThunk(
  "updateUserPassword",
  async ({
    newPassword,
    token,
  }: {
    newPassword: string;
    token: string | null;
  }) => {
    console.log("inside reducer");
    try {
      console.log("token is", token);
      const userCreated = await axios.patch<boolean>(
        "http://localhost:5145/api/v1/users/updatepassword",
        newPassword,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );
      console.log("user data", userCreated.data);
      return userCreated.data;
    } catch (e) {
      console.log("error");
      const error = e as AxiosError;
      return error.message;
    }
  }
);
export const fetchAllUsers = createAsyncThunk("fetchAllUsers", async () => {
  try {
    const usersList = await axios.get<UserRead[]>( //backend change
      "http://localhost:5145/api/v1/users"
    );
    console.log(usersList.data);
    return usersList.data;
  } catch (e) {
    const error = e as AxiosError;
    return error.message;
  }
});
export const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(createNewUser.pending, (state) => {
        state.loading = true;
        state.error = null;
        console.log("PENDING MAIN");
      })
      .addCase(
        createNewUser.fulfilled,
        (state, action: PayloadAction<CreateUser | string>) => {
          //backend
          if (typeof action.payload === "string") {
            state.error = action.payload;
            console.log(state.error);
          } else {
            console.log("fulfilled main");
            state.users.push(action.payload);
            state.userCreated = true;
          }
          state.loading = false;
        }
      )
      .addCase(createNewUser.rejected, (state, action) => {
        console.log("rejected case");
        state.loading = false;
        state.error = action.error.message || "Failed to create user";
      })
      .addCase(fetchAllUsers.pending, (state, action) => {
        state.loading = true;
      })
      .addCase(fetchAllUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = "cannot perform this action";
      })
      .addCase(fetchAllUsers.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        } else {
          state.usersList = action.payload;
          console.log("user list state", state.usersList);
        }
      });
  },
});

const usersReducer = usersSlice.reducer;
export default usersReducer;
