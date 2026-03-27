using APIBiblioteca.Data;
using APIBiblioteca.Dtos.Author;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Controllers;

[ApiController]
[Route("authors")]
public class AuthorsController(LibraryDbContext dbContext, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<AuthorListItemDto>>> GetAllAsync()
    {
        var authors = await dbContext.Authors
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .ProjectTo<AuthorListItemDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Ok(authors);
    }
}

