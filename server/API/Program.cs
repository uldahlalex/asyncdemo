using Infra;
using LinqToDB;

var builder = WebApplication.CreateBuilder(args);

var options = new DataOptions<MyDatabaseConnection>(
    new DataOptions().UseSQLite("Data Source=../Infra/db.db"));

builder.Services.AddScoped<MyDatabaseConnection>(_ => 
    new MyDatabaseConnection(options));

builder.Services.AddScoped<LibraryService>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddCors();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<MyExceptionHandler>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDatabaseConnection>();
    db.CreateTable<Book>(tableOptions: TableOptions.CreateIfNotExists);
    db.CreateTable<Author>(tableOptions:TableOptions.CreateIfNotExists);
    if (db.Authors.Count() == 0)
    {
        db.Insert(new Author()
        {
            AuthorId = "1",
            AuthorName = "bob"
            
        });
    }
    if (db.Books.Count() == 0)
    {
           db.Insert(new Book()
            {
                BookId = "1",
                BookTitle = "book 1",
                NumberOfPages = 100,
                AuthorId = "1"
            });
    }
   
}

app.UseExceptionHandler();
app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();