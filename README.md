> An Angular Appliction for Customer Crud with .Net Core 6.0 API

## The Angular application is located under Customer-Angular-Crud
## The .Net Core 6.0 uses EF Core with SQlLite Database


The Project highlights the following under Angular:
  - Crud for Customer 
  - Paging and Caching for Customer
  - Lazy Loading of the grid 
  - Akita State management for State management
  - Customer module for plug and play
  - Login and session
  - JWT Authorization integration for .Net Core
  
For angular project to work, update the environment.ts file for the environment.
```
export const environment = {<
    production: false,
   apiUrl: 'https://localhost:7071'
};
```

For full end to end Integration, we need .net Core Rest AI working. 
The .net Core API has the following requirements:

- Visual Studio 2022
- .Net Core 6.0
- Sql Lite
  -- Copy the SqlLite from \customer-app\Customer.API\Customer.API\Data\Customers.db into your local folder
  -- Update the appsettings.json to folder
  ```
  "ConnectionStrings": {
    "DB": "C:\\customer\\Customer.API\\Customer.API\\Data\\Customers.db"
  },
  ```
  
  Run the .net Core project first. The .net core 6.0 also has Swagger integrated.
  ![image](https://user-images.githubusercontent.com/8276312/229308617-f283ac5f-474d-4d63-8477-1e40698951cb.png)

 Make sure the baseUrl is updated in Angular applicaiton.
 
 Run the angular using the following :
 - npm install
 - update environment.ts to update api urlk
 - ng serve
 
 - login 
 
 ![image](https://user-images.githubusercontent.com/8276312/229323727-862c19e7-f5fb-4d09-8765-8f93f00bc88a.png)
- Home page

 ![image](https://user-images.githubusercontent.com/8276312/229323752-bf15538f-0fa0-486b-8983-92413a75ec9a.png)

 
 - Create to create a new customer
 
 ![image](https://user-images.githubusercontent.com/8276312/229323764-8cdfc7cb-1235-4b14-8a0c-c86d069d6554.png)
 
 - Update existing customer
 
 ![image](https://user-images.githubusercontent.com/8276312/229323798-19a3f15c-e2f4-4bd6-a752-f3f961409861.png)
 
 - Last Active customer
 
 ![image](https://user-images.githubusercontent.com/8276312/229323815-24445247-5cb5-451e-9dd1-4621ff961508.png)




  
