namespace todo.API.Dtos {
    public class CreateTodoDto {
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int? ParentTodoId { get; set; }
    }
}