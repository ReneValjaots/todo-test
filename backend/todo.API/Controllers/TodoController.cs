using Microsoft.AspNetCore.Mvc;
using todo.API.Dtos;
using todo.API.Interfaces;
using todo.API.Models;

namespace todo.API.Controllers {
    [Route("api/todo")] [ApiController]
    public class TodoController : ControllerBase {
        private readonly ITodoRepo _repo;

        public TodoController(ITodoRepo repo) {
            _repo = repo;
        }

        [HttpGet] public async Task<ActionResult<IEnumerable<Todo>>> GetTodos() {
            var result = await _repo.GetTodosAsync();
            return Ok(result);
        }

        [HttpGet("filter")] public async Task<ActionResult<IEnumerable<Todo>>> GetFilteredTodos([FromQuery] bool? isCompleted, [FromQuery] DateTime? dueDate, [FromQuery] string? searchText) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _repo.GetFilteredTodosAsync(isCompleted, dueDate, searchText);
            return Ok(result);
        }

        [HttpGet("{id:int}")] public async Task<ActionResult<Todo>> GetTodo(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.GetTodoAsync(id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpPut("{id:int}")] public async Task<IActionResult> PutTodo(int id, UpdateTodoDto todoDto) {
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

        [HttpPost] public async Task<ActionResult<Todo>> PostTodo(CreateTodoDto todoDto) {
            try {
                var createdTodo = await _repo.PostTodoAsync(todoDto);
                return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("{id:int}")] public async Task<IActionResult> DeleteTodo(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.DeleteTodoAsync(id);
            if (todo == null) return NotFound();

            return NoContent();
        }
    }
}
