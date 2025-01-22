using System.ComponentModel.DataAnnotations;

namespace todo.API.Dtos
{
    public class UpdateTodoDto
    {
        [Required]
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int? ParentTodoId { get; set; }
    }
}