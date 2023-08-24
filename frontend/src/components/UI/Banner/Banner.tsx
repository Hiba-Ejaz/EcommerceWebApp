import { Typography, useTheme } from "@mui/material";
import { useMediaQuery } from "@mui/material";
import React from 'react'
import { BannerContainer, BannerContent, BannerDescription, BannerImage, BannerTitle } from "../../../styles/bannerStyle/bannerStyle";
import banner from "../../../images/banner.webp";
function Banner() {
    const theme = useTheme();
    const matches = useMediaQuery(theme.breakpoints.down("md"));
  return (
    <BannerContainer>
       <BannerImage alt="banner-image" src={banner} />
      <BannerContent>
        <Typography sx={{paddingLeft:"4em" , '@media (max-width: 960px)': {paddingLeft:"2em",marginRight:"8em"} ,'@media (max-width: 600px)': {paddingLeft:"6em",marginRight:"0"}}} variant="h6">Unleash Limitless Choices</Typography>
        <BannerTitle variant="h2">Shop Every Category</BannerTitle>
        <BannerDescription variant="subtitle1">From fashion and electronics to home decor and wellness, we bring together the entire retail universe at your fingertips.</BannerDescription>
      </BannerContent>
    </BannerContainer>
  )
}

export default Banner
