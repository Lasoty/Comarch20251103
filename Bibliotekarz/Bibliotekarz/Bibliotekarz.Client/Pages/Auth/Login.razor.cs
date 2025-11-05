using Bibliotekarz.Client.Authentication;
using Bibliotekarz.Client.ClientServices;
using Bibliotekarz.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bibliotekarz.Client.Pages.Auth;

public partial class Login
{
    private LoginDto _model = new();
    private string _error = string.Empty;

    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; }

    [Inject]
    public IAuthClient AuthClient { get; set; }

    [Inject]
    public JwtAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private async Task OnLoginClicked()
    {
        LoginResponse? result = await AuthClient.LoginAsync(_model);

        if (result is null)
        {
            _error = "Błędny login lub hasło";
            return;
        }

        await AuthenticationStateProvider.MarkUserAsAuthenticated(result.Token);

        StateHasChanged();
        Snackbar.Add("Zalogowano", Severity.Success);
        Navigation.NavigateTo("/");
    }
}
