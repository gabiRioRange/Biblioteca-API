namespace APIBiblioteca.Domain;

public class Loan
{
    public int Id { get; set; }

    public DateTime LoanDateUtc { get; set; } = DateTime.UtcNow;

    public DateTime? ReturnDateUtc { get; set; }

    public int BookId { get; set; }

    public Book? Book { get; set; }
}

