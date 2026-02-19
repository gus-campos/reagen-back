Fluxo de uso (regra fixa)

Criar o server do postgre, e o database
Usar os dados de conexão na conection string dentro do appsettings.json

Build da API

```bash
dotnet publish -c Release
```

Execução

```bash
dotnet run --project ReagenBack.Core
```

Criar migration

```bash
dotnet restore
dotnet ef migrations add MigrationName
```

Aplicar migration

```bash
dotnet ef database update
```
