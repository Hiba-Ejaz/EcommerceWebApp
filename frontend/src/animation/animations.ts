import { keyframes } from "@mui/material";

 export const slideInBottom=keyframes`
 0%{
    -webkit-transform:translateY(50px);
            transform:translateY(50px);
    opacity:0; 
 }
 100%{
    -webkit-transform:translateY(0);
            transform:translateY(0);
    opacity:1; 
 }
 `;