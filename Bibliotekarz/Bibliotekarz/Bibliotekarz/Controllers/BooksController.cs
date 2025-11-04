using Bibliotekarz.Shared.DTOs;
using Bibliotekarz.Shared.ResultWrapper;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotekarz.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<BookDto> result = GetFakeBooks();
        return Ok(Result<List<BookDto>>.Success(result));
    }

    private List<BookDto> GetFakeBooks()
    {
        List<BookDto> result = [];
        return result;
    }
}
