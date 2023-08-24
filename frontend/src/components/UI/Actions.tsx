import {
  ActionIconsContainerDesktop,
  ActionIconsContainerMobile,
  MyList,
} from "../../styles/navbar/navbar";
import {
  Badge,
  Divider,
  ListItemButton,
  ListItemIcon,
  Tooltip,
} from "@mui/material";
import { ShoppingCart, Person, Dashboard } from "@mui/icons-material";
import { Colors } from "../../styles/theme/mainTheme";
import { Link } from "react-router-dom";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { displayCart } from "../../redux/reducers/cartReducer";
import { useAppDispatch } from "../../hooks/useCustomUsersType";

interface NavBarMatchesProps {
  matches: boolean;
}

function Actions({ matches }: NavBarMatchesProps) {
  const LoggedInUser = useCustomTypeSelector((state) => state.authReducer);
  const LoggedInUserRole = LoggedInUser.user.role;
  const cart = useCustomTypeSelector((state) => state.cartReducer);
  const user = useCustomTypeSelector((state) => state.authReducer.user);
  const Component = matches
    ? ActionIconsContainerMobile
    : ActionIconsContainerDesktop;
  const token = useCustomTypeSelector((state) => state.authReducer.accessToken);
  const dispatch = useAppDispatch();
  const handleCartLinkClick = () => {
    console.log("inside handle cart link");
    dispatch(displayCart(token));
  };

  return (
    <Component>
      <MyList listType="row">
        <ListItemButton sx={{ justifyContent: "center" }}>
          <ListItemIcon
            sx={{
              justifyContent: "center",
              display: "flex",
              color: { matches } && Colors.secondary,
            }}
          >
            <Badge
              badgeContent={`${cart.totalQuantity}`}
              sx={{ color: Colors.black, fontWeight: "bold" }}
            >
              <Tooltip title={`${cart.totalQuantity} items in cart`} arrow>
                <Link to={"/Cart"} onClick={handleCartLinkClick}>
                  {" "}
                  <ShoppingCart sx={{ color: Colors.dim_grey }} />
                </Link>
              </Tooltip>
            </Badge>
          </ListItemIcon>
        </ListItemButton>
        <Divider flexItem orientation="vertical" />
        <ListItemButton sx={{ justifyContent: "center" }}>
          <ListItemIcon
            sx={{
              justifyContent: "center",
              display: "flex",
              color: { matches } && Colors.secondary,
            }}
          >
            <Tooltip title={`${user.role} user`} arrow>
              <Link to={"/Profile"}>
                {" "}
                <Person sx={{ color: Colors.dim_grey }} />
              </Link>
            </Tooltip>
          </ListItemIcon>
        </ListItemButton>
        <Divider flexItem orientation="vertical" />
        {LoggedInUserRole === "Admin" && (
          <ListItemButton>
            <Link to={"/dashboard"}>
              <Dashboard></Dashboard>
            </Link>
          </ListItemButton>
        )}
      </MyList>
    </Component>
  );
}

export default Actions;
