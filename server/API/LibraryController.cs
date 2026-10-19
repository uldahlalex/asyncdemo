using Infra;
using Microsoft.AspNetCore.Mvc;

public class LibraryController(LibraryService service) : ControllerBase
{
    [HttpGet(nameof(GetBooks))]
    public async Task<List<BookDto>> GetBooks(int page, int resultsPerPage)
    {
        return await service.GetBooks(page, resultsPerPage);
    }

    [HttpPost(nameof(CreateBook))]
    public BookDto CreateBook(CreateBookRequestDto dto)
    {
        return service.CreateBook(dto);
    }
}