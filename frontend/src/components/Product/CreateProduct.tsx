import React, { FormEvent, useState } from "react";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import { createNewProduct } from "../../redux/reducers/productsReducer";
import {
  Box,
  Button,
  TextField,
  Typography,
  keyframes,
  styled,
} from "@mui/material";
import { ImageType, NewProduct } from "../../types/NewProduct";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { Category } from "@mui/icons-material";
import { Colors } from "../../styles/theme/mainTheme";
import { uploadFile } from "../../redux/common";
import { PayloadAction } from "@reduxjs/toolkit";
import { Console } from "console";
type AddProductType = {
  title: string;
  price: number;
  description: string;
  categoryId: number;
  images: Array<string>;
};
function CreateProduct() {
  const [CreateNew, setCreateNew] = useState(false);
  const [created, setCreated] = useState(false);
  const productCreated = useCustomTypeSelector(
    (state) => state.productsReducer.productCreated
  );
  const [productData, setProductData] = useState<NewProduct>({
    title: "",
    price: 0,
    description: "",
   // categoryId: "",
    quantity:0,
    //imagesIds:[],
  });
  const slideAnimation = keyframes`
from {
  transform: translateY(-100%);
}
to {
  transform: translateY(0);
}
`;
  const AnimatedBox = styled(Box)`
    animation: ${slideAnimation} 0.5s;
  `;
  const dispatch = useAppDispatch();
  const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    if (name === "price" || name === "categoryId") {
      setProductData((prevData) => ({
        ...prevData,
        [name]: Number(value),
      }));
    } else if (name === "images") {
      const files = event.target.files;
      if (files) {
        const imageIds: string[] = [];
      for (let i = 0; i < files.length && i<=2 ; i++) {
        const file = files[i];
        console.log("going to the common function");
        
        const response=  await dispatch(uploadFile(file)) as PayloadAction<{imagesId:string}>;
        const id = response.payload.imagesId;
        imageIds.push(id);
      }
      
      setProductData((prevState) => ({
        ...prevState,
        [name]: imageIds,
      }));
    } }
    else {
      setProductData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };
  function createImageObject(location: string): ImageType {
    return {
      link: location,
      productId: 0, // You need to set the actual product ID here if you have it
    };
  }
  const handleSubmit = (event: FormEvent) => {
    event.preventDefault();
    setCreateNew(false);
    const { title } = productData;
    dispatch(createNewProduct(productData));
    // if(products.find((item)=>item.id===productData.))
  };
  return (
    <div>
      <div>Create Product</div>
      {(!productCreated || CreateNew) && (
        <form onSubmit={handleSubmit}>
          <TextField
            label="Title"
            name="title"
            value={productData.title}
            onChange={handleChange}
            fullWidth
          />
          <TextField
            label="Price"
            name="price"
            value={productData.price || ""}
            onChange={handleChange}
            fullWidth
          />
          <TextField
            label="Description"
            name="description"
            value={productData.description}
            onChange={handleChange}
            multiline
            rows={4}
            fullWidth
          />
          <TextField
            label="Category ID"
            name="categoryId"
            type="number"
           // value={productData.categoryId}
            onChange={handleChange}
            fullWidth
          />
          <label>Add product images (it will take only first 3 images)</label>
          <input 
        type="file"
        name="images"
        onChange={handleChange}
        multiple
      />
          <Button type="submit" variant="contained" color="primary">
            Add Product
          </Button>
        </form>
      )}
      {productCreated && !CreateNew && (
        <AnimatedBox>
          <Box
            sx={{
              display: "flex",
              fontFamily: "Raleway, Arial",
              justifyContent: "center",
              alignContent: "center",
              margin: "3em",
              padding: "2em",
              boxShadow: "2",
              borderRadius: "2",
              backgroundColor: Colors.primary,
            }}
          >
            <Typography variant="h3">
              PRODUCT HAS BEEN SUCCESSFULLY CREATED
            </Typography>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              onClick={() => setCreateNew(true)}
            >
              Add Another Product
            </Button>
          </Box>
        </AnimatedBox>
      )}
    </div>
  );
}
export default CreateProduct;