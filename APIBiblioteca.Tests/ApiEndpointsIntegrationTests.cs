using System.Net;
using System.Net.Http.Json;
using APIBiblioteca.Data;
using APIBiblioteca.Dtos.Author;
using APIBiblioteca.Dtos.Book;
using APIBiblioteca.Dtos.Loan;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace APIBiblioteca.Tests;

public class ApiEndpointsIntegrationTests
{
    [Fact]
    public async Task GetAuthors_ReturnsOkWithSeededAuthors()
    {
        using var factory = new TestApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/authors");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var authors = await response.Content.ReadFromJsonAsync<List<AuthorListItemDto>>();

        Assert.NotNull(authors);
        Assert.NotEmpty(authors);
        Assert.Contains(authors, a => a.Name == "Machado de Assis");
    }

    [Fact]
    public async Task GetBooks_ReturnsOkWithAuthorName()
    {
        using var factory = new TestApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/books");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var books = await response.Content.ReadFromJsonAsync<List<BookListItemDto>>();

        Assert.NotNull(books);
        Assert.NotEmpty(books);
        Assert.Contains(books, b => !string.IsNullOrWhiteSpace(b.AuthorName));
    }

    [Fact]
    public async Task PostLoans_WhenCalledTwiceForSameBook_ReturnsCreatedThenConflict()
    {
        using var factory = new TestApiFactory();
        using var client = factory.CreateClient();

        var firstResponse = await client.PostAsJsonAsync("/loans", new CreateLoanDto { BookId = 1 });
        var secondResponse = await client.PostAsJsonAsync("/loans", new CreateLoanDto { BookId = 1 });

        Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);
    }

    private sealed class TestApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbPath = Path.Combine(Path.GetTempPath(), $"library-test-{Guid.NewGuid()}.db");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configBuilder) =>
            {
                var configValues = new Dictionary<string, string?>
                {
                    ["ConnectionStrings:LibraryConnection"] = $"Data Source={_dbPath}"
                };

                configBuilder.AddInMemoryCollection(configValues);
            });
        }

    }
}
