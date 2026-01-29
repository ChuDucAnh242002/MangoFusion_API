# MangoFusion_API

This is a simple .NET -based API that has simple API endpoints for managing Mango E-commerce store.

Here are brief summary of the all the API endpoints:

Authentication
POST /api/Auth/register - Register a new user
POST /api/Auth/login - Authenticate user and obtain token

Auth Test
GET /api/AuthTest - Test authentication
GET /api/AuthTest/{someValue} - Test authentication with admin role and parameter

Menu Items
GET /api/MenuItem - Get all menu items
POST /api/MenuItem - Create a new menu item
PUT /api/MenuItem - Update menu item
DELETE /api/MenuItem - Delete menu item
GET /api/MenuItem/{id} - Get specific menu item

Order Details
PUT /api/Orderdetails/{orderDetailsId} - Update order details

Order Headers
GET /api/OrderHeader - Get all order headers
POST /api/OrderHeader - Create new order header
GET /api/OrderHeader/{orderId} - Get specific order
PUT /api/OrderHeader/{orderId} - Update specific order
