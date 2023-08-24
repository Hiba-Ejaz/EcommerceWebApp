import { Outlet, Navigate } from 'react-router-dom'
import useCustomTypeSelector from './hooks/useCustomTypeSelector'
import CreateProduct from './components/Product/CreateProduct';

const PrivateRoutes = () => {
    const LoggedInUser = useCustomTypeSelector((state) => state.authReducer);
    const LoggedInUserRole = LoggedInUser.user.role;
    return(
        (LoggedInUserRole==="Admin") ? <Outlet/> : <Navigate to="/Profile"/>
    )
}

export default PrivateRoutes


 