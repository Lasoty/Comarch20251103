using Bibliotekarz.Client.ClientServices;
using Bibliotekarz.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace Bibliotekarz.Client.Pages.Auth;

public partial class Register
{
    private RegisterDto _model = new();
    private string _info = string.Empty;
    private string _error = string.Empty;

    [Inject] 
    public IAuthClient AuthClient { get; set; } = null!;

    private async Task OnRegisterClicked()
    {
        try
        {
            await AuthClient.RegisterAsync(_model);
            _info = "Utworzono użytkownika";
        }
        catch
        {
            _error = "Nie udało się utworzyć nowego użytkownika.";
        }
    }
}
