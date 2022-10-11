https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-3.1
https://blog.tonysneed.com/2018/05/27/customize-ef-core-scaffolding-with-handlebars-templates/
https://www.learnentityframeworkcore.com/walkthroughs/existing-database
https://www.youtube.com/watch?v=fmvcAzHpsk8&t=137s
https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

Importante
dotnet nuget add source --name nuget.org https://api.nuget.org/v3/index.json


dotnet new webapi -o TodoApi
cd TodoApi
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
code -r ../TodoApi

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Design

dotnet add package EntityFrameworkCore.Scaffolding.Handlebars
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

dotnet tool install --global dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers


dotnet ef dbcontext scaffold "Data Source=arba-xarb034; Initial Catalog=Malbec;User Id=sa;Password=Sql2017!" Microsoft.EntityFrameworkCore.SqlServer -d -o Model  -c  Entities  --context-dir Context  -f --use-database-names 
--context-namespace EmmploymeNet.Model

dotnet aspnet-codegenerator controller -name MeetingController -api -m Meeting -dc Entities -outDir Controllers
dotnet aspnet-codegenerator razorpage index Empty 


dotnet add package EntityFrameworkCore.Scaffolding.Handlebars 


https://stackoverflow.com/questions/36937276/is-there-any-replace-of-assemblybuilder-definedynamicassembly-in-net-core
    <PackageReference Include="System.Reflection.Emit" Version="4.3.0" />



dotnet ef dbcontext scaffold "Data Source=sql5031.site4now.net; Initial Catalog=DB_9AA5AE_PortalProveedores;User Id=DB_9AA5AE_PortalProveedores_admin;Password=malbec20" Microsoft.EntityFrameworkCore.SqlServer -d -o Model  -c  Entities  --context-dir Context  -f --use-database-names 

dotnet publish --configuration Release -o 'C:\Proyectos\Malbec\Cabernet.eploy'


dotnet ef dbcontext scaffold "Data Source=arba-xarb034; Initial Catalog=JaiElulCarpeta;User Id=sa;Password=Sql2017!" Microsoft.EntityFrameworkCore.SqlServer -d -o Model  -c  Entities  --context-dir Context  -f --use-database-names --no-build