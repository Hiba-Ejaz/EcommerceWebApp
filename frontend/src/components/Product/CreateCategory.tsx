import React, { FormEvent, useState } from "react";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import {
  Box,
  Button,
  TextField,
  Typography,
  keyframes,
  styled,
} from "@mui/material";
import { CategoryCreate } from "../../types/Category";
import { createNewCategory } from "../../redux/reducers/categoryReducer";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { Colors } from "../../styles/theme/mainTheme";
type AddProductType = {
  title: string;
  price: number;
  description: string;
  categoryId: number;
  images: Array<string>;
};
function CreateCategory() {
  const [CreateNew, setCreateNew] = useState(false);
  const [created, setCreated] = useState(false);
  const categoryCreated = useCustomTypeSelector(
    (state) => state.categoryReducer.categories
  );
  const [categoryData, setCategoryData] = useState<CategoryCreate>({
    name: "",
    //imagesIds:[],
  });
  const slideAnimation = keyframes`
from {
  transform: translateY(-100%);
}
to {
  transform: translateY(0);
}
`;
  const AnimatedBox = styled(Box)`
    animation: ${slideAnimation} 0.5s;
  `;
  const dispatch = useAppDispatch();
  const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    }   
  const token = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
 
  const handleSubmit = (event: FormEvent) => {
    event.preventDefault();
     setCreateNew(false);
     dispatch(createNewCategory({category:categoryData,token:token}));
  };
  return (
    <div>
      <div>Create Category</div>
      {(!categoryCreated || CreateNew) && (
        <form onSubmit={handleSubmit}>
          <TextField
            label="Title"
            name="title"
            value={categoryData.name}
            onChange={handleChange}
            fullWidth
          />

          <label>Add category images (it will take only first 3 images)</label>
          <input 
        type="file"
        name="images"
        onChange={handleChange}
        multiple
      />
          <Button type="submit" variant="contained" color="primary">
            Add Category
          </Button>
        </form>
      )}
      {categoryCreated && !CreateNew && (
        <AnimatedBox>
          <Box
            sx={{
              display: "flex",
              fontFamily: "Raleway, Arial",
              justifyContent: "center",
              alignContent: "center",
              margin: "3em",
              padding: "2em",
              boxShadow: "2",
              borderRadius: "2",
              backgroundColor: Colors.primary,
            }}
          >
            <Typography variant="h3">
              CATEGORY HAS BEEN SUCCESSFULLY CREATED
            </Typography>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              onClick={() => setCreateNew(true)}
            >
              Add Another Category
            </Button>
          </Box>
        </AnimatedBox>
      )}
    </div>
  );
}
export default CreateCategory;
