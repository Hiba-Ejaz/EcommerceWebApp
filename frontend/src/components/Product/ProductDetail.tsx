import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import { useParams } from 'react-router-dom'
import useCustomTypeSelector from '../../hooks/useCustomTypeSelector'
import { useAppDispatch } from '../../hooks/useCustomUsersType'
import { Product } from '../../types/Product'
import { ProductBox, ProductButton, ProductImage } from '../../styles/products/productsDisplay'
import { BannerTitle } from '../../styles/bannerStyle/bannerStyle'
import { Box, Typography } from '@mui/material'
import { Euro } from '@mui/icons-material'
import Carousel from 'react-material-ui-carousel'
import { addToCart}
 //  totalCartAmountAfterUpdate }
    from '../../redux/reducers/cartReducer'
function ProductDetail() {
  const dispatch = useAppDispatch();
  const { productId } = useParams();
  if (productId) {
  //  dispatch(getSingleProduct(productId));
  }
  const product = useCustomTypeSelector(select => select.productsReducer.singleProductDetail)
  //const ProductImages = product.imagesIds;
  return (
    <>
      <ProductBox>
        <Box
          display="flex"
          flexDirection="column"
          alignItems="center"
          justifyContent="center"
          borderColor="black"
          padding={1}
          margin={2}
          bgcolor="#f1f1f1"
        >
          {/* - */}
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
          <ProductButton
          sx={{border:"1px black solid"}}
                        variant="contained"
                        onClick={() => {
                         // dispatch(addToCart(product))
                       //   dispatch(totalCartAmountAfterUpdate());
                        }}
                      >
                        Add to Cart
                      </ProductButton>
        </Box>
      </ProductBox>
    </>
  );
}

export default ProductDetail