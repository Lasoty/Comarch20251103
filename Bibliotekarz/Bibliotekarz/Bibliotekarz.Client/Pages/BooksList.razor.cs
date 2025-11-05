using System.Collections.ObjectModel;
using Bibliotekarz.Client.ClientServices;
using Bibliotekarz.Shared.DTOs;
using Bibliotekarz.Shared.ResultWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Bibliotekarz.Client.Pages;

public partial class BooksList
{
    private bool _loading = true;

    public ObservableCollection<BookDto> Books { get; set; } = [];

    [Inject]
    public IBooksClient BooksClient { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        IResult<IEnumerable<BookDto>> response = await BooksClient.GetAllAsync();
        if (response.Succeeded)
        {
            foreach (var book in response.Data)
            {
                Books.Add(book);
            }
            Snackbar.Add("Pobrano książki.", Severity.Success);
        }
        else
        {
            Snackbar.Add("Nie udało się pobrać książek", Severity.Error);
        }

        _loading = false;
    }
}
