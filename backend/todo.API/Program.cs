using Microsoft.EntityFrameworkCore;
using todo.API.Interfaces;
using todo.API.Models;
using todo.API.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TodoDb"));

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

// Seed database for testing
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    await context.SeedDatabaseAsync();
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