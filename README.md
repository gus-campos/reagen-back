# Reagen Back

API ASP.NET Core (.NET 8) com PostgreSQL.

## Pré-requisitos

- [Docker](https://docs.docker.com/get-docker/) com Docker Compose
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0): só para rodar a API fora do Docker ou criar migrations

No Linux, para usar o Docker sem `sudo` (uma vez por máquina):

```bash
sudo usermod -aG docker $USER
```

Depois saia e entre de novo na sessão (ou rode `newgrp docker` no terminal atual). Confira com `docker info`.

## Configuração

```bash
git clone https://github.com/gus-campos/reagen-back.git
cd reagen-back
cp .env.example .env
```

Edite o `.env` com a senha do banco e a chave JWT (mínimo de 32 caracteres).

## Rodar com Docker

Na raiz do repositório (onde está o `docker-compose.yml`):

```bash
docker compose up --build
```

A API fica em http://localhost:5078/swagger. As migrations são aplicadas automaticamente quando a API inicia.

Para parar: `docker compose down` (adicione `-v` para apagar os dados do banco).

## Autenticação em desenvolvimento (dev-auth)

Enquanto não há login real, o endpoint `POST /dev-auth?providerUserId=<qualquer-texto>` gera um JWT sem senha. O `providerUserId` é um identificador inventado (ex.: `João`); cada valor é um usuário diferente, criado automaticamente na primeira chamada. Só funciona em `Development` (em outros ambientes retorna 404).

No Swagger:

1. Execute `POST /dev-auth` com um `providerUserId` e copie o `token` da resposta.
2. Clique em **Authorize** e cole o token.

## Rodar a API localmente

Suba só o Postgres:

```bash
docker compose up -d db
```

Se a API em container estiver rodando, pare-a antes (`docker compose stop api`), pois ela usa a mesma porta 5078.

Crie `ReagenBack.Core/appsettings.Development.json` (fora do git; não use o `appsettings.json`, que é versionado) com os mesmos dados do `.env`:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=reagen;Username=admin;Password=..."
  },
  "JwtSettings": {
    "Key": "..."
  }
}
```

Execute:

```bash
dotnet run --project ReagenBack.Core
```

## Migrations

Com o Postgres rodando e o `appsettings.Development.json` configurado:

```bash
dotnet tool restore
dotnet ef migrations add NomeDaMigration --project ReagenBack.Core
dotnet ef database update --project ReagenBack.Core
```

## Build

```bash
dotnet publish -c Release
```
