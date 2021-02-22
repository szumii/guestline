Written by Pawel Krajna. In case of any guestion please write on pawel.krajna at gmail dot com

# guestline intervie task

This repo contains 3 .NET Core projects:
- class library (application domain)
- xunit tests (application domain tests)
- consol app (UI to be run)


# building the application

Build using .NET Core CLI. Run below commands from UI project directory.

```console
cd Battleship.UI
dotnet build
dotnet run
```

Follow the instrucition displayed on the console and have a fun.


# develop \ test the application

Open projects in Visual Studio Code.
Go to main direcory (of cloned repo) in terminal and type

```console
code .
dotnet build
```

Build unit test project before run.
Run unit tests to validate code quality.
In VS Code open Palette (Shift + Ctrl + P) and hit 'Run All Tests'.
'.NET Core Test Explorer' extension can be helpfull to visualize the test results.

