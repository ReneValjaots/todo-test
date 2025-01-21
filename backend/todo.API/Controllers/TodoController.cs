using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var result = await _repo.GetTodos();
            return Ok(result);
        }

        [HttpGet("filter")] public async Task<ActionResult<IEnumerable<Todo>>> GetFilteredTodos([FromQuery] bool? isCompleted, [FromQuery] DateTime? dueDate, [FromQuery] string? searchText) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _repo.GetFilteredTodos(isCompleted, dueDate, searchText);
            return Ok(result);
        }

        [HttpGet("{id:int}")] public async Task<ActionResult<Todo>> GetTodo(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.GetTodo(id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpPut("{id:int}")] public async Task<IActionResult> PutTodo(int id, Todo todo) {
            if (id != todo.Id) return BadRequest();

            var updatedTodo = await _repo.UpdateTodo(id, todo);
            if (updatedTodo == null) return NotFound();

            return NoContent();
        }

        [HttpPost] public async Task<ActionResult<Todo>> PostTodo(Todo todo) {
            var createdTodo = await _repo.CreateTodo(todo);
            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpDelete("{id:int}")] public async Task<IActionResult> DeleteTodo(int id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _repo.DeleteTodo(id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }
    }
}
