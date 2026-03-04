using Microsoft.AspNetCore.Mvc.RazorPages;
using Agent8.Models;
using Agent8.Services;

namespace Agent8.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITodoService _todoService;

        public IndexModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IList<TodoItem> Todos { get; set; } = new List<TodoItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            Todos = (await _todoService.GetAllTodosAsync()).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostToggleCompletedAsync(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsCompleted = !todo.IsCompleted;
            todo.UpdatedAt = DateTime.UtcNow;
            
            await _todoService.UpdateTodoAsync(todo);
            
            return RedirectToPage();
        }
    }
}