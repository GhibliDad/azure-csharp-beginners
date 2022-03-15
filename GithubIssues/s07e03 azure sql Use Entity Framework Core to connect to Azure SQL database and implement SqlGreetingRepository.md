Finally time to implement a `SqlGreetingRepository` that reads and writes `Greeting` objects to `Azure SQL`. When interacting with a database there are typically two types of interactions:
 - Database management operations: Modify tables, columns, datatypes, indexes etc
 - Data operations: Create/Read/Update/Delete operations (a.k.a. CRUD). These operations are sent to the database as `queries`.

It's possible to send raw `T-SQL` queries to the database from our application. It's also possible to use a framework that translates our programming model into `T-SQL`. EF Core is one example of such a framework and in this exercise we'll use it to interact with Azure SQL.

We'll use `Code First` and `Migrations` in EF Core first create the database model in our application (i.e. the `Greeting` class) and use tools within EF Core to create the corresponding database model (tables/columns) for us. 

We'll use LINQ to query the database, letting EF Core translate our LINQ code into `T-SQL` under the hood. This allows us to write our data access code in a language that we're familiar with. Though a senior developer should also have good understanding of `T-SQL` though we will not cover `T-SQL` in detail in this course.

Database access code usually becomes quite complex and messy over time. This is a huge domain and in this course we'll only scratch the surface. There are many ways to implement this, the method shown here is one of the easiest ways to get started.

#### EF Core resources
EF Core:
https://docs.microsoft.com/en-us/ef/core/
https://entityframeworkcore.com

EF Core Code First:
https://entityframeworkcore.com/approach-code-first

EF Core Migrations:
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

#### Basic `T-SQL` queries for querying data in the `greetings` table:
##### Insert a row:
```sql
insert greetings values (newid(), 'hello', 'Keen', 'Anton', getdate())
```
`newid()` is a built in function that generates a new `guid`
`getdate()` is a build in function that gets the current date similar to c# `DateTime.Now` or `DateTime.UtcNow`

##### Update a row:
```sql
update greetings set message = 'How are you' where id = '2ff67329-32e4-45e5-9f07-329f30bdf61b'
```

##### Select rows:
```sql
select * from greetings where [from] = 'Keen'
```
`from` is a built in key word in SQL, to be able to query columns named `from` we need to surround it with brackets like this `[from]`

##### Delete rows:
```sql
delete from greetings where id = '2ff67329-32e4-45e5-9f07-329f30bdf61b'
```

### Goal
* Azure SQL
* Entity Framework Core

### Steps
1. Install these nuget packages to the project `GreetingService.Infrastructure`, these nuget packages are used to help us send queries to the database:
    - `Microsoft.EntityFrameworkCore` 
    - `Microsoft.EntityFrameworkCore.SqlServer`
    - `Microsoft.EntityFrameworkCore.Tools`
2. Install these nuget packages to the project `GreetingService.API.Function`, these nuget packages are used to help us create tables etc in the database:
    - `Microsoft.EntityFrameworkCore.Design`
3. Create a new class `GreetingDbContext` and make it inherit from `DbContext`
    - `DbContext` is the primary component we will use to send queries to the database
4. Add this constructor to `GreetingDbContext`. This constructor makes it possible for us to configure the `connection string` in dependency injection:
```c#
public GreetingDbContext(DbContextOptions options) : base(options)
        {
        }
```

5. Also add this empty constructor to `GreetingDbContext`. This constructor will be used by EF Core tools to update the database models
```c#
        public GreetingDbContext()
        {
        }
```

6. Add this property: `public DbSet<Greeting> Greetings { get; set; }` to `GreetingDbContext`. We will use this property to query the database with `LINQ` in `EF Core`
    - A `DbSet<T>` is equivalent to a table in the database
7. Add this `override` method to `GreetingDbContext`, we need this to be able to run the `EF Core` tools to create the tables needed in the database:
```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("GreetingDbConnectionString"));
        }
```
8. Create an `Environment Variable` in Windows with `name`:  `GreetingDbConnectionString` and set the value to the connection string of your database. (DB connection string can be retrieved from the `SQL Database` resource in Azure portal, don't forget to replace the password placeholder with your actual password) 
    - Right click on `This PC` in `File Explorer` and click on `Properties`
![image](https://user-images.githubusercontent.com/2921523/153290706-6cc3e082-4294-4724-9b8b-d1d8793769ba.png)

    - Click on `Advanced System Settings`
![image](https://user-images.githubusercontent.com/2921523/153292452-f59954eb-0ed9-4f06-b457-f887f6606569.png)

    - Click on `Environment Variables`
![image](https://user-images.githubusercontent.com/2921523/153292517-a14dede7-2c16-4024-a9a5-a06420e5eeb1.png)

    - Add the environment variable to `User Variables`
![image](https://user-images.githubusercontent.com/2921523/153292689-f9918de5-6410-4d30-a1c0-988f8d0953be.png)

9. Open `Package Manager Console` in Visual Studio from the menu: `Tools` -> `NuGet Package Manager` -> `Package Manager Console`
![image](https://user-images.githubusercontent.com/2921523/153293360-817cd6e1-a5ba-4aca-b573-6d1367a073a3.png)

10. In the console: type the command `Add-Migration InitialCreate -Project GreetingService.Infrastructure` to create a `EF Core migration`. This packages any code changes in our models (`Greeting` class) that need to be reflected in the database

11. In the console: type the command `update-database -project GreetingService.Infrastructure` to send the migrations to the database. This command will create/update the tables and columns in the database according to our model.

12. Log in to the database using Azure SQL and verify that the `Greetings` table has been created
<img width="349" alt="image" src="https://user-images.githubusercontent.com/2921523/153294881-e74c3319-cbc5-40de-8e0b-f7648956c287.png">

13. Register `SqlGreetingRepository` as `IGreetingRepository` in dependecy injection using `AddScoped<>`.

14. Register `GreetingDbContext` as a `DbContext` using this code in dependency injection:
```c#
            builder.Services.AddDbContext<GreetingDbContext>(options =>
            {
                options.UseSqlServer(config["GreetingDbConnectionString"]);
            });
```

15. Add a `GreetingDbContext` parameter to the constructor in `SqlGreetingRepository` and save it to a private field `_greetingDbContext`
16. Implement all interface methods in `SqlGreetingRepository` using `_greetingDbContext.Greetings`
    - Check out the examples here: https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#create-read-update--delete

### Example output
<img width="700" alt="image" src="https://user-images.githubusercontent.com/2921523/153296672-11c4bd6a-bc0c-4bce-88d0-03809864d689.png">
