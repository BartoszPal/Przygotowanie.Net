using System.Transactions;
using Kolos.DTOs;
using Kolos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kolos.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [Route("{id:int}/authors")]
    public async Task<IActionResult> GetBook(int id)
    {
        if (!await _bookRepository.DoesBookExist(id))
        {
            return NotFound("Book doesn't exist");
        }

        return Ok(await _bookRepository.GetBook(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBook(NewBook book)
    {
        if (await _bookRepository.DoesBookExist(book.Title))
        {
            return NotFound($"Book with title {book.Title} already exist");
        }

        int bookId;
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            bookId = await _bookRepository.AddBook(book.Title);
            foreach (var author in book.Authors)
            {
                int authorId = await _bookRepository.AddAuthor(author);
                await _bookRepository.AddAuthorWithBookId(bookId, authorId);
            }
            
            scope.Complete();
        }

        Book result = new Book()
        {
            Id = bookId,
            Title = book.Title,
            Authors = book.Authors
        };

        return Created(Request.Path.Value ?? $"api/books/{bookId}/authors", result);
    }
}