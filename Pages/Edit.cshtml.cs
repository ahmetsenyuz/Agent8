using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agent8.Models;
using Agent8.Services;

namespace Agent8.Pages
{
    public class EditModel : PageModel
    {
        private readonly ITodoService _todoService;

        public EditModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [BindProperty]
        public TodoItem Todo { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var todo = _todoService.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }
            
            Todo = todo;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = Todo.Title;
            todo.Description = Todo.Description;
            todo.UpdatedAt = DateTime.UtcNow;

            await _todoService.UpdateTodoAsync(todo);

            TempData["SuccessMessage"] = "Todo item updated successfully!";
            
            return RedirectToPage("./Index");
        }
    }
}