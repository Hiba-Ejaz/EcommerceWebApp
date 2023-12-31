import React, { FormEvent, useState } from 'react'
import useCustomTypeSelector from '../../hooks/useCustomTypeSelector'
import { User } from '../../types/User';
import { Box, Button, TextField, Typography, keyframes, styled } from '@mui/material';
import { CustomisedLink } from '../../styles/navbar/navbar';
import axios from 'axios';
import { useAppDispatch } from '../../hooks/useCustomUsersType';
import { setUser } from '../../redux/reducers/authReducer';
import { fetchAllUsers, updateUserPassword } from '../../redux/reducers/usersReducer';
import { UserUpdate } from '../../types/UserUpdate';
import { Colors } from '../../styles/theme/mainTheme';
import { CustomisedForm } from '../../styles/Form';
import { uploadFile } from '../../redux/common';
import { PayloadAction } from '@reduxjs/toolkit';
import { CreateUser, ReadUser } from '../../types/UserCreate';

function UpdateUser() {
  const dispatch = useAppDispatch();
  const LoggedInUser = useCustomTypeSelector(state => state.authReducer);
  const details = LoggedInUser.user;
  
  const [updated, setUpdated] = useState(false);
  const [newPassword, setNewPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  
  const [userData, setUserData] = useState<ReadUser>({
    email: details.email,
    //password: details.password,
    name: details.name,
    role: details.role,
    avatar: details.avatar,
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
const token = useCustomTypeSelector(
  (state) => state.authReducer.accessToken
);
console.log("token is",token);
  const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name } = event.target;
    if(name==="avatar"){
      const file=event.target.files?.[0];
      if(file){
        const response=  await dispatch(uploadFile(file)) as PayloadAction<{location:string}>;
        const location = response.payload.location;
        setUserData((prevState) => ({ ...prevState, [name]: location }));
      }
    }
    else{
      const { value } = event.target;
    setUserData((prevState) => ({ ...prevState, [name]: value }));
  }};
  const handlePasswordChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { value } = event.target;
    setNewPassword(value);
  }
    // const updateUserFromApi = async () => {
  //   try {
  //     const response = await axios.put<User>(
  //       `https://api.escuelajs.co/api/v1/users/${userId}`, userData
  //     );
  //     dispatch(setUser(response.data));
  //     dispatch(fetchAllUsers());
  //     setUpdated(true);
  //   } catch (error) {
  //     console.error("Failed to fetch user profile:", error);
  //   }
  // };
  const handleSubmit = (event: FormEvent) => {
    event.preventDefault();
    // updateUserFromApi();
  };
  if (!LoggedInUser.accessToken) {
    return (
      <>
        <Typography variant="body2" align="center">
          Login to update your profile?{' '}
          <CustomisedLink to={"/Profile"}> Login </CustomisedLink>
        </Typography>
      </>
    )
  }
  return (
    <Box>
      {!updated && (<CustomisedForm  onSubmit={handleSubmit}>
        <TextField
          label="ID"
         // value={userId}
          disabled
          fullWidth
          margin="normal"
          
        />
        <TextField
          label="Name"
          name="name"
          value={userData.name}
          onChange={handleChange}
          fullWidth
          margin="normal"
          disabled
        />
        <TextField
          label="Email"
          name="email"
          value={userData.email}
          onChange={handleChange}
          fullWidth
          margin="normal"
          disabled
          type="email"
        />
        <TextField
          label="Password"
          name="password"
          //value={setNewPassword}
          onChange={handlePasswordChange}
          fullWidth
          margin="normal"
          required
          type="password"
        />
        {error && (
        <Typography variant="body2" color="error" gutterBottom>
          {error}
        </Typography>
      )}
         <Button type="button" onClick={() => {
                           const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
                           if (!passwordRegex.test(newPassword)) {
                             setError("Password must contain at least 8 characters with a combination of letters and numbers.");
                             return;
                           }
                           else{
                          dispatch(updateUserPassword({newPassword,token}));
                          setUpdated(true);
                           }
                        }} variant="contained" sx={{backgroundColor:Colors.black ,color:Colors.light_grey}}>
          Update your password
        </Button>
        {/* <Button type="submit" onClick={() => {
                          // dispatch(addToCart(product))
                          //dispatch(UpdateUser(userData.password));
                        }} variant="contained" sx={{backgroundColor:Colors.black ,color:Colors.light_grey}}>
          Update your profile
        </Button> */}
      </CustomisedForm >)}
      {updated &&
        <AnimatedBox>
          <Box sx={{ display: "flex", fontFamily: 'Raleway, Arial', justifyContent: "center", alignContent: "center", margin: "3em", padding: "2em", boxShadow: "2", borderRadius: "2", backgroundColor: Colors.primary }}>
            <Typography variant='h3'>
              YOUR PASSWORD HAS BEEN UPDATED
            </Typography>
          </Box>
        </AnimatedBox>
      }
    </Box>
  )
}

export default UpdateUser