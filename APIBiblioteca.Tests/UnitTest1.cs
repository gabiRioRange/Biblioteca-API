using APIBiblioteca.Controllers;
using APIBiblioteca.Data;
using APIBiblioteca.Domain;
using APIBiblioteca.Dtos.Loan;
using APIBiblioteca.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace APIBiblioteca.Tests;

public class LoanFlowTests
{
    [Fact]
    public async Task CreateLoan_WhenBookDoesNotExist_ReturnsNotFound()
    {
        await using var dbContext = CreateDbContext(nameof(CreateLoan_WhenBookDoesNotExist_ReturnsNotFound));
        var controller = new LoansController(dbContext, CreateMapper());

        var result = await controller.CreateAsync(new CreateLoanDto { BookId = 999 });

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Contains("was not found", notFound.Value?.ToString());
    }

    [Fact]
    public async Task CreateLoan_WhenBookHasOpenLoan_ReturnsConflict()
    {
        await using var dbContext = CreateDbContext(nameof(CreateLoan_WhenBookHasOpenLoan_ReturnsConflict));
        var author = new Author { Name = "Autor" };
        var book = new Book { Title = "Livro", Author = author };

        dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync();

        dbContext.Loans.Add(new Loan { BookId = book.Id, LoanDateUtc = DateTime.UtcNow, ReturnDateUtc = null });
        await dbContext.SaveChangesAsync();

        var controller = new LoansController(dbContext, CreateMapper());

        var result = await controller.CreateAsync(new CreateLoanDto { BookId = book.Id });

        var conflict = Assert.IsType<ConflictObjectResult>(result.Result);
        Assert.Contains("already loaned", conflict.Value?.ToString());
    }

    [Fact]
    public async Task CreateLoan_WhenRequestIsValid_ReturnsCreatedWithPayload()
    {
        await using var dbContext = CreateDbContext(nameof(CreateLoan_WhenRequestIsValid_ReturnsCreatedWithPayload));
        var author = new Author { Name = "Autor" };
        var book = new Book { Title = "Livro", Author = author };

        dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync();

        var controller = new LoansController(dbContext, CreateMapper());

        var result = await controller.CreateAsync(new CreateLoanDto { BookId = book.Id });

        var created = Assert.IsType<CreatedResult>(result.Result);
        var payload = Assert.IsType<LoanCreatedDto>(created.Value);

        Assert.Equal(book.Id, payload.BookId);
        Assert.True(payload.Id > 0);
        Assert.StartsWith("/loans/", created.Location);
    }

    [Fact]
    public async Task SeedAsync_WhenDatabaseIsEmpty_CreatesAuthorsAndBooks()
    {
        await using var dbContext = CreateDbContext(nameof(SeedAsync_WhenDatabaseIsEmpty_CreatesAuthorsAndBooks));

        await DbInitializer.SeedAsync(dbContext);

        var authorCount = await dbContext.Authors.CountAsync();
        var bookCount = await dbContext.Books.CountAsync();

        Assert.Equal(2, authorCount);
        Assert.Equal(3, bookCount);
    }

    private static LibraryDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new LibraryDbContext(options);
    }

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<LibraryProfile>(), NullLoggerFactory.Instance);
        return config.CreateMapper();
    }
}
