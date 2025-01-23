using Microsoft.EntityFrameworkCore;
using todo.API.Models;

namespace todo.API.Data {
    public sealed class TodoDbInitializer(DbContext db, DbSet<Todo> set) : DbInitializer<Todo>(db, set) {
        private readonly Random _random = new Random();

        protected override void SetValues(int index) {
            if (Item == null) return;
            Item.Description = $"Task {index}";
            Item.DueDate = DateTime.Now.AddMinutes(index);
            Item.IsCompleted = _random.Next(2) == 0;
            if (_random.NextDouble() < 0.25) Item.ParentTodoId = _random.Next(1, index);
        }
    }
}
