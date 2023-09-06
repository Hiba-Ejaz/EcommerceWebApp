import {
  Button,
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import { useAppDispatch } from "../../hooks/useCustomUsersType";
import useCustomTypeSelector from "../../hooks/useCustomTypeSelector";
import { deleteUser, displayAllUsers } from "../../redux/reducers/usersReducer";
import { useEffect, useState } from "react";

function Users() {
  const dispatch = useAppDispatch();
  const [isDeleting, setIsDeleting] = useState(false);
  const [isFetching, setIsFetching] = useState(false);
  const token = useCustomTypeSelector((state) => state.authReducer.accessToken);
  const userList = useCustomTypeSelector(
    (state) => state.usersReducer.usersList
  );
  const handleDeleteUser = async (userId: string) => {
    setIsDeleting(true);
    try {
      await dispatch(deleteUser({ userId: userId, token: token }));
    } catch (error) {
      console.error("Error deleting user:", error);
    } finally {
      setIsDeleting(false);
      setIsFetching(true);
      try {
        await dispatch(displayAllUsers(token));
      } catch (error) {
        console.error("Error fetching users:", error);
      } finally {
        setIsFetching(false);
      }
    }
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Admin Dashboard
      </Typography>
      {(isFetching || isDeleting) && <p>Deleting...</p>}
      <div>
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Email</TableCell>
                <TableCell>Role</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {userList.map((user) => (
                <TableRow>
                  <TableCell>{user.id}</TableCell>
                  <TableCell>{user.name}</TableCell>
                  <TableCell>{user.email}</TableCell>
                  <TableCell>{user.role}</TableCell>
                  <TableCell>
                    <Button
                      variant="outlined"
                      color="error"
                      onClick={() => handleDeleteUser(user.id)}
                    >
                      Delete
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </Container>
  );
}
export default Users;
