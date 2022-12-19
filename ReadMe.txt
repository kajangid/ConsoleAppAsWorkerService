Open Power Shell as Admin

also put "appsettings.json" in publish folder.

sc.exe create "_ConsoleAppAsWorkerService" binpath="{Publish Folder Path}\ConsoleAppAsWorkerService.exe"

sc.exe start "_ConsoleAppAsWorkerService"

sc.exe stop "_ConsoleAppAsWorkerService"



after run open file.txt to check if it's working.