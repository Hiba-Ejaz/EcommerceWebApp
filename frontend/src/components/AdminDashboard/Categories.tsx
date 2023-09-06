import { Button } from '@mui/material'
import React from 'react'
import { Colors } from '../../styles/theme/mainTheme'
import { CustomisedLink } from '../../styles/navbar/navbar'


function Categories() {
  return (
    <div>
    <Button variant="outlined" sx={{width:"100%", border:"none",
      transition: "background-color 0.3s, border 0.3s, color 0.3s",
      "&:hover": {
        background: Colors.dim_grey,
        border: "none",
        color: Colors.black,
      }}}>
    <CustomisedLink sx={{color: Colors.black}} to={"/CreateCategory"}>
                          Create Category
    </CustomisedLink>
                      </Button>
                      </div>
  )
}

export default Categories