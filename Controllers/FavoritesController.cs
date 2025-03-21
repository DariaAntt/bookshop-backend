using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class FavoritesController: ControllerBase{

    [HttpGet("books")]
    public async Task<IActionResult> GetFavoritesWithBooks()
    {
        using (var db = new ApplicationContext())
        {
            var favorites = await (
                from favorite in db.Favorites
                join book in db.Books on favorite.BookId equals book.Id
                select new BookCardDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    BookImage = book.BookImage
                }
            ).ToListAsync();
            return Ok(favorites);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        using (var db = new ApplicationContext())
        {
            var favorites = await db.Favorites.ToListAsync();
            return Ok(favorites);
        }
    }

    // Добавить книгу в избранное
    [HttpPost("{bookId}")]
    public Task<IActionResult> AddToFavorites(int bookId)
    {
        using(var db = new ApplicationContext()){
            var favorite = new Favorite
            {
                BookId = bookId,
            };
            db.Favorites.Add(favorite);
            db.SaveChanges(); 
            return Task.FromResult<IActionResult>(Ok(new { 
                message = "Книга добавлена в избранное", 
                icon = "/img/heart2.png" 
            }));
        }

    }

    // Удалить книгу из избранного
    [HttpDelete("{bookId}")]
    public async Task<IActionResult> RemoveFromFavorites(int bookId)
    {
        using(var db = new ApplicationContext()){
            var favorite = await db.Favorites.FirstOrDefaultAsync(f => f.BookId == bookId);

            if (favorite == null)
            {
                return NotFound(new { message = "Книга не найдена в избранном" });
            }

            db.Favorites.Remove(favorite);
            await db.SaveChangesAsync();

            return Ok(new { message = "Книга удалена из избранного", icon = "/img/heart1.png" });
        }

    }

    // Проверка, находится ли книга в избранном
    [HttpGet("check/{bookId}")]
    public async Task<IActionResult> IsFavorite(int bookId)
    {
        using(var db = new ApplicationContext()){
            var isFavorite = await db.Favorites.AnyAsync(f => f.BookId == bookId);

            return Ok(new { isFavorite });
        }
    }
}