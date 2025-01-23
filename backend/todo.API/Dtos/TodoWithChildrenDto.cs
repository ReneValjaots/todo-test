using todo.API.Models;

namespace todo.API.Dtos {
    public class TodoWithChildrenDto {
        public Todo Todo { get; set; }
        public List<Todo> Children { get; set; } = new List<Todo>();
    }
}