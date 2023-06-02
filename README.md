# JwtDotNet


- Paquetes para Instalar Entity Framework:

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Design

- Para trabajar con SqlServer y EF:

Microsoft.EntityFrameworkCore.SqlServer

- Para saber que quedó instalado en el command line:

dotnet ef

- Si hay problemas:

dotnet took install --glogal dotnet-ef

- Para las migraciones de la base de datos:

dotnet ef migrations add CreateUsersTable

- Error Your startup project doesn't reference Microsoft.EntityFrameworkCore.Design

Ir al proyecto y comentar esta línea 		<!--<PrivateAssets>all</PrivateAssets>-->

- Remover todas las migraciones

dotnet ef migrations remove

- Para hacer que las migraciones se vean reflejadas en la base de datos

dotnet ef database update

