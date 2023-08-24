import { Box, Button, Card, CardContent, Divider, Grid, Typography } from "@mui/material";
import useCustomTypeSelector from "../hooks/useCustomTypeSelector";
import { useAppDispatch } from "../hooks/useCustomUsersType";
import { useEffect } from "react";
import { displayOrder } from "../redux/reducers/orderReducer";

  
  function Cart() {
    const dispatch = useAppDispatch();
    const order = useCustomTypeSelector((state) => state.orderReducer);
    const token = useCustomTypeSelector(
      (state) => state.authReducer.accessToken
    );
   
    const orderItems = order.order;
  
  //const orderId=orderItems[0].orderId;
  const orderId = orderItems.length > 0 ? orderItems[0].orderId : null;

  useEffect(() => {
    dispatch(displayOrder(token));
  }, [dispatch]);

    return (
      <div>
        <Typography variant="subtitle1" gutterBottom>
        Order Details
        </Typography>
        <Typography variant="subtitle1" gutterBottom>
          Order Id :   {orderId}
        </Typography>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={8}>
            {orderItems.map((product) => (
         
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
                      <Typography variant="subtitle1" gutterBottom>
                        {product.quantity}
                      </Typography> 
                    </Box>
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
                 Order Summary
                </Typography>
                <Typography variant="subtitle1" gutterBottom>
                  Total Quantity: {order.totalQuantity}
                </Typography>
                <Typography variant="subtitle1" gutterBottom>
                  Total Amount: {order.total}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      </div>
    );
  }
  
  export default Cart;
  