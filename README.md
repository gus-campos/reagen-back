Fluxo de uso (regra fixa)

Criar o server do postgre, e o database
Usar os dados de conexão na conection string dentro do appsettings.json

Build da API

```
dotnet publish -c Release
```

Criar migration

```
dotnet restore
dotnet ef migrations add MigrationName
```

Aplicar migration

```
dotnet ef database update
```
