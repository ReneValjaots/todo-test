using todo.API.Dtos;
using todo.API.Models;

namespace todo.API.Interfaces
{
    public interface ITodoRepo {
        Task<IEnumerable<Todo>> GetTodosAsync();
        Task<Todo?> GetTodoAsync(int id);
        Task<Todo?> PutTodoAsync(int id, UpdateTodoDto todoDto);
        Task<Todo> PostTodoAsync(CreateTodoDto todoDto);
        Task<Todo?> DeleteTodoAsync(int id);
        Task<IEnumerable<Todo>> GetFilteredTodosAsync(bool? isCompleted, DateTime? dueDate, string? searchText);
    }
}
