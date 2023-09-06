import {
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { Fragment, useEffect, useState } from "react";
import { fetchAllOrders } from "../../redux/reducers/orderReducer";
import { useAppDispatch } from "../../hooks/useCustomUsersType";

function Orders() {
  const dispatch = useAppDispatch();
  const token = useCustomTypeSelector((state) => state.authReducer.accessToken);
  // const [retryCount, setRetryCount] = useState(1);
  // useEffect(() => {
  //   if (retryCount === 0) {
  //     let hasDispatched = false; 
  //     const fetchData = async () => {
  //       try {
  //         if (!hasDispatched) {
  //           hasDispatched = true; 
  //           console.log("going to dispatch display all orders");
  //           await dispatch(fetchAllOrders(token));
  //         }
  //       } catch (error) {
  //         console.error("Error fetching orders:", error);
  //         setRetryCount(retryCount + 1);
  //       }
  //     };
  //     fetchData();
  //   }
  // }, [retryCount]);
  
  const orderList = useCustomTypeSelector(
    (state) => state.orderReducer.orderList
  );

  return  (
    <Container>
      <Typography variant="h4" gutterBottom>
        Admin Dashboard
      </Typography>
      <div>
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Order ID</TableCell>
                <TableCell>Customer Name</TableCell>
                <TableCell>Customer Email</TableCell>
                <TableCell>Total Amount</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {orderList.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={4}>No orders available</TableCell>
                </TableRow>
              ) : (
                orderList.map((order) => (
                  <Fragment key={order.orderId}>
                    <TableRow>
                      <TableCell>{order.orderId}</TableCell>
                      <TableCell>{order.name}</TableCell>
                      <TableCell>{order.email}</TableCell>
                      <TableCell>{order.totalAmount}</TableCell>
                    </TableRow>
                    <TableRow>
                      <TableCell colSpan={4}>
                        <Table>
                          <TableHead>
                            <TableRow>
                              <TableCell>Product ID</TableCell>
                              <TableCell>Product Title</TableCell>
                              <TableCell align="right">
                                Product Price
                              </TableCell>
                            </TableRow>
                          </TableHead>
                          <TableBody>
                             {order.orderItems.map((orderItem) => (
                              <TableRow key={orderItem.productId}>
                                <TableCell>{orderItem.productId}</TableCell>
                                <TableCell>{orderItem.productTitle}</TableCell>
                                <TableCell align="right">
                                  {orderItem.productPrice}
                                </TableCell>
                                <TableCell align="right">
                                  {orderItem.quantity}
                                </TableCell>
                              </TableRow>
                            ))} 
                          </TableBody>
                        </Table>
                      </TableCell>
                    </TableRow>
                  </Fragment>
                ))
              )}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </Container>
  );
}
    
  
export default Orders;
