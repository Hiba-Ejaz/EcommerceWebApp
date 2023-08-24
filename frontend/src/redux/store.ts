import { configureStore } from "@reduxjs/toolkit";
import productsReducer from "./reducers/productsReducer";
import usersReducer from "./reducers/usersReducer";
import cartReducer from "./reducers/cartReducer";
import  { authReducer } from "./reducers/authReducer";
import orderReducer from "./reducers/orderReducer";

const store = configureStore({
  reducer: { productsReducer, usersReducer, cartReducer, authReducer, orderReducer },
});
export type globalType = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
