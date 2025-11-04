using System.Net.Http.Json;
using Bibliotekarz.Shared.DTOs;
using Bibliotekarz.Shared.ResultWrapper;

namespace Bibliotekarz.Client.ClientServices;

public class BooksClient(HttpClient httpClient) : IBooksClient
{
    public async Task<IResult<IEnumerable<BookDto>>> GetAllAsync()
    {
        Result<IEnumerable<BookDto>>? response = await httpClient.GetFromJsonAsync<Result<IEnumerable<BookDto>>>(Endpoints.Books.GetAll);
        return response;
    }
}

public interface IBooksClient : IApiClient
{
    Task<IResult<IEnumerable<BookDto>>> GetAllAsync();
}
