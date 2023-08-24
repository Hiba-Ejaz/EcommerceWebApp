 import { Product } from '../types/Product';
 import { globalType } from '../redux/store';
 import { useSelector } from 'react-redux';
 import { TypedUseSelectorHook } from 'react-redux';

 const useCustomTypeSelector: TypedUseSelectorHook<globalType> = useSelector;

const useDataFilter = (categoryId: number): any => {
  //const products = useCustomTypeSelector((state) => state.filterProductsReducer);
   //const filteredData = products.filter((product: Product) => product.category.id === categoryId);
   //return filteredData;
 };

 export default useDataFilter;
