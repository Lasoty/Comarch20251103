using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bibliotekarz.Client.Authentication;

public class JwtAuthenticationStateProvider(
    ILocalStorageService localStorage,
    HttpClient httpClient
    ) : AuthenticationStateProvider
{

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsStringAsync(StorageConstants.Local.TokenKey);

        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var user = CreateClaimsPrincipal(token);
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await localStorage.SetItemAsStringAsync(StorageConstants.Local.TokenKey, token);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var user = CreateClaimsPrincipal(token);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        await localStorage.RemoveItemAsync(StorageConstants.Local.TokenKey);
        httpClient.DefaultRequestHeaders.Authorization = null;

        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

    private ClaimsPrincipal CreateClaimsPrincipal(string token)
    {
        IEnumerable<Claim> claims = ParseClamsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        return user;
    }

    private IEnumerable<Claim> ParseClamsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
