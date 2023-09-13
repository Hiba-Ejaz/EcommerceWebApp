
# Fullstack E-commerce Application
## Description
This is a fullstack e-commerce application built using Redux Toolkit with TypeScript and Material-UI for the frontend. It makes use of the Platz API for the backend. The application allows users to browse and purchase products online, with different functionalities depending on their user role.

The main features and functionalities of the application include:
Product Listing: The application displays a list of all products fetched from the Platz API. This list can be viewed by anyone visiting the website.

### User Roles: 
There are two types of users in the application: customers and admins. Customers can browse products, add them to the cart, and proceed to checkout. Admins have additional privileges such as creating, updating, and deleting products.

### User Registration:
 Users need to sign up to register as new users in the application. This allows them to access personalized features such as managing their profile and making purchases.

### User Authentication: 
Users are required to log in before proceeding to certain actions, such as checking out or updating their profile. JWT (JSON Web Token) is used for user authentication.

### Cart Management: 
The application maintains a cart to track the selected products for purchase. Users can add products to their cart, view the cart contents, and proceed to checkout.
The application was developed using Visual Studio Code as the code editor. It incorporates various technologies and concepts, including React, Redux Toolkit, TypeScript, Material-UI, JWT for authentication, and API integration with the Platz backend.

## Installation
To install and run the application locally, follow these steps:

Clone the repository to your local machine.
Navigate to the project directory.
Install the required dependencies by running the command: npm install.
Set up the configuration files and environment variables as per the instructions provided.
Start the development server by running the command: npm start.
## Usage
Once the application is running, you can access it by opening your web browser and navigating to the provided URL. Here are some common usage scenarios:

Browsing Products: Users can browse the available products by visiting the homepage or the product listing page. They can view product details, such as descriptions and prices.

User Registration: New users can sign up for an account to access personalized features. They will need to provide the required information and follow the registration process.

User Authentication: Users can log in to their accounts to access restricted features, such as checking out or updating their profile. They will need to provide their credentials and receive a valid JWT for authentication.

Cart Management: Users can add products to their cart, view the cart contents, and proceed to checkout. The cart is maintained throughout the user's session.


### The following third-party libraries and APIs were used in this project:

Redux Toolkit - State management library for React applications.
TypeScript - Programming language for type-safe JavaScript development.
Material-UI - React component library for building user interfaces.
Platz API - Backend API for product data and user management.

The project is deployed and can be accessed [here] (shopnshop.netlify.app)

## PROJECT STRUCTURE
src
    │   App.tsx
    │   filter.json
    │   index.css
    │   index.tsx
    │   PrivateRoutes.tsx
    │   react-app-env.d.ts
    │   reportWebVitals.ts
    │   setupTests.ts
    │
    ├───animation
    │       animations.ts
    │
    ├───components
    │   ├───Product
    │   │   │   CreateProduct.tsx
    │   │   │   ProductDetail.tsx
    │   │   │   Products.tsx
    │   │   │   UpdateProduct.tsx
    │   │   │
    │   │   └───cart
    │   │           Cart.tsx
    │   │
    │   ├───UI
    │   │   │   Actions.tsx
    │   │   │   NavBar.tsx
    │   │   │   NavBarDesktop.tsx
    │   │   │   NavBarMobile.tsx
    │   │   │
    │   │   └───Banner
    │   │           Banner.tsx
    │   │
    │   └───User
    │       │   Profile.tsx
    │       │   SignUp.tsx
    │       │   UpdateUser.tsx
    │       │
    │       └───Auth
    │               Login.tsx
    │
    ├───hooks
    │       useCustomTypeSelector.ts
    │       useCustomUsersType.ts
    │       useDataFilterHook.ts
    │
    ├───images
    │       ban.png
    │       banner.webp
    │
    ├───redux
    │   │   common.ts
    │   │   store.ts
    │   │
    │   └───reducers
    │           authReducer.ts
    │           cartReducer.ts
    │           productsReducer.ts
    │           usersReducer.ts
    │
    ├───styles
    │   │   Form.ts
    │   │
    │   ├───bannerStyle
    │   │       bannerStyle.ts
    │   │       
    │   ├───navbar
    │   │       navbar.ts=
    │   │
    │   ├───products
    │   │       productsDisplay.ts
    │   │
    │   └───theme
    │           mainTheme.ts
    │
    └───types
            Category.ts
            NewProduct.ts
            Product.ts
            User.ts
            UserCreate.ts
            UserUpdate.ts