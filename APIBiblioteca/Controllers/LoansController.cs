using APIBiblioteca.Data;
using APIBiblioteca.Domain;
using APIBiblioteca.Dtos.Loan;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Controllers;

[ApiController]
[Route("loans")]
public class LoansController(LibraryDbContext dbContext, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LoanCreatedDto>> CreateAsync([FromBody] CreateLoanDto request)
    {
        var bookExists = await dbContext.Books
            .AsNoTracking()
            .AnyAsync(b => b.Id == request.BookId);

        if (!bookExists)
        {
            return NotFound($"Book with id {request.BookId} was not found.");
        }

        // A book cannot be loaned twice while an active loan is open.
        var hasOpenLoan = await dbContext.Loans
            .AsNoTracking()
            .AnyAsync(l => l.BookId == request.BookId && l.ReturnDateUtc == null);

        if (hasOpenLoan)
        {
            return Conflict($"Book with id {request.BookId} is already loaned.");
        }

        var loan = mapper.Map<Loan>(request);

        dbContext.Loans.Add(loan);
        await dbContext.SaveChangesAsync();

        var response = mapper.Map<LoanCreatedDto>(loan);

        return Created($"/loans/{loan.Id}", response);
    }
}

