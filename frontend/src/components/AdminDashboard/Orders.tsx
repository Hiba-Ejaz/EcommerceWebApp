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
  import { Fragment } from "react";
  
  function Orders() {
    const orderList = useCustomTypeSelector(
      (state) => state.orderReducer.orderList
    );
  
    return (
      <Container>
        <Typography variant="h4" gutterBottom>
          Admin Dashboard
        </Typography>
        <div>
          <TableContainer component={Paper}>
            <Table>  
              <TableHead>
                <TableRow>
                  <TableCell >Order ID</TableCell>
                  <TableCell  >Customer Name</TableCell>
                  <TableCell  >Customer Email</TableCell>
                  <TableCell >Total Amount</TableCell>
                </TableRow>
              </TableHead>        
              <TableBody>
                {orderList.map((order) => (
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
                              <TableCell align="right">Product Price</TableCell> {/* Align right */}
                              <TableCell align="right">Quantity</TableCell> {/* Align right */}
                            </TableRow>
                          </TableHead>
                          <TableBody>
                            {order.orderItems.map((orderItem) => (
                              <TableRow key={orderItem.productId}>
                                <TableCell>{orderItem.productId}</TableCell>
                                <TableCell>{orderItem.productTitle}</TableCell>
                                <TableCell align="right">{orderItem.productPrice}</TableCell>
                                <TableCell align="right">{orderItem.quantity}</TableCell>
                              </TableRow>
                            ))}
                          </TableBody>
                        </Table>
                      </TableCell>
                    </TableRow>
                  </Fragment>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </div>
      </Container>
    );
  }
  
  export default Orders;
  