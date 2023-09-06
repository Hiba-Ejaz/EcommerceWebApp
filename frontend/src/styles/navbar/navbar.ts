import Box from "@mui/material/Box";

import { Colors } from "../theme/mainTheme";
import "@fontsource/montez";
import Typography from "@mui/material/Typography";
import { styled } from "@mui/system";
import List, { ListProps } from "@mui/material/List";
import { Link } from "react-router-dom";

export const NavbarContainer = styled(Box)(() => ({
  display: "flex",
  marginTop: 4,
  justifyContent: "center",
  alignItems: "center",
  padding: "2px 8px",
}));

export const NavbarHeader = styled(Typography)(() => ({
  padding: "4px",
  flexGrow: 1,
  fontSize: "2em",
  fontFamily: '"Montez","cursive"',
  color:Colors.dim_grey,
}));

interface MyListProps extends ListProps {
  listType?: string;
}

export const MyList = styled(List)<MyListProps>(({ listType }) => ({
  display: listType === "row" ? "flex" : "block",
  flexGrow: 3,
  justifyContent: "center",
  alignItems: "center",
}));

export const CustomisedLink=styled(Link)(()=>({
    textDecoration:"none",
    color:Colors.dim_grey,
    display:"flex",
    justifyContent: "center",
    alignItems: "center",
}));

export const ActionIconsContainerMobile = styled(Box)(() => ({
  display: "flex",
  background: Colors.shaft,
  bottom: 0,
  left: 0,
  borderTop: `1px solid ${Colors.border}`,
  justifyContent: "center",
  alignItems: "center",
  width: "100%",
  zIndex: 99,
  position: "fixed",
}));

export const ActionIconsContainerDesktop = styled(Box)(() => ({
  flexGrow: 0,
  color:Colors.dim_grey,
}));
