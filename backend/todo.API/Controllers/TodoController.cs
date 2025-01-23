using Microsoft.AspNetCore.Mvc;
using todo.API.Dtos;
using todo.API.Interfaces;
using todo.API.Models;

namespace todo.API.Controllers {
    [Route("api")] [ApiController]
    public class TodoController : ControllerBase {
        private readonly ITodoRepo _repo;

        public TodoController(ITodoRepo repo) {
            _repo = repo;
        }

        [HttpGet("todos")] public async Task<ActionResult<IEnumerable<Todo>>> GetTodos() {
            var result = await _repo.GetTodosAsync();
            return Ok(result);
        }

        [HttpGet("todos/filter")] public async Task<ActionResult<IEnumerable<Todo>>> GetFilteredTodos([FromQuery] bool? isCompleted, [FromQuery] DateTime? dueDate, [FromQuery] string? searchText) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _repo.GetFilteredTodosAsync(isCompleted, dueDate, searchText);
            return Ok(result);
        }

        [HttpGet("todo/{id:int}")] public async Task<ActionResult<Todo>> GetTodoById(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.GetTodoByIdAsync(id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpGet("todo/{id:int}/subtodos")] public async Task<ActionResult<SubTodosDto>> GetSubtodos(int id) {
            var todoWithChildren = await _repo.GetSubtodosAsync(id);

            if (todoWithChildren == null) {
                return NotFound($"Todo with ID {id} not found.");
            }

            return Ok(todoWithChildren);
        }

        [HttpPut("todo/{id:int}")] public async Task<IActionResult> PutTodo(int id, UpdateTodoDto todoDto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try {
                var updatedTodo = await _repo.PutTodoAsync(id, todoDto);
                if (updatedTodo == null) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost("todo")] public async Task<ActionResult<Todo>> PostTodo(CreateTodoDto todoDto) {
            try {
                var createdTodo = await _repo.PostTodoAsync(todoDto);
                return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("todo/{id:int}")] public async Task<IActionResult> DeleteTodo(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.DeleteTodoAsync(id);
            if (todo == null) return NotFound();

            return NoContent();
        }
    }
}
