namespace Bibliotekarz.Shared.DTOs;

public class BookDto
{
    public string Author { get; set; }

    public string Title { get; set; }

    public int PageCount { get; set; }

    public bool IsBorrowed { get; set; }

    public CustomerDto Borrower { get; set; }
}

public class CustomerDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
