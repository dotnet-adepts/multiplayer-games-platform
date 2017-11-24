# multiplayer-games-platform

Project is under the development of 7 computer science students. 

## Main requirements/assumptions 

Use: 

- ASP.NET Core MVC
- continous integration
- code reviews sessions
- docker containers
- enterprise-like development methods 

## Demo link

available soon

## How to run on Linux <3
```bash
cd GameApplication/GameApplication
dotnet restore
dotnet user-secrets set initFolderMockup "anything" # just to autogenerate microsoft folder
# Then get our secrets.json file and paste it inside
# ~/.microsoft/usersecrets/<your_app_name_with_random_string>
dotnet ef database update --context ApplicationDBContext
ASPNETCORE_ENVIRONMENT=Development dotnet watch run
# or just simpy
dotnet run
```
## Asciinema example

It's only a random demo of top-notch markdown embedding feature

[![asciicast](https://asciinema.org/a/3466.png)](https://asciinema.org/a/3466)

## More detailed description

available soon (at reasonable development stage)



