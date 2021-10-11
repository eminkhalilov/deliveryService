# Delivery Service
Farfetch Delivery Service

# Description

This project is for Delivery service which is calculating and finding best route from one point to another point. I used here Graph algorithm for counting the routes. In this solution 2 APIs (Points and Routes) and one Identity Server (Authentication/Authorization) which described below.

# Technology stack, Frameworks and Tools

- C# programming langugage
- .Net Core 3.0
- Swagger for Web API
- Identity4Server for Authentication/Authorization
- Microsoft Sql Database
- NSubstitute for mocking in Unit testing
- xUnit framework for Unit testing
- Restful API
- Dapper
- EntityFramework Core (code first migration)

# Solution Layers

- Web Api (Farfetch.DeliveryService.WebApi) => Delivery service Web Api edpoints located here
- Test (Farfetch.DeliveryService.Tests) => All Unit tests located here
- Service (Farfetch.DeliveryService.Services) => All services and provders (business logic) located here
- Model (Farfetch.DeliveryService.Models) => All model classes located here
- Repository (Farfetch.DeliveryService.Repositories) => All database queries and commands from this place
- Authentication/Authorization (Farfetch.IdentityServer) => I use here identity4server for oAuth2 standard protocol

# Identity Server (Authentication/Authorization)

=> Farfetch.IdentityServer

For Authentication and Authorization I use here Identity4Server and in config I created two ClientId one with admin role another one is with user (will be good to use database for preserving them)

=============================
Admin role

ClientId: admin_client_id
Secret: adminfarfetchcsecret
Scope: deliveryservice

User role

ClientId: user_client_id
Secret: userfarfetchcsecret
Scope: deliveryservice

==============================

Before calling endpoints I use [Authorize] attribute for authorizing users. For Creating,Deleting and Updating only users with admin role can do and on those endpoints I use this [Authorize(Roles = "Admin")]


# Delivery Service Web Api

=> Farfetch.DeliveryService.WebApi

Web Api consists from 2 parts Points and Routes

Note: Only users with admin role can create, update and delete

Points APIs

Registering Point
- POST /api/v1/points/point

Updating Point
- PUT /api/v1/points/point

Deleting Point
- DELETE /api/v1/points/point

Getting all registered Points
- GET /api/v1/points

Routes APIs

Creating Route
POST /api/v1/routes/route

Updating Route
PUT /api/v1/routes/route

Deleting Route
DELETE /api/v1/routes/route

Getting all registered routes
GET /api/v1/routes

Getting all routes from source point to destination point
GET /api/v1/routes/{sourcepointid}/{destinationpointid}

Getting route with smallest time from source point to destination point
GET /api/v1/routes/route/time/{sourcepointid}/{destinationpointid}

Getting route with lowest cost from source point to destination point
GET /api/v1/routes/route/cost/{sourcepointid}/{destinationpointid}

# How to run the solution ?

Steps:

1. In Farfetch.DeliveryService.WebApi project in localsettings config file change Sql database connection string to your installed database
2. Start running Farfetch.DeliveryService.WebApi project 
- Database will be created authomatically  (code first migration Farfetch.DeliveryService.Migrator)
- Database schema be update authomatically (code first migration  Farfetch.DeliveryService.Migrator)
- Test values will be inserted in database (code first migration  Farfetch.DeliveryService.Migrator)
3. We have Web Api and Authenication/Authorization server so they are integrated. Please make sure that you run both of them. Multiple Project start is good for this
4. When you will run web api Swagger UI will open authomatically and you will see all endpoints there
5. If you want to avoid Authentication/Authorization just comment [Authorize] or [Authorize(Roles = "Admin")] attributes
example: //[Authorize] or //[Authorize(Roles = "Admin")]
6. For calling routes I use there points id which you can get from Points API
