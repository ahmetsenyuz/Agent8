using System.Collections.Concurrent;
using Agent8.Models;

namespace Agent8.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<TodoItem> CreateTodoAsync(TodoItem todo);
        Task<TodoItem?> UpdateTodoAsync(TodoItem todo);
        Task<bool> DeleteTodoAsync(int id);
    }

    public class TodoService : ITodoService
    {
        private readonly ConcurrentDictionary<int, TodoItem> _todos = new ConcurrentDictionary<int, TodoItem>();
        private int _nextId = 1;

        /// <summary>
        /// Gets all todo items asynchronously
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains all todo items.</returns>
        public Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return Task.FromResult(_todos.Values.AsEnumerable());
        }

        /// <summary>
        /// Gets a todo item by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the todo item to retrieve</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the todo item or null if not found.</returns>
        public Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            _todos.TryGetValue(id, out var todo);
            return Task.FromResult(todo);
        }

        /// <summary>
        /// Creates a new todo item asynchronously
        /// </summary>
        /// <param name="todo">The todo item to create</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created todo item.</returns>
        public Task<TodoItem> CreateTodoAsync(TodoItem todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            todo.Id = _nextId++;
            _todos[todo.Id] = todo;
            return Task.FromResult(todo);
        }

        /// <summary>
        /// Updates an existing todo item asynchronously
        /// </summary>
        /// <param name="todo">The todo item to update</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated todo item or null if not found.</returns>
        public Task<TodoItem?> UpdateTodoAsync(TodoItem todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));

            if (_todos.TryGetValue(todo.Id, out var existingTodo))
            {
                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;
                existingTodo.IsCompleted = todo.IsCompleted;
                existingTodo.UpdatedAt = DateTime.UtcNow;
                return Task.FromResult<TodoItem>(existingTodo);
            }

            return Task.FromResult<TodoItem>(null);
        }

        /// <summary>
        /// Deletes a todo item by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the todo item to delete</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public Task<bool> DeleteTodoAsync(int id)
        {
            if (_todos.TryRemove(id, out _))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}