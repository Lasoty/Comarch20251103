namespace Bibliotekarz.Shared.DTOs;

public class BookDto
{
    public string Author { get; set; }

    public string Title { get; set; }

    public int PageCount { get; set; }

    public bool IsBorrowed { get; set; }

    public Customer Borrower { get; set; }
}

public class Customer
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
