import { ThemeProvider } from "@emotion/react";
import { Box, Container } from "@mui/material";
import React, { useState } from "react";
import NavBar from "./UI/NavBar";
import Banner from "./UI/Banner/Banner";
import theme from "../styles/theme/mainTheme";
import {
  CategoryButton,
  CategoryTag,
} from "../styles/products/productsDisplay";
import Products from "./Product/Products";
import shoes from "../images/shoes.jpg";
import electronics2 from "../images/electronics2.jpg";
import furniture from "../images/furniture.jpg";
import clothes from "../images/clothes.jpg";
import others from "../images/others.jpg";
import all from "../images/all.jpg";

function Home() {
  const [categoryId, setCategoryId] = useState(0);
  const handleCategoryChangeButton = (
    e: React.MouseEvent<HTMLButtonElement>
  ) => {
    const catId = Number(e.currentTarget.value);
    setCategoryId(catId);
  };
  return (
    <ThemeProvider theme={theme}>
      <Container maxWidth="xl" sx={{ background: "#fff" }}>
        <Box
          sx={{
            textAlign: "center",
            display: "flex",
            margin: "auto",
            [theme.breakpoints.down("md")]: {
              flexDirection: "column",
            },
          }}
        >
          {/* <CategoryButton
            variant="outlined"
            value="1"
            style={{
              backgroundImage: `url(${clothes})`,
              backgroundSize: "cover",
            }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>Clothes</CategoryTag>
          </CategoryButton>
          <CategoryButton
            variant="outlined"
            value="5"
            style={{
              backgroundImage: `url(${others})`,
              backgroundSize: "cover",
            }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>Others</CategoryTag>
          </CategoryButton>
          <CategoryButton
            variant="outlined"
            value="2"
            style={{
              backgroundSize: "cover",
              backgroundImage: `url(${electronics2})`,
            }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>Electronics</CategoryTag>
          </CategoryButton>
          <CategoryButton
            variant="outlined"
            value="3"
            style={{
              backgroundImage: `url(${furniture})`,
              backgroundSize: "cover",
            }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>Furniture</CategoryTag>
          </CategoryButton>
          <CategoryButton
            variant="outlined"
            value="4"
            style={{
              backgroundImage: `url(${shoes})`,
              backgroundSize: "cover",
            }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>Shoes</CategoryTag>
          </CategoryButton>
          <CategoryButton
            variant="outlined"
            value="0"
            style={{ backgroundImage: `url(${all})`, backgroundSize: "cover" }}
            onClick={handleCategoryChangeButton}
          >
            <CategoryTag>All</CategoryTag>
          </CategoryButton> */}
        </Box>
      </Container>
      {/* {categoryId && <Products categoryId={categoryId} />} */}
      <Products></Products>
    </ThemeProvider>
  );
}

export default Home;
