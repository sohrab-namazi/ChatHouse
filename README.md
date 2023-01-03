# ChatHouse - Back End

ChatHouse is a social media application which users can create intereset-based chatrooms.   

## Description

Chathouse can be placed in social media application category. by this app, you can create or join chatrooms which each one is created for a specefic purpose. <br /> Users can chose their interests so that the app suggests related rooms to them.
<br /> Users can follow each others, the interests of your followings and the rooms whcih they are joined, can affect the rooms that the application suggests to you. <br /> Real time interest-based chatrooms can be experienced in ChatHouse. 

## Getting Started

### Dependencies

*  This project can be run on any OS, but the following instructions are for Windows, you can easily run the equivalent instructions on other operating systems if you want.  
*  Have SQL-Server 2019 already installed on your system.
*  Download MinIO.exe from MinIO official website, set MINIO_ROOT_USER & MINIO_ROOT_PASSWORD as environment variables with the same values which they have in appsettings.json file of the project.
*  Create "Data" folder which MinIO will store the data in that. 
*  Download redis

### Installing

* Clone the project
* Make sure the dependencies are satisfied

### Executing program

* run the following command in command prompt to create the corresponding database for you
```
dotnet ef database update
```

* run the following command to start MinIO
```
minio.exe server [Path to Data folder]
```

* run the project by the following command:
```
dotnet run
```

* now the Swagger-UI is up, you can attempt to request. 

## Help

If you had any problem with running the project, any idea about making it better, etc, share it with the project collaborators.

## ChatHouse Front End Project
[ChatHouse-Frontend](https://github.com/ali-f-alfa/Sam-front)

## Authors
  
[@SohrabNamaziNia](https://github.com/SohrabNamaziNia)
[@ali-f-alfa](https://github.com/ali-f-alfa)
[@meliiwamd](https://github.com/meliiwamd)

![1](https://user-images.githubusercontent.com/46357028/199154460-618f8ccf-3d1f-4263-aca4-3310a56179c6.jpg)
![2](https://user-images.githubusercontent.com/46357028/199154609-d709c0f0-65d0-4f31-837f-a8bde5b7d264.jpg)
