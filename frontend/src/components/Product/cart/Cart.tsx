import {
  Box,
  Button,
  Card,
  CardContent,
  Divider,
  FormControl,
  Grid,
  IconButton,
  Typography,
  debounce,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useAppDispatch } from "../../../hooks/useCustomUsersType";
import useCustomTypeSelector from "../../../hooks/useCustomTypeSelector";
import { AddCircle, RemoveCircle } from "@mui/icons-material";
import CircularProgress from "@mui/material/CircularProgress";

import { useEffect, useState } from "react";
import { Colors } from "../../../styles/theme/mainTheme";
import { addToCart, deleteCart, deleteItemFromCart, displayCart } from "../../../redux/reducers/cartReducer";
import { createIncrementalCompilerHost } from "typescript";
import { newOrder } from "../../../types/Product";
import { addOrder } from "../../../redux/reducers/orderReducer";

function Cart() {
  const dispatch = useAppDispatch();
  const cart = useCustomTypeSelector((state) => state.cartReducer);
  const token = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
  const cartItems = cart.cart;
  const isCartLoading = useCustomTypeSelector((state) => state.cartReducer.isCartLoading);
 const [isLoading, setIsLoading] = useState(false);
const navigate = useNavigate();
const handleOrderButtonClick = async () => {
  try {
    await dispatch(addOrder(token));
    await dispatch(deleteCart(token));
    navigate("/Order"); 
  } catch (error) {
    console.log("error occured in deletion of cart");
  }
};
  const manageQuantityInCart = async ( productId:string,quantity:number) => {
    setIsLoading(true); 
    const newOrderr:newOrder = {
      productId: productId,
      quantity: quantity,
    };
    try {
      await dispatch(addToCart({ newOrderr, token }));
      await dispatch(displayCart(token));
    } catch (error) {
      console.error('An error occurred:', error);
    }finally {
      setIsLoading(false); 
    }
   
  }
  return (
    <div>
     {isCartLoading ? (
        <CircularProgress /> 
      ) : (  
      <>
      {cart.error && <p>Error: {cart.error}</p>} 
      {cartItems.length === 0 ? (
            <Typography variant="subtitle1" gutterBottom>
              Your cart is empty. Shop more!
            </Typography>
          ) :(
      <Grid container spacing={2}>
        <Grid item xs={12} sm={8}>
          {cartItems.map((product) => (
       
            <Card sx={{ marginBottom: 2 }} key={product.productId}>
              <Box display="flex" alignItems="center" p={2}>
                {/* <img
                  src={product.imagesIds[0]}
                  alt={product.title}
                  style={{ height: 200,width: 200, objectFit: "cover" }}
                /> */}
                <CardContent>
                  <Typography variant="h6" gutterBottom>
                    {product.productTitle}
                  </Typography>
                  {/* <Typography variant="subtitle1" gutterBottom>
                    Category Name: {product.category.name}
                  </Typography>
                  <Typography variant="subtitle1" gutterBottom>
                    Category ID: {product.category.id}
                  </Typography> */}
                  <Typography variant="subtitle1" gutterBottom>
                   Product Price: {product.productPrice}
                  </Typography>
                  <Typography variant="subtitle1" gutterBottom>
                  Sub-Total{product.productPrice*product.quantity}
                  </Typography>
                  <Box display="flex" alignItems="center">
                    <IconButton  disabled={isCartLoading} 
                      onClick={() => { manageQuantityInCart(product.productId,-1)                       
                      }}
                    >
                      <RemoveCircle />
                    </IconButton>
                    <Typography variant="subtitle1" gutterBottom>
                      {product.quantity}
                    </Typography>
                    <IconButton disabled={isCartLoading} 
                      onClick={() => {manageQuantityInCart(product.productId,1)  
                      }
                      }
                    >
                      <AddCircle />
                    </IconButton>
                  </Box>
                  <Button
                    variant="contained"
                    disabled={isCartLoading} 
                    sx={{backgroundColor:Colors.light_grey,
                    color:Colors.black,   
                     transition: "background-color 0.3s, border 0.3s, color 0.3s",
                    "&:hover": {
                      background: Colors.dim_grey,
                      border: "none",
                      color: Colors.black,
                    },}}

                    onClick={async() => {
                     await dispatch(deleteItemFromCart({ productId: product.productId, token }));      
                    await dispatch(displayCart(token)); 
                    }}
                  >
                    Remove from Cart
                  </Button>
                </CardContent>
              </Box>
              <Divider />
            </Card>
          ))}
        </Grid>
        <Grid item xs={12} sm={4}>
          <Card sx={{ marginBottom: 2 }}>
            <CardContent>
              <Typography variant="h6" gutterBottom>
                Summary
              </Typography>
              <Typography variant="subtitle1" gutterBottom>
                Total Quantity: {cart.totalQuantity}
              </Typography>
              <Typography variant="subtitle1" gutterBottom>
                Total Amount: {cart.total}
              </Typography>
              
              <Button variant="contained" disabled={isCartLoading}  
               onClick={handleOrderButtonClick}
               sx={{backgroundColor:Colors.light_grey,
                    color:Colors.black,   
                     transition: "background-color 0.3s, border 0.3s, color 0.3s",
                    "&:hover": {
                      background: Colors.dim_grey,
                      border: "none",
                      color: Colors.black,
                    },}}>
                {/* <Link to={"/Order"}> Proceed to Checkout</Link> */}
               Proceed to place Order
              </Button>
            </CardContent>
          </Card>
        </Grid>
      </Grid>)}
      </>)};
    </div>
      
  );
}

export default Cart;
