using Microsoft.EntityFrameworkCore;

namespace todo.API.Models {
    public abstract class DbInitializer<TItem>(DbContext db, DbSet<TItem> set) where TItem : class, new() {
        protected TItem? Item;
        private readonly List<TItem> _list = [];
        protected abstract void SetValues(int index);

        private async Task Save() {
            await set.AddRangeAsync(_list);
            await db.SaveChangesAsync();
            _list.Clear();
        }

        private bool CanInitialize() {
            if (db is null)
                return false;
            if (set is null)
                return false;
            db.Database.EnsureCreated();
            return !set.Any();
        }

        public async Task Initialize(uint count) {
            if (!CanInitialize())
                return;
            try {
                for (var i = 1; i < count; i++) {
                    Item = new TItem();
                    SetValues(i);
                    _list.Add(Item);
                    if (i % 100 != 0) continue;
                    await Save();
                }
            }
            finally {
                await Save();
            }
        }
    }
}
