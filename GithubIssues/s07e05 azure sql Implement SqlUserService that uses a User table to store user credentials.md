Create a proper `User` table for storing users in our `GreetingService`. 

A `User` should have the following properties:
* first name (`string`)
* last name (`string`)
* email (`string`)
* password (`string`)
* created (`DateTime`)
* modified (`DateTime`)

In this exercise we'll store password in the db in clear text, this is not a good practice in read life. In real life the password would be stored with a one way hash.
https://www.c-sharpcorner.com/UploadFile/d0e913/how-will-you-store-a-password-in-database/

Remember to use the `EF Core tools` commands to update the db:
* `Add-Migration AddedUserTable -Project GreetingService.Infrastructure`
* `update-database -project GreetingService.Infrastructure`

### Steps
1. Create a new class `User.cs` in `GreetingService.Core` and add the properties that a `User` should have
2. Add a `DbSet` propert to `GreetingDbContext` for `User`. We will use this to interact with the user table in the db
3. Add this `override` method to `GreetingDbContext` to specify `email` as primary key
```c#
        /// <summary>
        /// This is a way to specify table config in db (i.e. specifying primary key)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tell EF Core that the primary key of User table is email
            modelBuilder.Entity<User>()
                .HasKey(c => c.Email);
        }
```
4. Update the method `IsValidUser()` to `async` in `IUserService`
5. Create a new class `SqlUserService` in `GreetingService.Infrastructure` that implements `IUserService`
6. Implement the method `IsValidUser()` using the `GreetingDbContext.User` table
7. Run the `EF Core tools` commands to create a migration and then update the db. This step will create the `User` table using the `DbSet<User>` property in the `GreetingDbContext` class.      
    - `Add-Migration AddedUserTable -Project GreetingService.Infrastructure`
    - `update-database -project GreetingService.Infrastructure`
8. Update dependency injection to use the `SqlUserService` and test it
    - Insert users to the table using `Azure SQL Studio`

### Example output
<img width="362" alt="image" src="https://user-images.githubusercontent.com/2921523/154059029-947901b7-a6a3-4224-80e7-4ed1bd92d386.png">
