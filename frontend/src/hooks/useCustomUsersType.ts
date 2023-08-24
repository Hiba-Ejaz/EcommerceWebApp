import { useDispatch } from "react-redux";
import { AppDispatch,  globalType } from "../redux/store";

export const useAppDispatch: () => AppDispatch = useDispatch
