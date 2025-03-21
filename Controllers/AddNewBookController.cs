using System.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AddNewBookController: ControllerBase{

    [HttpPost]
    public IActionResult Post([FromForm] AddBookDTO addBookRequest){
        Console.WriteLine($"Title: {addBookRequest.Title}");
        Console.WriteLine($"Author: {addBookRequest.Author}");
        Console.WriteLine($"Description: {addBookRequest.Description}");
        Console.WriteLine($"Category: {addBookRequest.Category}");
        Console.WriteLine($"Publishing: {addBookRequest.Publishing}");
        Console.WriteLine($"Binding: {addBookRequest.Binding}");
        Console.WriteLine($"Year: {addBookRequest.Year}");
        Console.WriteLine($"AgeLimit: {addBookRequest.AgeLimit}");
        Console.WriteLine($"Price: {addBookRequest.Price}");

        using(var db = new ApplicationContext()){
            
            // Сохранение фото
            var book_image = addBookRequest.BookImage != null ? ImageSaveHelper.SaveImage(addBookRequest.BookImage): null;

            var new_book = new Book{
                Title = addBookRequest.Title,
                Author = addBookRequest.Author,
                Description = addBookRequest.Description,
                Category = addBookRequest.Category,
                Publishing = addBookRequest.Publishing,
                Binding = addBookRequest.Binding,
                Year = addBookRequest.Year,
                AgeLimit = addBookRequest.AgeLimit,
                Price = addBookRequest.Price,
                BookImage = book_image
            };

            db.Books.Add(new_book);
            db.SaveChanges();            
        }
        return Ok();
        
    }
}
