namespace Kolos.DTOs;

public class NewBook
{
    public string Title { get; set; } = String.Empty;
    public List<Author> Authors { get; set; } = null!;
}