import React, { useState, FormEvent } from 'react';
import { TextField, Button, Typography, useTheme } from '@mui/material';
import { useDispatch } from 'react-redux';
import { useAppDispatch } from '../../hooks/useCustomUsersType';
import { createNewUser } from '../../redux/reducers/usersReducer';
import { User } from '../../types/User';
import { CreateUser } from '../../types/UserCreate';
import axios from 'axios';
import useCustomTypeSelector from '../../hooks/useCustomTypeSelector';
import { useNavigate } from "react-router-dom";
import theme, { Colors } from '../../styles/theme/mainTheme';
import { CustomisedForm } from '../../styles/Form';
import { uploadFile } from '../../redux/common';
import { PayloadAction } from '@reduxjs/toolkit';

const UserDataForm = () => {
  const [userData, setUserData] = useState<CreateUser>({
    name: '',
    email: '',
    password: '',
    avatar: ''
  });

  type LocationObject={
    location:string
  }
  type isAvailableResponse = {
    isAvailable: boolean;
  }
  const [error, setError] = useState<string | null>(null);
  const users = useCustomTypeSelector(state => state.usersReducer.users);
  const dispatch = useAppDispatch();
  const userCreated = useCustomTypeSelector(state => state.usersReducer.userCreated);
  const handleChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name,value } = event.target;
    if(name==="avatar"){
      // const file = event.target.files?.[0];
      // if (file) {
      //  const response=  await dispatch(uploadFile(file)) as PayloadAction<{location:string}>;
      //  const location = response.payload.location;
       setUserData((prevState) => ({ ...prevState, [name]:value  }));
          }
       //}
    else{ 
      const { name, value } = event.target;
    setUserData((prevState) => ({ ...prevState, [name]: value }));
    }
  };
  const navigate = useNavigate();
  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    const { email,name,password } = userData;
    if (!name || !email || !password) {
      setError("Name, email, and password are required fields.");
      return;
    }
    const emailRegex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
    if (!emailRegex.test(email)) {
      setError("Email must be in the format 'abc@gmail.com'.");
      return;
    }
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;
    if (!passwordRegex.test(password)) {
      setError("Password must contain at least 8 characters with a combination of letters and numbers.");
      return;
    }
    const userExists = users.some((user) => user.email === email);
    if (userExists) {
      setError("email already in use");
    } else {
      try {
        await dispatch(createNewUser(userData));
        navigate("/Profile");
      } catch (error) {
        console.log("error occured while creating a new user",error);
      }
    }
  };

  const theme = useTheme();
  return (
    <CustomisedForm onSubmit={handleSubmit} >
      <TextField
        label="Name"
        name="name"
        value={userData.name}
        onChange={handleChange}
        fullWidth
        margin="normal"
        required
      />
      <TextField
        label="Email"
        name="email"
        value={userData.email}
        onChange={handleChange}
        fullWidth
        margin="normal"
        required
        type="email"
      />
      {error && (
        <Typography variant="body2" color="error" gutterBottom>
          {error}
        </Typography>
      )}
      <TextField
        label="Password"
        name="password"
        value={userData.password}
        onChange={handleChange}
        fullWidth
        margin="normal"
        required
        type="password"
      />
       <label>Add Image Url</label>
            <TextField
              fullWidth
              type="text"
              name="avatar"
              onChange={handleChange}
              value={userData.avatar}
            />
      {/* <input 
        type="file"
        name="avatar"
        onChange={handleChange}
      /> */}
      <Button type="submit" variant="contained" sx={{backgroundColor:Colors.black ,color:Colors.light_grey}}>
        Submit
      </Button>
    </CustomisedForm>
  );
};

export default UserDataForm;
function useEffect(arg0: () => void, arg1: boolean[]) {
  throw new Error('Function not implemented.');
}

