import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import { Link, useParams } from 'react-router-dom'
import { newOrder } from "../../types/Product";
import useCustomTypeSelector from '../../hooks/useCustomTypeSelector'
import { useAppDispatch } from '../../hooks/useCustomUsersType'
import { Product } from '../../types/Product'
import { ProductBox, ProductButton, ProductImage } from '../../styles/products/productsDisplay'
import { BannerTitle } from '../../styles/bannerStyle/bannerStyle'
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { Box, IconButton, Tooltip, Typography } from '@mui/material'
import { Euro } from '@mui/icons-material'
import Carousel from 'react-material-ui-carousel'
import { addToCart}
 //  totalCartAmountAfterUpdate }
    from '../../redux/reducers/cartReducer'
import { getProductById } from '../../redux/reducers/productsReducer'
function ProductDetail() {
  const dispatch = useAppDispatch();
  const [addingToCart, setAddingToCart] = useState(false);
  const [showOptions, setShowOptions] = useState<boolean>(false);
  const { productId } = useParams();
  const LoggedInUser = useCustomTypeSelector((state) => state.authReducer);
  const LoggedInUserRole = LoggedInUser.user.role;
  const token = useCustomTypeSelector((state) => state.authReducer.accessToken);
  useEffect(() => {
    if (productId) {
     dispatch(getProductById(productId));
    }
  }, [productId]);
  const product = useCustomTypeSelector(select => select.productsReducer.productbyId)
  const ProductImages = product.images;
  const handleAddToCart = async (productId: string, quantity: number) => {
    if (addingToCart) {
      return;
    }
    const newOrderr: newOrder = {
      productId: productId,
      quantity: quantity,
    };
    try {
      setAddingToCart(true);
      await dispatch(addToCart({ newOrderr, token }));
    } catch (error) {
      console.error("An error occurred:", error);
    } finally {
      setAddingToCart(false);
    }
  };
  return (
    <>
      <ProductBox>
        <Box
          display="flex"
          flexDirection="column"
          alignItems="center"
          justifyContent="center"
          borderColor="black"
          padding={3}
          margin={2}
          bgcolor="#f1f1f1"
        >
            <Carousel sx={{ width: "20rem", height: "30rem", display: "flex", alignItems: "center", justifyContent: "center", flexDirection: "column" }} >
            {ProductImages?.map((s) =>
              <ProductImage
                src={s}
                alt={product.title}
                sx={{ objectFit: "cover", width: "20rem", height: "20rem" }}>
              </ProductImage>
            )}
          </Carousel>
          <Typography padding-top="1px" variant="h3" component="h3" style={{ fontFamily: 'FunkyFont', textTransform: "uppercase", color: 'grey', textShadow: '2px 2px 4px #000000' }}>
            {product.title}
          </Typography>
          <Typography variant="h4" style={{ fontFamily: 'FunkyFont', color: 'grey', textShadow: '2px 2px 4px #000000' }} >
            Product Code : {product.id}
          </Typography>
          <Typography variant="h4" style={{ fontFamily: 'FunkyFont', color: 'grey', textShadow: '2px 2px 4px #000000' }} >
            {product.price} <Euro></Euro>
          </Typography>
          <Typography style={{ fontFamily: 'FunkyFont', color: 'grey', textShadow: '2px 2px 4px #000000' }} variant="h6" component="p" display={"flex"} justifyContent={"center"} alignItems={"center"} marginBottom={"3em"}>
            Description: {product.description}
          </Typography>
          <Box>
            <ProductButton   sx={{border:"1px black solid"}} variant="contained">
                        {!token ? (
<Tooltip title={`login to shop`} arrow>
              <Link to={"/Profile"} style={{ textDecoration: "none" }}>  
              <div>Login to Add to Cart</div>
              <IconButton>
                <AccountCircleIcon />
             </IconButton>          
              </Link>
</Tooltip>
): (
                  LoggedInUserRole !== "Admin"  && (
                    <ProductButton
                      variant="outlined"
                      disabled={addingToCart}
                      onClick={() => {
                        handleAddToCart(product.id, 1);
                      }}
                    >
                      Add to Cart
                    </ProductButton>
                  ))}
                  </ProductButton>      
                  </Box>
      
        </Box>
      </ProductBox>
    </>
  );
}

export default ProductDetail