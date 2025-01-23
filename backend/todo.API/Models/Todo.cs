using System.ComponentModel.DataAnnotations;

namespace todo.API.Models {
    public class Todo {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int? ParentTodoId { get; set; }
    }
}
