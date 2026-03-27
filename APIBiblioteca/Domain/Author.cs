namespace APIBiblioteca.Domain;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Navigation property for the 1:N Author -> Books relationship.
    public ICollection<Book> Books { get; set; } = new List<Book>();
}

