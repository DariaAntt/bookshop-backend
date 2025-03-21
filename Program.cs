

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


using(ApplicationContext db = new ApplicationContext()){

    var books = db.Books.ToList();
    foreach(var book in books){
        System.Console.WriteLine(book.Title);
    }
}


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", builder => {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
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

//----------


app.Run();
