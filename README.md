# SamMou
1. Get the primary of Cosmos DB and paste it in [here](https://github.com/moumot/SamMou/blob/main/SamMou.Api/appsettings.Development.json#:~:text=%22-,PrimaryKey,-%22%3A%20%22)
2. cd to SamMou.Api 
3. Run following dotnet command on terminal
```
dotnet build
```
4. Run following dotnet command on terminal
```
dotnet run
```
5. Verify that dotnet run is running in the terminal, dotnet run will keep running until terminated 
6. Browse localhost:5001/swagger or the port as specified in your launchSettings.json to verify that it run succesfully
