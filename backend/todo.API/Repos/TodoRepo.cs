using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.API.Interfaces;
using todo.API.Models;

namespace todo.API.Repos {
    public class TodoRepo : ITodoRepo {
        private readonly ApplicationDbContext _context;

        public TodoRepo(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetTodos() {
            return await _context.Todos.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Todo>>> GetFilteredTodos(bool? isCompleted, DateTime? dueDate, string? searchText) {
            if (isCompleted != null && dueDate != null && searchText != null) {
                return await _context.Todos.Where(x => x.Description.Contains(searchText) && 
                                                       x.DueDate.Date == dueDate.Value.Date && 
                                                       x.IsCompleted == isCompleted).ToListAsync();
            }

            var query = _context.Todos.AsQueryable();

            if (!string.IsNullOrEmpty(searchText)) {
                query = query.Where(x => x.Description.Contains(searchText));
            }

            if (dueDate != null) {
                query = query.Where(x => x.DueDate.Date == dueDate.Value.Date);
            }

            if (isCompleted != null) {
                query = query.Where(x => x.IsCompleted == isCompleted);
            }

            return await query.ToListAsync();
        }

        public async Task<Todo?> GetTodo(int id) {
            return await _context.Todos
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Todo?> UpdateTodo(int id, Todo todo) {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (existingTodo == null) return null;

            existingTodo.Description = todo.Description;
            existingTodo.CreationDate = todo.CreationDate;
            existingTodo.DueDate = todo.DueDate;
            existingTodo.IsCompleted = todo.IsCompleted;
            existingTodo.ParentTodoId = todo.ParentTodoId;

            await _context.SaveChangesAsync();
            return existingTodo;
        }

        public async Task<Todo> CreateTodo(Todo todo) {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo?> DeleteTodo(int id) {
            var todo = await _context.Todos.FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null) return null;
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }
    }
}
