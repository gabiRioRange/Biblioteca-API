using APIBiblioteca.Domain;
using APIBiblioteca.Dtos.Author;
using APIBiblioteca.Dtos.Book;
using APIBiblioteca.Dtos.Loan;
using AutoMapper;

namespace APIBiblioteca.Mapping;

public class LibraryProfile : Profile
{
    public LibraryProfile()
    {
        CreateMap<Author, AuthorListItemDto>();

        CreateMap<Book, BookListItemDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : string.Empty));

        // Loan date is defined by the API at creation time, not by client input.
        CreateMap<CreateLoanDto, Loan>()
            .ForMember(dest => dest.LoanDateUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ReturnDateUtc, opt => opt.Ignore());

        CreateMap<Loan, LoanCreatedDto>();
    }
}

