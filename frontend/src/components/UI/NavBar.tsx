import { useTheme } from "@mui/material";
import { useMediaQuery } from "@mui/material";
import React from "react";
import NavBarMobile from "./NavBarMobile";
import NavBarDesktop from "./NavBarDesktop";

function NavBar() {
  const theme = useTheme();
  const matches = useMediaQuery(theme.breakpoints.down("md"));
  return (
    <>
      {matches ? (
        <NavBarMobile matches={matches}></NavBarMobile>
      ) : (
        <NavBarDesktop matches={matches}></NavBarDesktop>
      )}
    </>
  );
}

export default NavBar;
