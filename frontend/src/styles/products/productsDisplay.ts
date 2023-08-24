import { Box, Button, ButtonProps, IconButton, Theme, Typography, styled } from "@mui/material";
import { Colors } from "../theme/mainTheme";
import { slideInBottom } from "../../animation/animations";
import { Directions } from "@mui/icons-material";
import { pink } from "@mui/material/colors";


export const FilterBox=styled(Box)(({theme})=>({
display:"flex",
width:"100%",
height:"100%",
justifyContent:"center",
padding:"0px 0px",
background:Colors.dove_grey,
alignItems:"center",
maxWidth:420,
borderRadius: "4px",
boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
[theme.breakpoints.down('md')]:{
display:"block",
textAlign: "center",
}
}));

export const ProductBox=styled(Box)(({theme})=>({
    display:"flex",
    justifyContent:"center",
    alignItems:"center",
    flexDirection:"column",
    height:"100%",
    width:"100%",
    borderSpacing:"6em",
    border: "2px solid rgba(0, 0, 0, 0.2)",
    borderRadius: "4px",
    boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
    [theme.breakpoints.down("md")]:{
        position:'relative',
        margin:"1em",
        width:"80%",
        borderRadius: "2px",
    }
}));

export const ProductImage=styled('img')(({src,theme})=>({
src:`url(${src})`,
width:"100%",
boxShadow: '0 0 10px 5px rgba(0, 0, 0, 0.5)',
//filter: 'blur(5px)',
padding:'2px',
height:"250px",
marginTop:"2px",
[theme.breakpoints.down('md')]:{
    width:"100%",
    padding:"1px",
}
}));

export const ProductActionButton=styled(IconButton)(()=>({
background:Colors.black,
margin:4,
}));

export const CategoryTag=styled(Typography)(({theme})=>({
    backgroundColor:"black", position: 'absolute',
    fontWeight:"bolder",
     top: '80%',
      left: '50%', 
      transform: 'translate(-50%, -50%)',
       zIndex: 1 ,
       display:"flex",
       justifyContent:"center",
       alignItems:"center",
       width:"100%",
       height:"20%",
       background: 'rgba(0, 0, 0, 0.8)',
    "&:hover": {
        background: Colors.black,
        border: "none",
        color: Colors.dim_grey
      }
}))


export const CategoryButton=styled(Button)(({theme})=>({
    background:Colors.black,
    display:"flex",
    margin:4,
    justifyContent:"center",
    textDecoration:"none",
    width:"24rem",
    height:"12rem",
    color:Colors.dim_grey,
    transition: "background-color 0.3s, border 0.3s, color 0.3s",
    [theme.breakpoints.down('md')]:{
        flexDirection:"column",
        width:"100%",
        margin:"auto",  
    },
    }));



interface ProductAddToCartProps extends ButtonProps {
    show?: boolean;
  } 

export const ProductButton=styled(Button,{shouldForwardProp:(prop)=>prop!=='show'})<ProductAddToCartProps>(({show,theme})=>({
    width:'120px',
    fontSize:'12px',
    margin:"0.2em",
    backgroundColor:Colors.light_grey,
    color:Colors.black,
    border:"none",
    [theme.breakpoints.down('md')]:{
        animation:
        show && `${slideInBottom} 0.5s cubic-bexier(0.250,0.460,0.450,0.940) both`,  
    },
    background:Colors.primary,
    opacity:0.9,
    transition: "background-color 0.3s, border 0.3s, color 0.3s",
    "&:hover": {
      background: Colors.dim_grey,
      border: "none",
      color: Colors.black,
    },
}));

