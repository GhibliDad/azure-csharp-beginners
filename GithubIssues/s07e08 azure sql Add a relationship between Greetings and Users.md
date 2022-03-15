A core tenet in Relational databases are relationships between tables in a db. We currently have two tables: `Greetings` and `Users`. Let's create a relationship between these two tables. 

`Greeting.From` should contain an email of a user in `User` table. 
`Greeting.To` should contain an email of a user in `User` table. 

The `From` and `To` column in the db are so called `Foreign Keys`. The only values that are acceptable are values that map to an existing user in the `User` table. Creating this type of hard constraint helps us ensure a level of data quality. We will not allow storing `Greeting` objects to non existing users in our application.

Relationships in EF Core:
https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key

There are many ways to configure `Foreign Keys` in EF Core, we will try with this method here:
https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#without-navigation-property

### Steps
1. Update the `OnModelCreating()` method in `GreetingDbContext` to looks similar to this:
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

            //Tell EF Core that Greeting.From is a foreign key for User table. 
            //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#without-navigation-property
            modelBuilder.Entity<Greeting>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.From)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            //Tell EF Core that Greeting.To is a foreign key for User table
            modelBuilder.Entity<Greeting>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.To)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
```

2. Run the `EF Core` tool command to create a migration and update the database (you might have to remove rows from `Greeting` table before update-database will succeed because of the new constraints in the migration)
    - `Add-Migration CreatedRelationships -project GreetingService.Infrastructure`
    - `update-database -project GreetingService.Infrastructure`
3. Test the `POST Greeting` endpoint, with different combinations of `To` and `From`
    - Can you improve exception handling and exception messages?


### Example output
```console
PM> Add-Migration CreatedRelationships -project GreetingService.Infrastructure
Build started...
Build succeeded.
An operation was scaffolded that may result in the loss of data. Please review the migration for accuracy.
To undo this action, use Remove-Migration.
PM> update-database -project GreetingService.Infrastructure
Build started...
Build succeeded.
Applying migration '20220215214725_CreatedRelationships'.
Done.
PM> 
```

<img width="333" alt="image" src="https://user-images.githubusercontent.com/2921523/154157566-59738873-cb2d-4a54-b710-c654ea83d097.png">
