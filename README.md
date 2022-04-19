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