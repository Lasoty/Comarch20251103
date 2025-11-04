using ComarchCwiczenia.Client.Model;
using Microsoft.AspNetCore.Components;

namespace ComarchCwiczenia.Client.Pages;

public partial class BlazorValidations
{
    [Inject]
    public NavigationManager Navigation { get; set; }

    public Person PersonModel { get; set; } = new();

    private async Task AddPerson()
    {
        
    }

}
