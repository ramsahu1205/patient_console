SET ASPNETCORE_ENVIRONMENT=Development
SET LAUNCHER_PATH=bin\Debug\net6.0\publish\doc_app_project.exe
cd /d "C:\Program Files\IIS Express"
iisexpress.exe /config:"D:\dotnet_project\doc_app_project\.vs\doc_app_project\config\applicationhost.config" /site:"WebSite1" /apppool:"Clr4IntegratedAppPool"