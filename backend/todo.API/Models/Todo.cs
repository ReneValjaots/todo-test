namespace todo.API.Models {
    public class Todo {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public int? ParentTodoId { get; set; }
    }
}
