namespace APIBiblioteca.Dtos.Loan;

public class LoanCreatedDto
{
    public int Id { get; init; }

    public int BookId { get; init; }

    public DateTime LoanDateUtc { get; init; }
}

