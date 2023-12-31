import { useNavigate } from "react-router";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import { newOrder } from "../../types/Product";
import {
  addToCart,
  deleteItemFromCart,
  displayCart,
} from "../../redux/reducers/cartReducer";
import {
  Box,
  Button,
  Card,
  CardContent,
  Divider,
  Grid,
  IconButton,
  ListItemButton,
  Typography,
} from "@mui/material";
import { AddCircle, Dashboard, RemoveCircle } from "@mui/icons-material";
import { Colors } from "../../styles/theme/mainTheme";
import { Link } from "react-router-dom";
import Users from "./Users";
import {
  displayAllUsers,
  fetchAllUsers,
} from "../../redux/reducers/usersReducer";
import { fetchAllOrders } from "../../redux/reducers/orderReducer";

function Dashboarddd() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const token = useCustomTypeSelector((state) => state.authReducer.accessToken);
  const handleNavigateAndFetch = async () => {
    try {
      await dispatch(displayAllUsers(token));
      navigate("/users");
    } catch (error) {
      console.error("Error fetching and navigating:", error);
    }
  };
  const handleNavigateAndFetchOrders = async () => {
    try{
     await dispatch(fetchAllOrders(token));
    navigate("/orders");
    }
    catch (error) {
      console.error("Error fetching and navigating orders:", error);
    }
  };
  // const handleNavigateCategories = () => {
  //   navigate("/categories");
  // };

  return (
    <div>
      <nav>
        <ul>
          <li>
            <ListItemButton>
              <Link to={"/users"} onClick={handleNavigateAndFetch}>
                Users Panel
              </Link>
            </ListItemButton>
          </li>
          <li>
            <ListItemButton>
              <Link to={"/orders"} onClick={handleNavigateAndFetchOrders}>
                Orders Panel
              </Link>
            </ListItemButton>
          </li>
          {/* <li>
          <ListItemButton>
          <Link to={"/categories"} onClick={handleNavigateCategories}>
              Categories
            </Link>
         </ListItemButton>
          </li> */}
        </ul>
      </nav>
      {/* <Grid container spacing={2}>
          
          <Grid item xs={12} sm={4}>
            <Card sx={{ marginBottom: 2 }}>
              <CardContent>
               
                
              
              </CardContent>
            </Card>
          </Grid>
        </Grid> */}
    </div>
  );
}

export default Dashboarddd;
