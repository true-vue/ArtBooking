# ArtBooking

.net ticket booking web application demo

## STEP 0: Visual Studio Code setup

Install and configure VSCode for running .net projects.

https://code.visualstudio.com/docs/setup/setup-overview
(windows) https://code.visualstudio.com/docs/setup/windows

https://code.visualstudio.com/docs/languages/dotnet#_net-coding-pack-for-students

## STEP 1: Project creation

Run in command line:

create project folder
`> md ArtBooking`

enter the folder
`> cd ArtBooking`

create new webapi project .net (with controllers for endpoints)
`> dotnet new webapi --use-controllers`

Open project in VSCode
`> code .`

## STEP 2: Running the project

Open terminal: top menu > Terminal > new terminal
In terminal use below command to start the project

`> dotnet run`

You should seen info like below:

Trwa kompilowanie...
info: Microsoft.Hosting.Lifetime[14]
Now listening on: http://localhost:5118
info: Microsoft.Hosting.Lifetime[0]
Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
Content root path: C:\Users\pk\Documents\projects\ArtBooking

Now you can examine the app http://localhost:5118/swagger/index.html using listed host adress (in your case port might be different)
To stop the app use Ctrl+C keys in terminal window.

## STEP 3: Debugging the project

In VSCode click debug icon (arrow with bug) located at the left sidebar.
Initially no configuration for debugging will be present and additional messages will be visible inside the pane.
Click: "create a launch.json file" link. That will prompt dropdown menu in top center. Pick ".Net 5 and .net core" from dropdown list.
That will create lauch.json and tasks.json files in .vscode folder.
Now at the top of debug pane ".NET Code Lauch" with green arrow can be seen.
Click green arrow to start debugging.
You should see commands running in Terminal window and new browser window will be opened with the app.
You can set breakpoints in code to capture control of running the code.
