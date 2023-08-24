import React, { useEffect, useState } from "react";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import { Product } from "../../types/Product";
import axios from "axios";
//import { totalCartAmountAfterUpdate, updateCartItem } from "../../redux/reducers/cartReducer";
import { Box, Button, TextField, Typography, keyframes, styled } from "@mui/material";
import { Colors } from "../../styles/theme/mainTheme";
import { CustomisedForm } from "../../styles/Form";
import { ProductUpdate } from "../../types/NewProduct";
import { fetchAllProducts, updateProduct } from "../../redux/reducers/productsReducer";

type AddUpdateProductType = {
  title: string;
  price: number;
  description: string;
  quantity:number;
  //categoryId: number;
  //images: Array<string>;
};
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

function UpdateProduct() {
  const dispatch = useAppDispatch();
  const [updated, setUpdated] = useState(false);
  const productForUpdate = useCustomTypeSelector(
    (state) => state.productsReducer.currentProductForUpdate
  );
  const productIdForUpdate = useCustomTypeSelector(
    (state) => state.productsReducer.currentProductIdForUpdate
  );
  console.log("productIdForUpdate in update Product",productForUpdate);
  const token = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );

   const [productData, setProductData] = useState<AddUpdateProductType>
  ({
    title: productForUpdate?.title,
    price: productForUpdate?.price,
    description: productForUpdate?.description,
    quantity:productForUpdate.quantity,
    // categoryId: productFOrUpdate?.category.id || 0,
    //categoryId:  0,
    //images: productFOrUpdate?.images,
  });
  useEffect(() => {
    setProductData({
      title: productForUpdate?.title,
      price: productForUpdate?.price,
      description: productForUpdate?.description,
      quantity: productForUpdate?.quantity || 0,
    });
  }, [productForUpdate]);

  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = event.target;
    if (name === "price" || name === "categoryId") {
      setProductData((prevData) => ({
        ...prevData,
        [name]: Number(value),
      }));
    } else if (name === "images") {
      const imagesArray = value.split(",");
      setProductData((prevData) => ({ ...prevData, [name]: imagesArray }));
    } else {
      setProductData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    dispatch(updateProduct({ id: productIdForUpdate, updateProductData: productData, token: token })).then((resultAction)=> {
      if (updateProduct.fulfilled.match(resultAction)) {
        console.log('Product updated successful:', resultAction.payload);
        dispatch(fetchAllProducts());
        setUpdated(true);
      } else if (updateProduct.rejected.match(resultAction)) {
        console.error('Product deletion failed:', resultAction.error.message);
      }})
  };
  return (
    <>
        <Box>
          {!updated && (<CustomisedForm  onSubmit={handleSubmit}>
        <TextField
          label="Title"
          name="title"
          value={productData.title}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
                <TextField
          label="Price"
          name="price"
          value={productData.price}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
            <TextField
          label="description"
          name="description"
          value={productData.description}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
              {/* <TextField
                label="Category ID:"
                type="number"
                name="categoryId"
                value={productData.categoryId}
                onChange={handleInputChange}
              /> */}
              <br/>
              {/* <TextField
                type="text"
                label="Images"
                name="images"
                value={productData.images.join(", ")}
                onChange={handleInputChange}
              /> */}
              <Box>
          <Button type="submit" variant="outlined" sx={{width:"100%",background: Colors.black, color:Colors.dim_grey, border:"none",
      transition: "background-color 0.3s, border 0.3s, color 0.3s",
      "&:hover": {
        background: Colors.dim_grey,
        border: "none",
        color: Colors.black,
      },}} >
          Update Product
        </Button>
        </Box>
      </CustomisedForm >)}
        </Box>
      {updated &&
        <AnimatedBox>
          <Box sx={{ display: "flex", fontFamily: 'Raleway, Arial', justifyContent: "center", alignContent: "center", margin: "3em", padding: "2em", boxShadow: "2", borderRadius: "2", backgroundColor: Colors.primary }}>
            <Typography variant='h3'>
              PRODUCT HAS BEEN UPDATED
            </Typography>
          </Box>
        </AnimatedBox>
      }
    </>
  );
}

export default UpdateProduct;
