# NetTest

NetTest is project to calculate the value of foreign currencies according to Argentinian Pesos

## SetUp
The project can be connected to SQL Server or SQLite using one of this lines.

```c#
services.AddScoped<IDataConnection, SQLite>(x => new SQLite(Configuration.GetValue<string>("Database:Sqlite")));

services.AddScoped<IDataConnection, SqlServer>(x => new SqlServer(Configuration.GetValue<string>("Database:SQLServer")));
```

## Question

### About the endpoints created above, we would like to know what do you think about using the user ID as the input endpoint. 
I think it is an important security breach because a malician person could perform transactions using the user ID without any way of preventing this to happen.

### How would you improve the transaction to ensure that the user who makes the purchase is the correct user?

There are some common practices you can use such as using a single factor Authentication with JWT token to be sure the a person perform transaction only on its account. But in cases where money is involve, having multiple factor to validate the users are mandatory. 