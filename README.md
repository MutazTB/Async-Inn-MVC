# Async Inn 

## Description
This is a RESTful API server built using ASP.NET Core to allow Async Hotel management to better manage the assets in their hotels. This application can modify and manage rooms, amenities, and new hotel locations. The data entered by the user will persist across a relational database and maintain its integrity as changes are made to the system.

## **Async Inn ERD:**
![image](images/ERD.png)

* Hotel table has one to many relationship with HotelRoom table
* Room table has one to many relationship with HotelRoom table
* Amenities table has one to many relationship with RoomAmenities table
* Room table  has one to many relationship with RoomAmenities table
* RoomAmenities and HotelRoom tables are a Composite tables


## Endpoints

### Hotels

| Method | EndPoint | 
|:-|:-|
| GET | ```/api/Hotels``` | 
| POST | ```/api/Hotels``` |
| GET | ```/api/Hotels/{id}``` |
| PUT | ```/api/Hotels/{id}``` |
| DELETE | ```/api/Hotels/{id}``` |


### Rooms

| Method | EndPoint | 
|:-|:-|
| GET | ```/api/Rooms/``` | |
| POST | ```/api/Rooms/``` | |
| GET | ```/api/Rooms/{id}``` | |
| PUT | ```/api/Rooms/{id}``` | |
| DELETE | ```/api/Rooms/{id}``` | |


### Amenities

| Method | EndPoint | 
|:-|:-|
| GET | ```/api/Amenities``` | |
| POST | ```/api/Amenities``` | |
| GET | ```/api/Amenities/{id}``` | |
| PUT | ```/api/Amenities/{id}``` | |
| DELETE | ```/api/Amenities/{id}``` | |

### HotelRooms

| Method | EndPoint | 
|:-|:-| 
| GET | ```/api/Hotels/{hotelId}/Rooms``` | |
| POST | ```/api/Hotels/{hotelId}/Rooms``` | |
| GET | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |
| PUT | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |
| DELETE | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |


## DTO and Testing
Expanded further with DTO's which help to filter Database table information to be more presentable to a user. Routing of different DTO tables was possible through Navigation properties with in DTO's that would later have linq querys .Select made to gather desired data from other data tables.

## Identity 
Implemented `Identity Framework`, created DTO's for Login, Register, and User to filter sensative authentication information. Created an `IUser` interface and `IdentityUserService` service to handle the registration and login requests, and integrated that serivce into the UsersController where the Post requests are handled

## Roles
Created a new service class JWTTOkenService3, Registered the new service to our start up file, add the jwtTokenService as a dependency of IdUserService.
Setup a secret validation in the JWT service and added it to the app configuration.
After secret validation was possible, Authentication Service as add, and add them to startup. Developred Get/Create token for User login.
Created UserDto with unique tokens in IdUserService.
Now a token can be use to login instead of having to send a User name and password.
Introduced Policies in a away where they can be distrubited among many roles through permissions.