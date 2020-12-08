# readlater
Overview

Read Later is a fictional service designed to allow users to bookmark webpages they want to read later. It has been designed to follow general good practices within coding and to test your knowledge of these. The solution uses a multi tiered approach, with POCO entities being used to represent objects in the system, a Repository pattern to manage the data access, and a service layer to handle all functionality.

Visual Studio 2017 solution

The solution consists of 5 projects:

Data

The data project handles the communication with the database and is using Entity Framework (Code first approach). Migrations are enabled already - to initialise your database you will need to enter a connection string to Sql Server in app.config, then in package manager console ensure that Data is the selected project and enter the command update-database. this will create the database (if it is not there already), and generate the tables based on the existing entities.

Entities

The Entities project contains the POCO classes that represent the system objects. These inherit from EntityBase which implements the IObjectState interface, giving us a way of explicitly marking the classes as modified / added etc

Repository

The Repository layer provides data access functionality to the service layer. A generic repository class is used to prevent the need for separate repositories for each Entity, unless very specialised functionality is required. A helper class is also provided (RepositoryQuery) which enables queries to be built up in a fluent manner

Services

Services expose the functionality to the UI layer.

MVC

The web layer is an ASP.net MVC 5 project using the standard templates provided. Dependency Injection is managed from here and the DI / IoC framwork used is Simple Injector (https://simpleinjector.codeplex.com/) . It is very straightforward to use and all configuration takes place in app_start/SimpleInjectorInitializer.cs


Quick Start

1. Open solution in Visual Studio 2017
2. Update the connection string in data/app.config and ReadLaterWeb/web.config to point to a Sql Server / Sql Server Express instance (no need to create a database)
3. In Package Manager Console, ensure the Data project is selected and run the command 'update-database'
4. Your database will be created if necessary, and the tables created

Please feel free to contact me if you have any questions, I am on email or my Skype is rob.harlow

Thanks
