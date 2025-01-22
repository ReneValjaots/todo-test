using Microsoft.EntityFrameworkCore;

namespace todo.API.Models
{
    public sealed class TodoDbInitializer(DbContext db, DbSet<Todo> set) : DbInitializer<Todo>(db, set) {
        private readonly Random _random = new Random();
        protected override void SetValues(int index) {
            if (Item == null) return;
            Item.Description = $"Task {index}";
            Item.DueDate = DateTime.Now.AddDays(index);
            Item.IsCompleted = _random.Next(2) == 0;
        }
    }
}
