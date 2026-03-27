using APIBiblioteca.Data;
using APIBiblioteca.Dtos.Book;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Controllers;

[ApiController]
[Route("books")]
public class BooksController(LibraryDbContext dbContext, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<BookListItemDto>>> GetAllAsync()
    {
        var books = await dbContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ProjectTo<BookListItemDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(books);
    }
}

