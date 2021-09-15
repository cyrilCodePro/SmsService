
# README:
The solution is structured into five projects
1. DataAccessLayer : Is A class library that is managing all database operations.
2. MessageContracts :Is a class library project, and hosts message contracts only.A message contract is just an interface that defines a message type, but does not include any methods or behaviors.
3. MessageComponets : class library project, and it will host our consumer classes and the abstracted 3rd party api service
4. SmsMicroservice :  Is a ASP.NET Core worker Application.
5. SmsMicroserviceTests: Contains the project tests

.The project is using [MassTransit](https://masstransit-project.com/) :A free, open-source distributed application framework for .NET and and for this excercise am using in memory event bus for testing purpose ,any broker can be registered in the startup.cs file and will work fine.
.I have also used Mongodb which is hosted online to ensure expected consitency  and to avoid sending duplicate sms.
.I have  also added a parameter on SendSms command called ```IdempotenceKey``` to ensure expected consistence


## To Build and Run Tests
Make sure you have [dotnet 5](https://dotnet.microsoft.com/download) installed

Navigate to the root directory of the project on terminal or cmd or powershell and type the command below

```sh
dotnet restore
```

Then Build the Project by typing the command below  on Terminal or cmd or powershell.

```sh
dotnet build
```
Then Run tests
```sh
dotnet test --no-build --verbosity normal
```
## To run the project

To run the smsmicroservice and simulate the flow  run the  command below on Terminal or cmd or powershell
```sh
dotnet run --project SmsMicroservice
