using System.ComponentModel.DataAnnotations;
using Infra;
using LinqToDB;
using LinqToDB.Async;
using LinqToDB.DataProvider.SQLite;

public class LibraryService(MyDatabaseConnection db)
{

    public async Task<List<BookDto>> GetBooks(int page, int resultsPerPage)
    {
        if (page < 1)
            throw new ValidationException("Page must be 1 or higher");
        if (resultsPerPage < 1)
            throw new ValidationException("Must have 1 or more result per page");
        
        
        return await db.Books
            .LoadWith(b => b.Author)
            .ThenLoad(a => a.BooksWrittenByAuthor)
            .Take(resultsPerPage)
            .Skip((page - 1) * resultsPerPage)
            .Select(b => new BookDto(b)
        {
            Author = new AuthorDto(b.Author)
        })
            .ToListAsync();
    }

    public BookDto CreateBook(CreateBookRequestDto dto)
    {
        if (dto.NumberOfPages<1)
        {
            throw new ValidationException("Pages must be 1 or higher");
        }
        
        var b = new Book()
        {
            NumberOfPages = dto.NumberOfPages,
            BookTitle = dto.BookTitle,
            BookId = Guid.NewGuid().ToString(),
            AuthorId = dto.AuthorId
        };
        db.Insert(b);
        return new BookDto(b);
    }
}