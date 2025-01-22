using Microsoft.EntityFrameworkCore;

namespace todo.API.Models {
    public class TodoDbContext(DbContextOptions<TodoDbContext> options) : BaseDbContext<TodoDbContext>(options) {
        internal const string TodoSchema = "Todos";
        public DbSet<Todo> Todos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            InitializeTables(builder);
        }

        internal static void InitializeTables(ModelBuilder builder) {
            ToTable<Todo>(builder, nameof(Todos), TodoSchema);
        }
    }
}
