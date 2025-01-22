﻿using Microsoft.EntityFrameworkCore;
using todo.API.Dtos;
using todo.API.Interfaces;
using todo.API.Models;

namespace todo.API.Repos {
    public class TodoRepo : ITodoRepo {
        private readonly TodoDbContext _context;

        public TodoRepo(TodoDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync() {
            return await _context.Todos.ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetFilteredTodosAsync(bool? isCompleted, DateTime? dueDate, string? searchText) {
            if (isCompleted != null && dueDate != null && searchText != null) {
                return await _context.Todos.Where(x => x.Description.Contains(searchText) && 
                                                       x.DueDate.Date == dueDate.Value.Date && 
                                                       x.IsCompleted == isCompleted).ToListAsync();
            }

            var query = _context.Todos.AsQueryable();

            if (!string.IsNullOrEmpty(searchText)) {
                query = query.Where(x => x.Description.Contains(searchText));
            }

            if (dueDate != null) {
                query = query.Where(x => x.DueDate.Date == dueDate.Value.Date);
            }

            if (isCompleted != null) {
                query = query.Where(x => x.IsCompleted == isCompleted);
            }

            return await query.ToListAsync();
        }

        public async Task<Todo?> GetTodoAsync(int id) {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<Todo?> PutTodoAsync(int id, UpdateTodoDto todoDto) {
            var existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null) return null;

            if (todoDto.DueDate < DateTime.Now) {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            if (todoDto.ParentTodoId != null && !await TodoExistsAsync(todoDto.ParentTodoId.Value)) {
                throw new ArgumentException("ParentTodoId must refer to an existing Todo.");
            }

            existingTodo.Description = todoDto.Description;
            existingTodo.DueDate = todoDto.DueDate;
            existingTodo.IsCompleted = todoDto.IsCompleted;
            existingTodo.ParentTodoId = todoDto.ParentTodoId;

            await _context.SaveChangesAsync();
            return existingTodo;
        }

        public async Task<Todo> PostTodoAsync(CreateTodoDto todoDto) {
            if (todoDto.DueDate < DateTime.Now) {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            if (todoDto.ParentTodoId != null && !await TodoExistsAsync(todoDto.ParentTodoId.Value)) {
                throw new ArgumentException("ParentTodoId must refer to an existing Todo.");
            }

            var todo = new Todo {
                Description = todoDto.Description,
                DueDate = todoDto.DueDate,
                ParentTodoId = todoDto.ParentTodoId,
            };

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo?> DeleteTodoAsync(int id) {
            var todo = await _context.Todos.FirstOrDefaultAsync(i => i.Id == id);
            if (todo == null) return null;
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        private async Task<bool> TodoExistsAsync(int id) {
            return await _context.Todos.AnyAsync(x => x.Id == id);
        }
    }
}
