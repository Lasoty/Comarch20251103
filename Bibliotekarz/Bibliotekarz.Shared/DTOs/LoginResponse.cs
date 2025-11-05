namespace Bibliotekarz.Shared.DTOs;

public record LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }

    public IList<string> Roles { get; set; } = [];
}
