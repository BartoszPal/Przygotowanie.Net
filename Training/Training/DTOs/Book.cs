namespace Kolos.DTOs;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public List<Author> Authors { get; set; } = null!;
}

public class Author
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}