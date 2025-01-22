using Microsoft.EntityFrameworkCore;
using todo.API.Interfaces;
using todo.API.Models;
using todo.API.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<ITodoRepo, TodoRepo>();


builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policyBuilder => {
        policyBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    }
));

var app = builder.Build();

EnsureDatabaseCreated(app);
var todoDbTask = Task.Run(async () => await TryInitializeTodoDatabase(app));

// Seed database for testing
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TodoDbContext>();

    context.Database.Migrate(); 
    //await context.SeedDatabaseAsync(); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("MyPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

todoDbTask.Wait();

static void EnsureDatabaseCreated(WebApplication app) {
    var db = GetContext<TodoDbContext>(app);
    db.Database.EnsureCreated();
}

static async Task TryInitializeTodoDatabase(WebApplication app) {
    var todoDb = GetContext<TodoDbContext>(app);
    await new TodoDbInitializer(todoDb, todoDb.Todos).Initialize(1000_000);

}

static TDbContext GetContext<TDbContext>(WebApplication app) where TDbContext : DbContext => app
    .Services
    .CreateScope()
    .ServiceProvider
    .GetRequiredService<TDbContext>();
