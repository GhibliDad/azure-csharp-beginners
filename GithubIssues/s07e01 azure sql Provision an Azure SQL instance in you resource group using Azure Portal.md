One of the most common ways to store data is to use a `Relational Database`. Microsoft SQL Server is a database technology from Microsoft and it's been around many years. This technology is offered for installation in `Virtual Machines` as `Microsoft SQL Server 2019`. It's also offered as a `PAAS` service in Azure as `Azure SQL` where many of the layers in the tech stack is managed by the platform allowing us to focus on the things that are important for us: the data.

<img width="1222" alt="image" src="https://user-images.githubusercontent.com/2921523/152879707-07acc7ce-c52f-47d8-b231-e75ef7328258.png">

Azure SQL:
https://docs.microsoft.com/en-us/azure/azure-sql/database/sql-database-paas-overview

Relational Database:
https://aws.amazon.com/relational-database/

Relational databases and Azure SQL are big topics, we will only have time to cover the most basic theory and use cases. 

We will work with these concepts:

Azure SQL Server:
Part of an Azure SQL deployment in Azure. An Azure SQL Server resource has a relation to an Azure SQL Database resource (similar to how an Azure App Service Plan is related to an Azure App Service).

Azure SQL Database:
The resource containing our tables and data. Is always associated with a corresponding Azure SQL Server

Table:
Our application will store data in one or more tables. A table consists of fixed and named columns and rows of data. Indexes in various columns can make querying data more efficient. Performance optimization in a database is a big topic, we won't cover this area.

Connection string:
A string containing everything an application needs to connect to a database, including address and access key. A connection string is a secret and should never be committed to source control.

Firewall:
It's generally a bad idea to expose a database to the internet. Best practice is to only allow whitelisted network traffic to the database.

SQL Authentication:
We will use SQL authentication to access the database. SQL authentication consists of a username and a password.

### Goal
* Azure SQL

### Steps
1. Create a new `Azure SQL` resource in you `resource group` in the Azure Portal
<img width="1052" alt="image" src="https://user-images.githubusercontent.com/2921523/152874781-e0e52000-cbb7-434e-acf1-2f4a78979496.png">

2. Choose resource type `Single Database`
<img width="410" alt="image" src="https://user-images.githubusercontent.com/2921523/152874880-ab496ac5-4eda-4efb-a71f-5b7ef388e34f.png">

3. Configure with these values:
    - Resource group: your existing resource group
    - Database name: `greeting-sqldb-dev`
    - Create a new `Server`
         - Server name: `greeting-sql-dev`
         - Location: `West Europe`
         - Authentication: `Use SQL Authentication`
         - Create new credentials and write these down somewhere secure
    - Want to use elastic pool: No
    - Service Tier: Basic
    - Backup storage redundancy: Locally reduntant backup storage

<img width="772" alt="image" src="https://user-images.githubusercontent.com/2921523/152876243-9aea1afb-1c78-4a7c-8808-6fed0ef41b0d.png">

<img width="740" alt="image" src="https://user-images.githubusercontent.com/2921523/152875904-47ba9a03-f5ee-4911-8a46-eae137c78937.png">

<img width="1185" alt="image" src="https://user-images.githubusercontent.com/2921523/152876159-e6ade67b-81d3-4614-83ec-cf11bcdbd103.png">

4. Click `Review + create`
5. Navigate to the SQL Server resource and click on `Firewalls and virtual networks`
6. Click on `Add Client IP` to add your current IP to the firewall whitelist
7. Change the config `Allow Azure services and resources to access this server` from `No` to `Yes`
<img width="975" alt="image" src="https://user-images.githubusercontent.com/2921523/152878119-95f7421b-95fb-4da5-89a5-c92b5cec83cd.png">

8. Click `Save` to save the firewall changes
9. Navigate to the `SQL Database` resource and click on `Query Editor`
10. Login with you SQL Server credentials from the create step
11. There are no tables or other artifacts since this resource is new, we'll create the needed tables in next episodes

### Example output
<img width="1136" alt="image" src="https://user-images.githubusercontent.com/2921523/152876353-bd57d89c-9ca5-4c7a-a8a5-b5d55decce94.png">

<img width="1596" alt="image" src="https://user-images.githubusercontent.com/2921523/152876796-75dbf0e4-b021-4a02-a5ad-dbec8b6d33aa.png">

<img width="1875" alt="image" src="https://user-images.githubusercontent.com/2921523/152878605-826dcfce-fada-4969-83fe-413c548dde26.png">
