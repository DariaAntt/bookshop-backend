using System.Security.Authentication.ExtendedProtection;

/// Хелпер класс для работы с изображениями.

public class ImageSaveHelper
{
    public static string SaveImage(IFormFile image)
    {

        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot/img/books"));

        var ext = Path.GetExtension(image.FileName);
        var newName = Guid.NewGuid().ToString() + ext;

        using (var fileStream = new FileStream(Path.Combine(path, newName), FileMode.Create))
        {
            image.CopyTo(fileStream);
        }

        return newName;
    }
}