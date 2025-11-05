using Bibliotekarz.Shared.DTOs;

namespace Bibliotekarz.Data.Model;

public class BookEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public int PageCount { get; set; }

    public bool IsBorrowed { get; set; }

    public CustomerEntity Borrower { get; set; }
}