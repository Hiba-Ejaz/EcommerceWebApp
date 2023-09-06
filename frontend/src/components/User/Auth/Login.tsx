import axios from "axios";
import { useEffect, useState } from "react";
import { useAppDispatch } from "../../../hooks/useCustomUsersType";
import useCustomTypeSelector from "../../../hooks/useCustomTypeSelector";
import {
  logout,
  setAccessToken,
  setUser,
} from "../../../redux/reducers/authReducer";
import { Link } from "react-router-dom";
import { CustomisedLink } from "../../../styles/navbar/navbar";
import { Box, Button, TextField, Typography } from "@mui/material";
import { User } from "../../../types/User";
import { Colors } from "../../../styles/theme/mainTheme";
import { CreateUser, ReadUser } from "../../../types/UserCreate";
import { Console } from "console";

interface LoginResponse {
  access_token: string;
 // refresh_token: string;
}

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const dispatch = useAppDispatch();
  const user = useCustomTypeSelector((state) => state.authReducer.user);
  const accessToken = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
  useEffect(() => {
    if (accessToken) {
      handleFetchUserProfile();
    }
  }, [accessToken]);
  const handleLogin = async () => {
    try {
      console.log("going to auth for getting token");
      const response = await axios.post<string>(
      //  "https://api.escuelajs.co/api/v1/auth/login",
      "https://shop-and-shop.azurewebsites.net/api/v1/auth",
        {
          email,
          password,
        }
      );
      const  access_token  : string = response.data;
   // const { access_token } = response.data.access_token;
      console.log("response is this", response.data);
      console.log("access_token yeh aaya hai", access_token);
      dispatch(setAccessToken(access_token));
      setError("");
    } catch (error) {
      setError("User doesnot exists");
    }
  };
  const handleLogout = () => {
    dispatch(logout());
  };
  const handleFetchUserProfile = async () => {
    console.log("going to fetch user profile");
    try {
      if (!accessToken) {
        console.error("Access token is missing");
        return;
      }
      else{
        console.error("Access token is present");
      }
      const response = await axios.get<ReadUser>(
        "https://shop-and-shop.azurewebsites.net/api/v1/profile",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      console.error("response is",response.data);
      dispatch(setUser(response.data));
    } catch (error) {
      console.error("Failed to fetch user profile:", error);
    }
  };
  return (
    <div>
      {!user.email && (
        <Box
          display="flex"
          flexDirection="column"
          justifyContent="center"
          alignItems="center"
          minHeight="60vh"
          bgcolor="#f5f5f5"
          padding={2}
          boxSizing="border-box"
        >
          <Typography variant="h2" align="center">
            Login Page
          </Typography>
          <form
            style={{ width: "100%", maxWidth: 400 }}
            onSubmit={(e) => {
              e.preventDefault();
              handleLogin();
            }}
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
              fullWidth
              size="large"
              sx={{ marginTop: 2 ,backgroundColor:Colors.black,color:Colors.light_grey}}
            >
              {" "}
              Loginnnn
            </Button>
          </form>
        </Box>
      )}
      <>
        {user.email && (
          <Box>
            <Box marginTop={4} textAlign="center">
              <Typography variant="h2">Welcome, {user.name}!</Typography>
              <Typography variant="body1" gutterBottom>
                Email: {user.email}
              </Typography>
              <Typography variant="body1" gutterBottom>
                Role: {user.role}
              </Typography>
              <img src={user.avatar} alt="User Avatar" height="200em" />
              <Box>
                <Button
                  variant="contained"
                  sx={{marginTop: 2 ,backgroundColor:Colors.black ,color:Colors.light_grey}}
                  size="large"
                  onClick={handleLogout}
                >
                  Logout
                </Button>
              </Box>
            </Box>
          </Box>
        )}
      </>
      <Box margin={"2em 0 4em 0"}>
        <Typography variant="body2" align="center">
          Don't have an account yet?{" "}
          <CustomisedLink to={"/SignUp"}> Sign up</CustomisedLink>
        </Typography>
        <Typography variant="body2" align="center">
          Update you account?{" "}
          <CustomisedLink to={"/UpdateUser"}> Update User</CustomisedLink>
        </Typography>
      </Box>
    </div>
  );
};

export default Login;
