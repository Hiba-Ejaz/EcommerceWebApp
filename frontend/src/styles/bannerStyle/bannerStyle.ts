import { styled } from "@mui/material/styles";
import { Colors } from "../theme/mainTheme";
import { Box, Typography } from "@mui/material";

export const BannerContainer = styled(Box)(({theme})=>({
    display:"flex",
    width:"100%",
    height:"100%",
    justifyContent:"center",
    padding:"0px 0px",
    background:Colors.dove_grey,
    [theme.breakpoints.down('sm')]:{
        flexDirection:'column',
        alignItems:'center'
    }
}));

export const BannerImage=styled('img')(({src,theme})=>({
    src:`url(${src})`,
    width:'500px',
    [theme.breakpoints.down('sm')]:{
        width:'320px',
        height:'300px'
    }
}))

export const BannerContent=styled(Box)(({theme})=>({
display:"flex",
flexDirection:"column",
maxWidth:420,
justifyContent:"center",
padding:"30px" ,
[theme.breakpoints.down('md')]:{
    FontSize:'40px',
    flexDirection:"column", 
        paddingLeft:"0",
        paddingRight:"4em",
},

}));

export const BannerTitle=styled(Typography)(({theme})=>({
lineHeight:1.5,
display:"flex",
textAlign:"center",
fontSize:"60px",
marginBottom:"20px",
justifyContent:"center",
[theme.breakpoints.down('md')]:{
    FontSize:'35px',
    flexDirection:"column", 
    marginRight:"10em",
    marginLeft:"0.5em",
},
[theme.breakpoints.down('sm')]:{
    fontSize:'42px',
    display:"flex",
textAlign:"center",
marginRight:"0",
marginLeft:"1.5em"
}
}));

export  const BannerDescription=styled(Typography)(({theme})=>({
    lineHeight:1.25,
    letterSpacing:1.25,
    marginBottom:"3em",
    display:"flex",
textAlign:"center",
[theme.breakpoints.down('md')]:{
        fontSize:"1em",
        flexDirection: 'column',
        marginRight:"9.5em",
        marginLeft:"1em"
},
    [theme.breakpoints.down('sm')]:{
        lineHeight:1.25,
        letterSpacing:1.15,
        marginBottom:"1.5em",
        textAlign:"center",
        marginRight:"0"
    }
}))
