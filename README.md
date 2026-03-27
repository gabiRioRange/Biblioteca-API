<div align="center">
  <h1>📚 APIBiblioteca</h1>
  <p>
    <em>API RESTful para estudo de relacionamentos com EF Core, DTOs e AutoMapper.</em>
  </p>

  <a href="https://github.com/gabiRioRange/Biblioteca-API/actions/workflows/ci.yml">
    <img src="https://github.com/gabiRioRange/Biblioteca-API/actions/workflows/ci.yml/badge.svg?branch=main" alt="CI" />
  </a>
  <a href="https://codecov.io/gh/gabiRioRange/Biblioteca-API">
    <img src="https://codecov.io/gh/gabiRioRange/Biblioteca-API/branch/main/graph/badge.svg" alt="Coverage" />
  </a>
  <br/>

  <img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet" alt=".NET 10" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91" alt="ASP.NET Core Web API" />
  <img src="https://img.shields.io/badge/EF%20Core-SQLite-0F6CBD" alt="EF Core SQLite" />
  <img src="https://img.shields.io/badge/status-estudo-success" alt="Status estudo" />
</div>

---

A **APIBiblioteca** é um projeto de estudo com organização limpa e foco em boas práticas de API: entidades de domínio, mapeamento com AutoMapper, acesso a dados com EF Core e retorno via DTOs.

## 🏷️ Badges (CI/Coverage)

- O badge de **CI** já está ativo e ligado ao workflow em `.github/workflows/ci.yml`.
- O badge de **Coverage** está ligado ao Codecov e depende do upload no pipeline.
- O workflow executa build + testes e publica `coverage.cobertura.xml` automaticamente.

## ✨ Funcionalidades

- Listar autores
- Listar livros com nome do autor
- Criar empréstimo de livro
- Bloquear empréstimo duplicado para livro com empréstimo em aberto
- Seed automático de dados iniciais

## 🧱 Estrutura do projeto

- **Controllers**: endpoints HTTP (`AuthorsController`, `BooksController`, `LoansController`)
- **Domain**: entidades (`Author`, `Book`, `Loan`)
- **Data**: `LibraryDbContext` e inicialização de dados (`DbInitializer`)
- **Dtos**: contratos de entrada/saída da API
- **Mapping**: perfil do AutoMapper (`LibraryProfile`)

## 🛠️ Stack tecnológica

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core + SQLite
- AutoMapper

## 📦 Endpoints

| Método | Rota | Descrição |
|---|---|---|
| GET | `/authors` | Lista autores |
| GET | `/books` | Lista livros |
| POST | `/loans` | Cria um novo empréstimo |

### Exemplo de requisição

**POST** `/loans`

```json
{
	"bookId": 1
}
```

### Exemplo de resposta (201 Created)

```json
{
	"id": 1,
	"bookId": 1,
	"loanDateUtc": "2026-03-27T20:15:00Z"
}
```

## 🚀 Como executar (Windows / PowerShell)

```powershell
cd C:\Users\gabriel\RiderProjects\APIBiblioteca\APIBiblioteca
dotnet restore
dotnet run
```

> O banco SQLite é criado automaticamente na primeira execução (`EnsureCreated`) e recebe dados iniciais de exemplo.

## 🔍 Swagger / OpenAPI

Em ambiente de desenvolvimento, a documentação interativa fica disponível em:

- `/swagger`

## 🧪 Smoke test rápido

Com a API em execução, rode:

```powershell
cd C:\Users\gabriel\RiderProjects\APIBiblioteca
.\scripts\smoke-test.ps1 -BaseUrl "https://localhost:5001"
```

O script valida o fluxo básico:

1. `GET /authors`
2. `GET /books`
3. `POST /loans`

## 🧯 Troubleshooting

### Erro de certificado HTTPS no PowerShell

Se necessário, confie no certificado local de desenvolvimento:

```powershell
dotnet dev-certs https --trust
```

### Erro de porta em uso

Rode em outra porta:

```powershell
dotnet run --urls "https://localhost:5003"
```

e ajuste o `-BaseUrl` no smoke test.

## 📁 Estrutura de pastas

```text
APIBiblioteca/
├── Controllers/
├── Data/
├── Domain/
├── Dtos/
├── Mapping/
├── Program.cs
└── appsettings.json
```

## 🤝 Contribuição

Sugestões de melhoria são bem-vindas. Priorize PRs pequenos, objetivos e com documentação atualizada.

