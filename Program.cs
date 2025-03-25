
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>();


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", builder => {
        builder.WithOrigins("https://bookshop-frontend-25k2p58sb-daria-ants-projects.vercel.app",
                            "https://bookshop-frontend-rose.vercel.app").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowReactApp");
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    try
    {
        var books = db.Books.ToList();
        foreach (var book in books)
        {
            Console.WriteLine(book.Title);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB Error on startup: {ex.Message}");
    }
}

//----------

app.Run();
