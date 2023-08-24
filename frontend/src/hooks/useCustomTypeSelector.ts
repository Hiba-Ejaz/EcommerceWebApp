import { TypedUseSelectorHook, useSelector } from "react-redux";

import { globalType } from "../redux/store";

const useCustomTypeSelector:TypedUseSelectorHook<globalType>=useSelector;

export default useCustomTypeSelector;