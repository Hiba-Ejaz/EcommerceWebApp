import { styled } from "@mui/material";

export const CustomisedForm=styled('form')(({theme})=>({
    padding:2,
    [theme.breakpoints.down('sm')]:{
    marginBottom:"4em"
    }
}))