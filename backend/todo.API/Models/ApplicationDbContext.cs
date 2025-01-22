using Microsoft.EntityFrameworkCore;

namespace todo.API.Models {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }

        public async Task SeedDatabaseAsync() {
            if (Todos.Any()) return; 

            var random = new Random();
            var todos = new List<Todo>();

            for (int i = 0; i < 1_000; i++) {
                todos.Add(new Todo {
                    Description = $"Todo item {i + 1}",
                    CreationDate = DateTime.Now.AddDays(-random.Next(0, 365)),
                    DueDate = DateTime.Now.AddDays(random.Next(1, 365)),
                    IsCompleted = random.Next(0, 2) == 1,
                    ParentTodoId = random.Next(0,4) == 1 ? random.Next(0, i) : null
                });
            }

            await Todos.AddRangeAsync(todos);
            await SaveChangesAsync();
        }
    }
}
