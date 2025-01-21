using Microsoft.AspNetCore.Mvc;
using todo.API.Models;

namespace todo.API.Interfaces
{
    public interface ITodoRepo {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo?> GetTodo(int id);
        Task<Todo?> UpdateTodo(int id, Todo todo);
        Task<Todo> CreateTodo(Todo todo);
        Task<Todo?> DeleteTodo(int id);
        Task<ActionResult<IEnumerable<Todo>>> GetFilteredTodos(bool? isCompleted, DateTime? dueDate, string? searchText);
    }
}
