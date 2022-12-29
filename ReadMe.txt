[Hack]
Publish Profile uncheck Single File otherwise logging will not work.
[ToDo]
Need to find Publish Single File solution.


[Host as worker service]
Open Power Shell as Admin

sc.exe create "_ConsoleAppAsWorkerService" binpath="{Publish Folder Path}\ConsoleAppAsWorkerService.exe"

sc.exe start "_ConsoleAppAsWorkerService"

sc.exe stop "_ConsoleAppAsWorkerService"

sc.exe delete "_ConsoleAppAsWorkerService"

