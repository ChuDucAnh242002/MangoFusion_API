# MangoFusion_API

This is a simple .NET -based API that has simple API endpoints for managing Mango E-commerce store.

## Technology used

Frontend: React.js, Redux
Backend: .NET, C#
Database: MSSQL

# How to run the project

To run the frontend, go to mangopedia-react folder, install dependencies and run the program with command

```
cd mangopedia-react

npm install

npm run dev
```

The frontend will be accessible through http://localhost:5173/

To run the backend, go to MangoFusion_API in Visual Insider and click run button with HTTPS.

The backend will be accessible through https://localhost:7227/

You can access the backend url and view all the API endpoints through scalar

# Interface

![Mango fusion interface](./img/Mango_fusion.png)

# API endpoints

Here are brief summary of the all the API endpoints:

| Method |               Endpoint               | Description                        |
| ------ | :----------------------------------: | ---------------------------------- |
| POST   |         `/api/Auth/register`         | Register a new user                |
| POST   |          `/api/Auth/login`           | Authenticate user and obtain token |
| GET    |           `/api/AuthTest`            | Test authentication endpoint       |
| GET    |     `/api/AuthTest/{someValue}`      | Test authentication with parameter |
| GET    |           `/api/MenuItem`            | Retrieve all menu items            |
| POST   |           `/api/MenuItem`            | Create a new menu item             |
| PUT    |           `/api/MenuItem`            | Update existing menu item(s)       |
| DELETE |           `/api/MenuItem`            | Delete menu item(s)                |
| GET    |         `/api/MenuItem/{id}`         | Get specific menu item by ID       |
| PUT    | `/api/Orderdetails/{orderDetailsId}` | Update order details               |
| GET    |          `/api/OrderHeader`          | Get all order headers              |
| POST   |          `/api/OrderHeader`          | Create a new order header          |
| GET    |     `/api/OrderHeader/{orderId}`     | Get specific order by ID           |
| PUT    |     `/api/OrderHeader/{orderId}`     | Update specific order              |
