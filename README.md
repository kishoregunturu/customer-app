> An Angular Appliction for Customer Crud with .Net Core 6.0 API

## The Angular application is located under Customer-Angular-Crud
## The .Net Core 6.0 uses EF Core with SQlLite Database


The Project highlights the following under Angular:
  - Crud for Customer 
  - Paging and Caching for Customer
  - Lazy Laoding of the grid 
  - Akita State management for State management
  - Customer module for plug and play
  
For angular project to work, update the environment.ts file for the environment.
```
export const environment = {<br>
    production: false,<br>
   apiUrl: 'https://localhost:7071'<br>
};
```

For full end to end Integration, we need .net Core Rest AI working. 
The .net Core API has the following requirements:

- Visual Studio 2022
- .Net Core 6.0
- Sql Lite
  -- Copy the SqlLite from C:\repos\customer-app\Customer.API\Customer.API\Data\Customers.db into your local folder
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
  
