using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Dtos.Loan;

public class CreateLoanDto
{
    [Required]
    public int BookId { get; init; }
}

