# E-Commerce Web App

Welcome to the E-Commerce Web App, an e-commerce application built using ASP.NET Core Web API for the backend and Redux with Material UI for the frontend. This application combines the power of clean architecture, database storage, and a well-designed user interface.

## Features

### Backend Features (ASP.NET Core Web API)
- Built with clean architecture principles, ensuring code organization and maintainability.
- Utilizes Entity Framework Core to interact with the database, with Elephant SQL as the chosen database provider.
- Implements secure JSON Web Tokens (JWT) authentication for user login and registration.
- Implements role-based authorization and claim-based authentication to manage user access permissions.
- Provides a wide range of REST API endpoints for user management, product management, cart handling, and order placement.
- Includes administrative functionalities to efficiently manage products, users, and orders.

### Frontend Features (Redux with Material UI)
- Presents an intuitive and visually appealing user interface crafted using Material UI.
- Uses Redux for state management, guaranteeing effective data flow and scalability.
- Displays a comprehensive product list for customers to explore and view.
- Requires user authentication for adding products to the cart, enhancing security.
- Enables users to efficiently manage their shopping cart by adding, removing products, and confirming orders.
- Empowers admin users with a dedicated dashboard for seamless product creation, editing, and deletion.
- Admin dashboard also provides insights into customer orders and user account management.

## Usage
1. Visit the home page to explore the array of available products.
2. Either create a new account or log in to your existing account to start adding products to your cart (Admin cannot add products to the cart).
3. Access the cart section to manage items, adjust quantities, and finalize orders.
4. Admin users can access the admin dashboard for streamlined product, order, and user management.
   
## Admin Account Details
- Email: mummy@gmail.com
- Password: mummy123

## Technologies Used

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- Clean Architecture

### Frontend
- React
- Redux
- Material UI

### Database
- Azure PostgreSQL 


![ERD](erd.png)


## Deployment
The backend of the application is hosted on Azure, and the frontend is hosted on Netlify.

- Frontend URL: [🌐 Netlify - E-Commerce Web App](https://shopnshop.netlify.app/)
  
- Backend API Documentation: [🌐 Azure Swagger Documentation](https://shop-and-shop.azurewebsites.net/swagger/index.html)




Thank you for choosing our E-Commerce Web App. We hope you have a seamless shopping experience!

