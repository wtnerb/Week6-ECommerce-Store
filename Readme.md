# Week 6 Lab: Build an E-Commerce Store

## Overview
This is a multi-day assignment. Take the topics from each day of lecture, and apply them to your 
personal e-commerce store. Be sure to read the directions in it's entirety before beginning.

<br />
Pay close attention to the naming conventions specified. 

### Day 1 - Intro to Identity

- Create a brand new empty web application in Visual Studio. 
- Enable/Include the Identity Framework and all of it's required components. 
	- Enable Identity in the `ConfigureServices`, and add authentication to your `Configure` method
	- Create an `ApplicationUser` that derives from `IdentityUser`
	- Create an `ApplicationDbContext` that derives from IdentiyDbContext
	- Register the `ApplicationDbContext` in the `Startup.cs` File. 

- Create both a *Login* and *Register* page for your site. These actions should live in an *AccountController*.
	- Bring in `UserManager<ApplicationUser>` and `SignInManager<ApplicationUser>` into your controller through D.I. 

- If Time allows, start on creating the CSS/Frontend Design of your e-commerce site. Start on this early, as it will be a 
burnden to complete if you wait until the end of the project. 

## Day 2 - Claims and Application Roles
Adding onto your previous day's lab...
- Add default Application Roles (Admin and Member) to your application upon startup (seeding)
- Seed the Database with default products for your store (You will need to create a new DBContext, and register it separately in your application)
- Upon Registration add onto your user a variety of claims
- Assign users upon registration to a "MEMBER" user role. 
- Create a controller called *ProductsController* that allows you to do the standard CRUD operations on Products (you are allowed to scaffold, if you wish).
	- Make this Products Controller accessible only by Authorized/Logged in users
	- Make the Register and the Login Actions in the AccountController accessible by anonymous users
- Upon login, If the User is a member, redirect them to a Shopping landing page (you will have to create this)
- Upon login, If the user is an Admin, Redirect them to an Admin Dashboard that displays all of the products that are 
currently in the inventory. 

- 


