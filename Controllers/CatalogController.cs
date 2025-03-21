using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase{

    List<BookCardDTO> books = new List<BookCardDTO>();

    [HttpGet]
    public IEnumerable<BookCardDTO> Get(){

         using(ApplicationContext db = new ApplicationContext()){
            books = db.Books.Select(c => new BookCardDTO{
                Id = c.Id,
                Title = c.Title, 
                Author= c.Author, 
                Price=c.Price, 
                BookImage=c.BookImage
                })
            .Take(8)
            .ToList();
         }
        return books;
    }


    [HttpGet("paged")]
    public IActionResult GetPagedBooks([FromQuery] BookFilterDTO filter, [FromQuery] int skip = 0, [FromQuery] int take = 8)
    {
        using (var db = new ApplicationContext())
        {
            var query = db.Books.AsQueryable();

            // Применяем фильтры
            if (filter.Categories != null && filter.Categories.Any())
                query = query.Where(b => filter.Categories.Contains(b.Category));

            if (filter.MinPrice.HasValue)
                query = query.Where(b => b.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= filter.MaxPrice);

            if (filter.Authors != null && filter.Authors.Any())
                query = query.Where(b => filter.Authors.Contains(b.Author));

            if (filter.Publishers != null && filter.Publishers.Any())
                query = query.Where(b => filter.Publishers.Contains(b.Publishing));

            if (filter.BindingType != null)
                query = query.Where(b => b.Binding == filter.BindingType);

            if (filter.MinYear.HasValue)
                query = query.Where(b => b.Year >= filter.MinYear);

            if (filter.MaxYear.HasValue)
                query = query.Where(b => b.Year <= filter.MaxYear);

            if (filter.AgeLimits != null && filter.AgeLimits.Any())
                query = query.Where(b => filter.AgeLimits.Contains(b.AgeLimit.Value));

            // Применяем пагинацию после фильтрации
            var filteredBooks = query
                .Select(c => new BookCardDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Author = c.Author,
                    Price = c.Price,
                    BookImage = c.BookImage
                })
                .Skip(skip) // Пропускаем указанное количество книг
                .Take(take) // Берем указанное количество книг
                .ToList();

            return Ok(filteredBooks);
        }
    }

    [HttpPost("filtered")]
    public IActionResult GetFilteredBooks([FromBody] BookFilterDTO filter)
    {
        
        Console.WriteLine($"Filter received: {filter}");        
        using (var db = new ApplicationContext())
        {
            var query = db.Books.AsQueryable();

            // Применяем фильтры
            if (filter.Categories != null && filter.Categories.Any())
                query = query.Where(b => filter.Categories.Contains(b.Category));

            if (filter.MinPrice.HasValue)
                query = query.Where(b => b.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= filter.MaxPrice);

            if (filter.Authors != null && filter.Authors.Any())
                query = query.Where(b => filter.Authors.Contains(b.Author));

            if (filter.Publishers != null && filter.Publishers.Any())
                query = query.Where(b => filter.Publishers.Contains(b.Publishing));

            if (filter.BindingType != null)
                query = query.Where(b => b.Binding == filter.BindingType);

            if (filter.MinYear.HasValue)
                query = query.Where(b => b.Year >= filter.MinYear);

            if (filter.MaxYear.HasValue)
                query = query.Where(b => b.Year <= filter.MaxYear);

            if (filter.AgeLimits != null && filter.AgeLimits.Any())
                query = query.Where(b => filter.AgeLimits.Contains(b.AgeLimit.Value));

            // Возвращаем результат
            var filteredBooks = query.Select(c => new BookCardDTO
            {
                Id = c.Id,
                Title = c.Title,
                Author = c.Author,
                Price = c.Price,
                BookImage = c.BookImage
            })
            .Take(8)
            .ToList();
            return Ok(filteredBooks);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookDetails(int id)
    {
        using (var db = new ApplicationContext())
        {
            var book = await db.Books
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Description,
                    b.Category,
                    b.Publishing,
                    b.Binding,
                    b.Year,
                    b.AgeLimit,              
                })
                .FirstOrDefaultAsync();

            if (book == null) 
                return NotFound();
            return Ok(book);
        }
    }


    [HttpGet("authors")]
    public IActionResult GetAuthors()
    {
        using (var db = new ApplicationContext())
        {
            var authors = db.Books
                .Select(b => b.Author) // Выбираем только поле Author
                .Distinct() // Убираем дубликаты
                .OrderBy(p=>p)
                .ToList();

            return Ok(authors);
        }
    }


    [HttpGet("publishings")]
    public IActionResult GetPublishings()
    {
        using (var db = new ApplicationContext())
        {
            var publishings = db.Books
                .Select(b => b.Publishing) // Выбираем только поле Author
                .Distinct() // Убираем дубликаты
                .OrderBy(p=>p)
                .ToList();

            return Ok(publishings);
        }
    }


}