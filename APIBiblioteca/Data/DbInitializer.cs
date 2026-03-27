using APIBiblioteca.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(LibraryDbContext dbContext)
    {
        if (await dbContext.Authors.AnyAsync())
        {
            return;
        }

        var machado = new Author { Name = "Machado de Assis" };
        var clarice = new Author { Name = "Clarice Lispector" };

        dbContext.Authors.AddRange(machado, clarice);

        dbContext.Books.AddRange(
            new Book { Title = "Dom Casmurro", Author = machado },
            new Book { Title = "Memorias Postumas de Bras Cubas", Author = machado },
            new Book { Title = "A Hora da Estrela", Author = clarice });

        await dbContext.SaveChangesAsync();
    }
}

