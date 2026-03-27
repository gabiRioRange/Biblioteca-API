namespace APIBiblioteca.Domain;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int AuthorId { get; set; }

    public Author? Author { get; set; }

    // Navigation property for the 1:N Book -> Loans relationship.
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}

