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
// import {
//   decrementQuantityOfIndiviualProduct,
//   incrementQuantityOfIndiviualProduct,
//   removeFromCart,
//   totalCartAmount,
//   totalCartAmountAfterUpdate,
// } from "../../../redux/reducers/cartReducer";
import { useEffect } from "react";
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
  // useEffect(() => {
  //   dispatch(displayCart(token));
  // }, [dispatch, cart]); 
  const cartItems = cart.cart;
const totalCartAmount=0;
  // const getQuantity = (productId: number) => {
  //   const quantityObj = cart.quantityOfIndiviualProduct.find(
  //     (item) => item.id === productId
  //   );
  //   return quantityObj ? quantityObj.quantity : 0;
  // };
//   const handleDeleteItem = (productId:string)=>{
//     dispatch(deleteItemFromCart({productId, token})).then(() => {
//       dispatch(displayCart(token)); 
//   });
// }
const navigate = useNavigate();
  const manageQuantityInCart = ( productId:string,quantity:number) => {
    const newOrderr:newOrder = {
      productId: productId,
      quantity: quantity,
    };
    console.log("going to dispatch add to cart");
    //dispatch(addToCart({newOrderr,token}));
    dispatch(addToCart({ newOrderr, token })).then(() => {
      dispatch(displayCart(token)); 
    });
   
  }
  return (
    <div>
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
                    Price: {product.productPrice*product.quantity}
                  </Typography>
                  <Box display="flex" alignItems="center">
                    <IconButton
                      onClick={() => { manageQuantityInCart(product.productId,-1) 
                       // dispatch(displayCart(token)); 
                      //  dispatch(decrementQuantityOfIndiviualProduct(product.id))
                        
                      }}
                    >
                      <RemoveCircle />
                    </IconButton>
                    <Typography variant="subtitle1" gutterBottom>
                      {product.quantity}
                    </Typography>
                    <IconButton
                      onClick={() => {manageQuantityInCart(product.productId,1)  
                       //  dispatch(displayCart(token)); 
                        // dispatch(incrementQuantityOfIndiviualProduct(product.id))
                        // dispatch(totalCartAmountAfterUpdate());
                      }
                      }
                    >
                      <AddCircle />
                    </IconButton>
                  </Box>
                  <Button
                    variant="contained"
                    sx={{backgroundColor:Colors.light_grey,
                    color:Colors.black,   
                     transition: "background-color 0.3s, border 0.3s, color 0.3s",
                    "&:hover": {
                      background: Colors.dim_grey,
                      border: "none",
                      color: Colors.black,
                    },}}

                    onClick={() => {
                      dispatch(deleteItemFromCart({ productId: product.productId, token }))
                      .then(() => {
                        dispatch(displayCart(token));
                      });
                    }}
                      //  handleDeleteItem(product.productId)}
                      
                    
                      // dispatch(totalCartAmountAfterUpdate());
                    
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
              
              <Button variant="contained"  onClick={async() => {
                      // dispatch(addOrder( token )).then(() => {
                      //   dispatch(deleteCart(token));
                      await dispatch(addOrder(token));
                      await dispatch(deleteCart(token));
                      navigate("/Order");
                    //  });
                    }
                  }
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
      </Grid>
    </div>
  );
}

export default Cart;
