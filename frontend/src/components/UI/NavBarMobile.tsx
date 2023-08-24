import React, { useState } from "react";
import {
  CustomisedLink,
  NavbarContainer,
  NavbarHeader,
} from "../../styles/navbar/navbar";
import { Button, IconButton,  ListItemButton, ListItemIcon, MenuItem, TextField } from "@mui/material";
import { Menu, Search } from "@mui/icons-material";
import Actions from "./Actions";
import { Link } from "react-router-dom";

interface NavBarMatchesProps {
  matches: boolean;
}

function NavBarMobile({ matches }: NavBarMatchesProps) {
  const [searchName, setSearchName] = useState("");
  const handleSearhByNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const text = e.target.value;
    setSearchName(text);
    // memoizedCallback(text);
  };
  return (
    <>
      <NavbarContainer>
        
        <ListItemButton sx={{ justifyContent: "center" }}>
          <CustomisedLink to={"/Products"}>Products</CustomisedLink>
          </ListItemButton>
          <ListItemIcon sx={{ justifyContent: "center"}}> 
         
         <TextField
         sx={{width:"10rem", marginLeft: '2em', fontFamily: 'Arial' , '& .MuiOutlinedInput-root.Mui-focused .MuiOutlinedInput-notchedOutline':  { borderColor: 'black' } }}
         placeholder="Search By Name"
         onChange={handleSearhByNameChange}
       />
       
       <Button component={Link} to={`/Products/${searchName}`}>
       <Search sx={{ color:"black"}}/>
       </Button>
       </ListItemIcon>
        <NavbarHeader>Shop Shop</NavbarHeader>
        <Actions matches={matches}></Actions>
      </NavbarContainer>
    </>
  );
}

export default NavBarMobile;
