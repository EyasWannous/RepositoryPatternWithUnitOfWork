using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryPatternWithUnitOfWork.Core;
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.EF;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var book = _unitOfWork.Books.GetById(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpGet("async/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var book = await _unitOfWork.Books.GetByIdAsync(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpGet("all/{title}")]
    public IActionResult GetAllByTitle([FromRoute] string title)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var books = _unitOfWork.Books.FindAll(book
            => book.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase), 
            [" Author"]
        );
        if (books.IsNullOrEmpty())
            return NotFound();

        return Ok(books);
    }


    [HttpGet("{title}")]
    public IActionResult GetByTitle([FromRoute] string title)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var book = _unitOfWork.Books.Find(book
            => book.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase),
            [" Author"]
        );
        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpGet]
    public IActionResult GetOrderd([FromQuery] string title)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var book = _unitOfWork.Books.FindAll(book
            => book.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase),
            null,null,book => book.Id
        );
        if (book == null)
            return NotFound();

        return Ok(book);
    }


    [HttpPost("addone")]
    public IActionResult AddBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var newBook = _unitOfWork.Books.Add(book);
        _unitOfWork.Complete();

        return Ok(newBook);
    }

    [HttpPost("addrange")]
    public IActionResult AddBooks([FromBody] IEnumerable<Book> books)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var newBooks = _unitOfWork.Books.AddRange(books);
        _unitOfWork.Complete();

        return Ok(newBooks);
    }
}
