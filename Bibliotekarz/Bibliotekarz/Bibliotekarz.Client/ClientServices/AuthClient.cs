using System.Net.Http.Json;
using Bibliotekarz.Client.Authentication;
using Bibliotekarz.Shared.DTOs;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bibliotekarz.Client.ClientServices;

public class AuthClient(
    HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authenticationStateProvider
    ) : IAuthClient
{
    public async Task RegisterAsync(RegisterDto request)
    {
        var response = await httpClient.PostAsJsonAsync(Endpoints.Auth.Register, request);
        response.EnsureSuccessStatusCode();
    }

    public async Task<LoginResponse?> LoginAsync(LoginDto request)
    {
        var response = await httpClient.PostAsJsonAsync(Endpoints.Auth.Login, request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task LogoutAsync()
    {
        await ((JwtAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOutAsync();
    }
}

public interface IAuthClient : IApiClient
{
    Task RegisterAsync(RegisterDto request);
    Task<LoginResponse?> LoginAsync(LoginDto request);
    Task LogoutAsync();
}
