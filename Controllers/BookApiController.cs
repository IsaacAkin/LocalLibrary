using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BookApiController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public BookApiController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult CreateBook(Book book)
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        Database.Instance.AddBook(book.Name, book.Author, book.Copies);
        return Created();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Book>> GetBooks()
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        return Ok(Database.Instance.GetAllBooks());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Book> UpdateBook(int isbn, string name, string author)
    {
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult DeleteBook(Book book)
    {
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        Database.Instance.DeleteBook(book.Name, book.Isbn);
        return Ok();
    }
}