using Facet;
using Infra;

[Facet(sourceType:typeof(Author), exclude:nameof(Author.BooksWrittenByAuthor))]
public partial class AuthorDto;

[Facet(sourceType: typeof(Book), exclude: nameof(Book.Author))]
public partial class BookDto
{
    public AuthorDto Author { get; set; }
}