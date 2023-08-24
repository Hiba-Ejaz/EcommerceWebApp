import React, { useCallback, useState } from "react";
import { CustomisedLink, MyList, NavbarContainer, NavbarHeader } from "../../styles/navbar/navbar";
import {
  Button,
  Divider,
  ListItemButton,
  ListItemIcon,
  ListItemSecondaryAction,
  ListItemText,
  TextField,
  debounce,
} from "@mui/material";
import { Search } from "@mui/icons-material";
import Actions from "./Actions";
import { Link } from "react-router-dom";
interface NavBarMatchesProps {
  matches: boolean;
}
function NavBarDesktop({ matches }: NavBarMatchesProps) {
  const [searchName, setSearchName] = useState("");
  const handleSearhByNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const text = e.target.value;
    setSearchName(text);
    // memoizedCallback(text);
  };
  // const memoizedCallback = useCallback(
  //  debounce((text: string) => {
  //     setSearchName(text);
  //   // <Products searchName={searchName}></Products>
  //   }, 1000),
  //   []
  // );


  return (
    <>
    <NavbarContainer>
      <NavbarHeader>Shop Shop</NavbarHeader>
      <MyList listType="row">
      {/* <ListItemButton sx={{ justifyContent: "center" }}> <CustomisedLink to={"/"}>Home</CustomisedLink></ListItemButton> */}
        <ListItemButton>
          <CustomisedLink to={"/Products"}>Products</CustomisedLink>
          </ListItemButton>
          
          <ListItemIcon sx={{ justifyContent: "center"}}> 
         
            <TextField
            sx={{width:"20rem", margin: '1em', fontFamily: 'Arial' , '& .MuiOutlinedInput-root.Mui-focused .MuiOutlinedInput-notchedOutline':  { borderColor: 'black' } }}
            placeholder="Search Product By Name"
            onChange={handleSearhByNameChange}
          />
          
          <Button component={Link} to={`/Products/${searchName}`}>
          <Search sx={{ color:"black"}}/>
          </Button>
          </ListItemIcon>
      </MyList>
      <Divider sx={{height:"2em",marginTop:"2em"}} flexItem orientation="vertical" />
      <Actions matches={matches}></Actions>
      </NavbarContainer>
     
     </>
  );
}

export default NavBarDesktop;
