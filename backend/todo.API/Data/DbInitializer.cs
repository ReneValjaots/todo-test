using Microsoft.EntityFrameworkCore;

namespace todo.API.Data {
    public abstract class DbInitializer<TItem>(DbContext db, DbSet<TItem> set) where TItem : class, new() {
        protected TItem? Item;
        protected abstract void SetValues(int index);

        private bool CanInitialize() {
            if (db is null || set is null) return false;
            db.Database.EnsureCreated();
            return !set.Any();
        }

        public async Task Initialize(uint count, int batchSize = 1000) {
            if (!CanInitialize())
                return;

            var batch = new List<TItem>(batchSize);

            try {
                for (var i = 1; i <= count; i++) {
                    Item = new TItem();
                    SetValues(i);
                    batch.Add(Item);

                    if (i % batchSize == 0 || i == count) {
                        await set.AddRangeAsync(batch);
                        await db.SaveChangesAsync();
                        batch.Clear();
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Seeding failed: {ex.Message}");
                throw;
            }
        }
    }
}
