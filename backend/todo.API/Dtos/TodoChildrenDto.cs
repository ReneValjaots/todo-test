using todo.API.Models;

namespace todo.API.Dtos {
    public class TodoChildrenDto {
        public List<Todo> Children { get; set; } = new List<Todo>();
    }
}
