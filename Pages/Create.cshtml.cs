using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Agent8.Models;
using Agent8.Services;

namespace Agent8.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ITodoService _todoService;

        public CreateModel(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [BindProperty]
        public TodoItem Todo { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Todo.CreatedAt = DateTime.UtcNow;
            Todo.UpdatedAt = DateTime.UtcNow;
            
            await _todoService.AddTodoAsync(Todo);
            
            TempData["SuccessMessage"] = "Todo item created successfully!";
            
            return RedirectToPage("./Index");
        }
    }
}