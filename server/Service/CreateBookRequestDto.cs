using Facet;
using Infra;

[Facet(sourceType:typeof(Book), exclude: [nameof(Book.Author), nameof(Book.BookId)]) ]
public partial class CreateBookRequestDto;