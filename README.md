# WindowsServiceLogger
WindowsServiceLogger is a windows service application that can be used for logging after performing certain operations(Email triggers, API calls, text logs, etc) at regular intervals of time. Here main use case is the long-running task in the background, provided the system is up and running. 

This project will create log after every 30 seconds in a text file. Time intervals can be modified from app.config file. API calls made and the response can be logged in to the text file. The API calls can be any operation such as triggering email, sending crash logs of application, etc. 


<br>

## Introduction to windows service:
- Microsoft Windows services are used for creating the long-running application in the background. 
- This can be leveraged to all users of the system or a specific user (installed for particular user account). Meaning, if the service is installed for the local user, the service will execute only when local user is logged into system, but if it is installed for everyone on the system, the service will continue to run for all users on the system.
- This type of application can automatically be started on system login.

<br>

## Environment:
- Operating System: Windows
- Visual Studio 2017 or the later (preferred).
- Dotnet Framework:  4.7.1

<br>

## Prerequisites:
- Windows service project template (This mostly are pre-installed with visual studio). You will have this project template if you have not made selective installation and skipped the project template selection.
- Visual Studio installer. [Click here for download link](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects)
	

<br>

## Key highlights:
- Create windows service project in visual studio <em>(File -> New -> Windows service)</em>.
- Main method in Program.cs is the starting point of the application. 
- To register the windows service to Windows service control manager, ProjectInstaller needs to be added in project <em>(Right click on Service file(Service1.cs in case of default project and in the current project it is LoggerService.cs) and then click on add installer)</em>
- Windows service starter application does not allow debugging, so have made some tweaks to the application to do so using preprocessor directives. [Preprocessor directives](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives)
- To create setup file, right click on solution and then add New Project <em>(Other Project Types -> Setup Project)</em>. Specify a name and click ok. Now right click on setup project in solution explorer, then click on View from context -> Custom Actions. On the designer right click on Custom Actions -> Add Custom Actions -> Select Application Folder -> Click on Add Output then Ok.
Now build the setup project, installable file will be created within the release folder of setup project.

<br>

## Service installation steps:
- Double click the setup file to install the service.
- Start the service -> type services.msc on run command -> select the LoggerService (service name) -> right click and then start. 

<br>

## Output:
- Log entry of service made to Event Viewer (Look for WindowsLoggerService in event viewer). Detailed entry of logs will be available.
- Text log will also be generated at the service installation folder and stored in folder named as ServiceLogs.

<br>

## Note:
- For debug mode, log file is stored at documents folder of logged-In user and for release mode it is stored to base directory (installation location)  of the service. 
- The reason for storing the log file at service installation location is because we have configured application for local system and local system is not specific to a user account. 
- Therefore, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) does not returns documents folder of any user. Instead it returns empty path which turns out to be folder where windows store all service files depending on the operating system bit version. 
- Still want to create log file in documents folder? **Change the account selection from LocalSystem to user account from serviceProcessInstaller. But then you have to use user credentials to install application**

<br>

## Resources:
- **Windows service:** https://docs.microsoft.com/en-us/dotnet/framework/windows-services/introduction-to-windows-service-applications
- **Preprocessor directives:** https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives
- **Visual Studio Installer:** https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects

