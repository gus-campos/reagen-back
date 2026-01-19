Fluxo de uso (regra fixa)

Build da API

```
dotnet publish -c Release
```

Subir banco + API

```
docker compose up -d db app
```

Criar migration

```
docker compose run --rm migrate dotnet restore
docker compose run --rm migrate dotnet ef migrations add MigrationName
```

Aplicar migration

```
docker compose run --rm migrate dotnet ef database update
```
