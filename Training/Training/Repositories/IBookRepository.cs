using Kolos.DTOs;

namespace Kolos.Repositories;

public interface IBookRepository
{
    Task<Book> GetBook(int id);
    Task<bool> DoesBookExist(int id);
    Task<bool> DoesBookExist(string title);
    Task<int> DoesAuthorExist(Author author);
    Task<int> AddBook(string title);
    Task<int> AddAuthor(Author author);
    Task AddAuthorWithBookId(int idBook, int idAuthor);
}