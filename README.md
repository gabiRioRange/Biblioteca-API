# API Biblioteca

Base de uma API RESTful para estudo de relacionamentos com EF Core, DTOs e AutoMapper.

## Stack

- .NET 10 (compatível com a ideia de .NET 8+)
- ASP.NET Core Web API
- Entity Framework Core (SQLite)
- AutoMapper

## Endpoints implementados

- `GET /authors`
- `GET /books`
- `POST /loans`

## Executar

```powershell
cd C:\Users\gabriel\RiderProjects\APIBiblioteca\APIBiblioteca
dotnet restore
dotnet run
```

Swagger abre em ambiente de desenvolvimento, normalmente em `/swagger`.

## Smoke test rápido (opcional)

Com a API rodando, execute:

```powershell
cd C:\Users\gabriel\RiderProjects\APIBiblioteca
.\scripts\smoke-test.ps1 -BaseUrl "https://localhost:5001"
```

