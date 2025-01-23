using todo.API.Models;

namespace todo.API.Dtos {
    public class SubTodosDto {
        public List<Todo> SubTodos { get; set; } = new List<Todo>();
    }
}
