# Assessment

## Setup
Establish connection to database
```
$ dotnet user-secrets init
$ dotnet user-secrets set "DbPassword" "1234"
$ dotnet user-secrets set "DbUser" "user"
$ dotnet user-secrets set "DbName" "assessment"
```

## Reference
1. [Model Validation](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-7.0)
2. [Global exception handling](https://blog.christian-schou.dk/how-to-do-global-exception-handling-in-net-6-and-7/)
3. [Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-7.0)
