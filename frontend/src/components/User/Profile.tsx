import React, { useState } from "react";
import axios from "axios";
import { useDispatch } from "react-redux";
import { setUser, setAccessToken } from "../../redux/reducers/authReducer";
import { Typography, Box, TextField, Button } from "@mui/material";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import { logout } from "../../redux/reducers/authReducer";
import { CustomisedLink } from "../../styles/navbar/navbar";
import { User } from "../../types/User";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";

interface LoginResponse {
  access_token: string;
  refresh_token: string;
}

const Profile = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const dispatch = useDispatch();
  const user = useCustomTypeSelector((state) => state.authReducer.user);
  const accessToken = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
  const handleLogin = async () => {
    if (!email || !password) {
      setError("Please fill in all fields.");
      return;
    }
    try {
      console.log("going to fetch token");
      const response = await axios.post<LoginResponse>(
        "https://shop-and-shop.azurewebsites.net/api/v1/auth/login",
        {
          email,
          password,
        }
      );
      const { access_token } = response.data;
      console.log("token:", access_token);
      dispatch(setAccessToken(access_token));
      handleFetchUserProfile();
    } catch (error) {
      console.error("Login failed:", error);
      setError("User doesnot exists");
    }
  };

  const handleLogout = () => {
    dispatch(logout());
  };

  const handleFetchUserProfile = async () => {
    try {
      console.log("going to fetch profile");
      if (!accessToken) {
        console.error("Access token is missing");
        return;
      }
      const response = await axios.get<User>(
        "https://shop-and-shop.azurewebsites.net/api/v1/auth/profile",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      dispatch(setUser(response.data));
      console.log("User Profile:", response.data);
    } catch (error) {
      console.error("Failed to fetch user profile:", error);
    }
  };

  return (
    <Box
      display="flex"
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
      minHeight="100vh"
      bgcolor="#f5f5f5"
      padding={2}
      boxSizing="border-box"
    >
      {!user.email && (
        <Box
          display="flex"
          flexDirection="column"
          justifyContent="center"
          alignItems="center"
          minHeight="100vh"
          bgcolor="#f5f5f5"
          padding={2}
          boxSizing="border-box"
        >
          <Typography variant="h2" align="center" gutterBottom>
            Login Page
          </Typography>
          <form
            onSubmit={(e) => {
              e.preventDefault();
              handleLogin();
            }}
            style={{ width: "100%", maxWidth: 400 }}
          >
            <TextField
              type="email"
              label="Email"
              variant="outlined"
              fullWidth
              margin="normal"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <TextField
              type="password"
              label="Password"
              variant="outlined"
              fullWidth
              margin="normal"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            {error && (
              <Typography variant="body2" color="error" gutterBottom>
                {error}
              </Typography>
            )}
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              size="large"
              sx={{ marginTop: 2 }}
            >
              Login
            </Button>
          </form>
        </Box>
      )}
      {user.email && (
        <Box marginTop={4} textAlign="center">
          <Typography variant="h2">Welcome, {user.name}!</Typography>
          <Typography variant="body1" gutterBottom>
            Email: {user.email}
          </Typography>
          <Typography variant="body1" gutterBottom>
            Role: {user.role}
          </Typography>
          <img src={user.avatar} alt="User Avatar"  style={{ width: '20px', height: '20px' }} />
          <Box>
            <Button
              variant="contained"
              color="secondary"
              size="large"
              onClick={handleLogout}
              sx={{ marginTop: 2 }}
            >
              Logout
            </Button>
          </Box>
        </Box>
      )}
      <Button
        variant="outlined"
        color="primary"
        size="large"
        onClick={handleFetchUserProfile}
        sx={{ marginTop: 4 }}
      >
        Fetch User Profile
      </Button>
      <Typography variant="body2" align="center" sx={{ marginTop: 4 }}>
        Don't have an account yet?{" "}

        <CustomisedLink  sx={{justifyContent:"center",textAlign: "center",width:"100%"}} to={"/SignUp"}>Sign up</CustomisedLink>
      
      </Typography>
      <Typography variant="body2" align="center">
        Update your account?{" "}
        <CustomisedLink to={"/UpdateUser"}>Update User</CustomisedLink>
      </Typography>
    </Box>
  );
};

export default Profile;
