import React, { useCallback, useEffect, useState } from "react";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import EuroSymbolIcon from '@mui/icons-material/EuroSymbol';
import {
  Box,
  Button,
  Card,
  CardContent,
  Container,
  FormControl,
  Grid,
  InputLabel,
  MenuItem,
  Pagination,
  Select,
  SelectChangeEvent,
  TextField,
  Typography,
  debounce,
  useMediaQuery,
  useTheme,
} from "@mui/material";

import { useAppDispatch } from "../../hooks/useCustomUsersType";
import {
  deleteProduct, fetchAllProducts,  getProductForUpdate, setProductIdForUpdate, 
 // fetchAllProducts,
 // setProductForUpdate,
} from "../../redux/reducers/productsReducer";
import { Product, newOrder } from "../../types/Product";
import { Link, Route, useParams } from "react-router-dom";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormLabel from "@mui/material/FormLabel";
import { addToCart} 
  // removeFromCart, totalCartAmount, totalCartAmountAfterUpdate, updateCartItem } 
from "../../redux/reducers/cartReducer";
import { CustomisedLink } from "../../styles/navbar/navbar";
import { CategoryButton, FilterBox, ProductActionButton, ProductBox, ProductButton, ProductImage } from "../../styles/products/productsDisplay";
import ProductDetail from "./ProductDetail";
import { Colors } from "../../styles/theme/mainTheme";
import { ForkRight } from "@mui/icons-material";
import { grey } from "@mui/material/colors";
import { ProductRead } from "../../types/NewProduct";
import internal from "stream";

const pageSize = 10;
function Products({  categoryId = 0 }: { categoryId?: number }) {
  const [pagination, setPagination] = useState({
    count: 0,
    from: 0,
    to: pageSize
  })
  const [paginatedProducts, setPaginatedProduct] = useState<ProductRead[]>([]);
  const theme = useTheme();
  const matches = useMediaQuery(theme.breakpoints.down('md'))
  const [sortOrder, setSortOrder] = useState("");
  const [searchByName, setSearchByName] = useState("");
  const [filteredList, setFilteredList] = useState<ProductRead[]>([]);
  const dispatch = useAppDispatch();
  const [lowerPriceRange, setLowerPriceRange] = useState(0);
  const [upperPriceRange, setUpperPriceRange] = useState(0);
  const [filter, setFilter] = useState(false);
  const [showOptions, setShowOptions] = useState<boolean>(false);
  const products = useCustomTypeSelector(
    (state) => state.productsReducer.products
  );

    
  const cart = useCustomTypeSelector((state) => state.cartReducer);
  const LoggedInUser = useCustomTypeSelector((state) => state.authReducer);
  const LoggedInUserRole = LoggedInUser.user.role;
  console.log("logged in user", LoggedInUserRole);
  console.log("cart Items", cart.cart);
  const service = {
    getData: ({ from, to }: { from: number; to: number }) => {
      return filteredList.slice(from, to)
    }
  }

  const { searchName } = useParams();
  const token = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
  const handleAddToCart = ( productId:string,quantity:number) => {
    const newOrderr:newOrder = {
      productId: productId,
      quantity: quantity,
    };
    console.log("going to dispatch add to cart");
    dispatch(addToCart({newOrderr,token}));
   
  }

  useEffect(() => {
    
      
    setPaginatedProduct(service.getData({ from: pagination.from, to: pagination.to }));
    setPagination({ ...pagination, count: filteredList.length })
  }, [pagination.from, pagination.to, filteredList]);

//   useEffect(() => {
//     dispatch(getAllProducts(searchName));
// }
//   , [searchName]);

  useEffect(() => {
    
    
    let filteredProducts = products;
    if (categoryId !== 0) {
     // filteredProducts = filteredProducts.filter(
       // (product) => product.category.id === categoryId
     // );
    }
    if (upperPriceRange !== 0) {
      filteredProducts = filteredProducts.filter(
        (product) =>
          product.price >= lowerPriceRange && product.price <= upperPriceRange
      );
    }
    if (searchName && searchName !== "") {
      console.log("search name in app rendered"+searchName);
      filteredProducts = filteredProducts.filter((product) =>
        product.title.toLowerCase().includes(searchName)
      );   
    }
    if (sortOrder === "asc") {
      filteredProducts = ([...filteredProducts].sort((a, b) => a.price - b.price))
    } else if (sortOrder === "desc") {
      console.log("desc");
      filteredProducts = ([...filteredProducts].sort((a, b) => b.price - a.price))
    }
    setFilteredList(filteredProducts);
  }, [categoryId, upperPriceRange, searchName, products, sortOrder]);
const handleSortChange = (e: SelectChangeEvent) => {
    setSortOrder(e.target.value);
  };

  const handleCategoryChangeButton = (e: React.MouseEvent<HTMLButtonElement>) => {
    const catId = Number(e.currentTarget.value);
    categoryId=catId;
    //setCategoryId(catId);
  };
  // const handleSetProductIdAndFetchProduct = async (productId: string) => {
  //   dispatch(setProductIdForUpdate(productId));
  //   try {
  //     await dispatch(getProductForUpdate(productId));
  //     return 1;
  //   } catch (error) {
      
  //   }
  // };
  
  const handleEditProduct = async (productId: string) => {
    dispatch(setProductIdForUpdate(productId));
    await dispatch(getProductForUpdate(productId)); // Use the async action creator
  };


  const handlePriceChange = (e: SelectChangeEvent) => {
    const price = e.target.value;
    const rangeValues = price.split("-");
    setLowerPriceRange(parseInt(rangeValues[0], 10));
    setUpperPriceRange(parseInt(rangeValues[1], 10));
  };

  const handleMouseLeave = () => {
    setShowOptions(false);
  }
  const handleMouseEnter = () => {
    setShowOptions(true);
  }
  const handlePageChange = (event: React.ChangeEvent<unknown>, page: number) => {
    const from = (page - 1) * pageSize;
    const to = (page - 1) * pageSize + pageSize;
    setPagination({ ...pagination, from: from, to: to })
  }

  return (
    <div>
      <Box sx={{textAlign:"center",display:"flex",margin:"auto", 
      [theme.breakpoints.down('md')]:{
        flexDirection:"column"
    }}}>
                      </Box>

      <FilterBox sx={{ maxWidth: 1300, display:"flex",
                justifyContent:"space-between", }}>
<Box>
<FormControl
            sx={{
              //m: 1,
              minWidth: 120,
              //marginRight: '0',
              fontFamily: 'Arial',
            }}
          >
            <Box display={"flex"}>
            <FormLabel sx={{ fontFamily: 'Arial' , margin:"auto",padding:"1em",textAlign:"center",   '&.Mui-focused': { color:Colors.dim_grey } }}>Select Price Range</FormLabel>
            <RadioGroup name="radio-buttons-group" onChange={handlePriceChange}>
              <FormControlLabel
                value="0-300"
                control={<Radio sx={{ color: Colors.dim_grey, '&.Mui-checked': { color: Colors.dim_grey  } }}/>}
                label="0-300"
                sx={{ fontFamily: 'Arial', color:Colors.dim_grey  }}
              />
              <FormControlLabel
                value="301-600"
                control={<Radio sx={{ color: Colors.dim_grey, '&.Mui-checked': { color: Colors.dim_grey  } }}/>}
                label="301-600"
                sx={{ fontFamily: 'Arial',color:Colors.dim_grey  }}
              />
              <FormControlLabel
                value="601-900"
                control={<Radio sx={{ color: Colors.dim_grey, '&.Mui-checked': { color: Colors.dim_grey  } }}/>}
                label="601-900"
                sx={{ fontFamily: 'Arial' ,color:Colors.dim_grey  }}
              />
              <FormControlLabel
                value="0-0"
                control={<Radio sx={{color: Colors.dim_grey, '&.Mui-checked': { color: Colors.dim_grey  } }}/>}
                label="no price range"
                sx={{ fontFamily: 'Arial' ,color:Colors.dim_grey }}
              />
            </RadioGroup>
            </Box>
          </FormControl>
</Box>
        <div>
          {/* <TextField
            sx={{ margin: '1em', fontFamily: 'Arial' , '& .MuiOutlinedInput-root.Mui-focused .MuiOutlinedInput-notchedOutline':  { borderColor: 'black' } }}
            variant="outlined"
            placeholder="Search Product By Name"
            onChange={handleSearhByNameChange}
          /> */}
        </div>
        <div>
          <FormControl
            sx={{
              m: 1,
              minWidth: 120,
              margin: '1em',
              fontFamily: 'Arial',
            }}
          >
            <InputLabel sx={{ fontFamily: 'Arial' , '&.Mui-focused': { color: 'black' }}}>Sort Order</InputLabel>
            <Select
              id="select-sort-order"
              value={sortOrder}
              onChange={handleSortChange}
              sx={{
                '& .MuiOutlinedInput-root': {
                  '& fieldset': {
                    borderColor: 'black',
                  },
                  '&:hover fieldset': {
                    borderColor: 'black',
                  },
                  '&.Mui-focused fieldset': {
                    borderColor: 'black',
                  },
                },
              }}
            >
              <MenuItem value="asc">Price Low to High</MenuItem>
              <MenuItem value="desc">Price High to Low</MenuItem>
            </Select>
          </FormControl>
        </div>

      </FilterBox>
      <Box display="flex" justifyContent={"center"} sx={{ p: 4 }}>
        <Typography variant="h4">
          Our Products
        </Typography>
      </Box>

      <Container>
      {LoggedInUserRole === "Admin" && (
      <Button variant="outlined" sx={{width:"100%", border:"none",
      transition: "background-color 0.3s, border 0.3s, color 0.3s",
      "&:hover": {
        background: Colors.dim_grey,
        border: "none",
        color: Colors.black,
      },}} >
                        <CustomisedLink sx={{color: Colors.black}} to={"/CreateProduct"}>
                          Create Product
                        </CustomisedLink>
                      </Button>
      )}
        <Grid container columns={{ xs: 4, sm: 8, md: 12 }} spacing={{ xs: 2, md: 3 }} justifyContent={"center"} >
          {paginatedProducts.map((product) => (
            <Grid display={matches ? "flex" : "flex"} item xs={2} sm={3} flexDirection="column"
              alignItems="center" 
              // key={product.id}
               sx={{ maxWidth: 400, margin: "0.5em" }}>
              <ProductBox onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}>
                {/* <Link to={`/details/${(product.id)}`}> */}
                  <ProductImage
                   // src={product.imagesIds[0]}
                    alt={product.title}
                    style={{ objectFit: "cover" }} />
                {/* </Link> */}
                <CardContent sx={{ textAlign: "center" }} >
                  <Typography>
                    {product.title}
                  </Typography>
                  <Typography >
                    {/* Category Name: {product.} */}
                  </Typography>
                  <Typography >
                    {/* Category ID: {product.category.id} */}
                  </Typography>
                  <Typography >
                    Price:  <EuroSymbolIcon sx={{ fontSize: '14px' }} />{product.price}
                  </Typography>
                  {LoggedInUserRole !== "Admin" && showOptions && 
                  (<ProductButton
                    variant="outlined" show={showOptions}
                    onClick={() => {
                     
                      handleAddToCart(product.id,1); //as i dont have quantity so making it 3 for now
                      // dispatch(addToCart(product,))
                      //dispatch(totalCartAmountAfterUpdate());
                    }}
                  >
                    Add to Cart
                  </ProductButton>)}
                  {LoggedInUserRole === "Admin" && (
                    <>
                      <ProductButton
                        variant="outlined"
                        onClick={() => {
                          // dispatch(addToCart(product))
                          // dispatch(totalCartAmountAfterUpdate());
                        }}
                      >
                        Add to Cart
                      </ProductButton>
                      <ProductButton
                        variant="outlined"
                        onClick={() => {
                          dispatch(deleteProduct(product.id)).then((resultAction)=> {
                            if (deleteProduct.fulfilled.match(resultAction)) {
                              console.log('Product deletion successful:', resultAction.payload);
                              dispatch(fetchAllProducts());
                            } else if (deleteProduct.rejected.match(resultAction)) {
                              console.error('Product deletion failed:', resultAction.error.message);
                            }
                          // dispatch(removeFromCart(product))
                         // dispatch(totalCartAmountAfterUpdate());
                        })}}
                      >
                        Delete Product
                      </ProductButton>
                      <ProductButton
                        variant="outlined"
                        onClick={
                          
                            () => handleEditProduct(product.id)
                        // dispatch(setProductIdForUpdate(product.id))
                        //   dispatch(getProductForUpdate(product.id));
                        }
                      > 
                        <CustomisedLink sx={{color:Colors.black}} to={"/UpdateProduct"}>
                          Edit Product
                        </CustomisedLink>
                      </ProductButton>
                    </>
                  )}
                </CardContent>
              </ProductBox>
            </Grid>
          ))}
        </Grid>
        <Box sx={{ margin: "20px 0px 80px 0px" }} justifyContent={"center"} alignItems={"center"} display={"flex"}>
          <Pagination onChange={handlePageChange} count={Math.ceil(pagination.count / pageSize)}></Pagination>
        </Box>    </Container>
    </div>
  );
}

export default Products;
