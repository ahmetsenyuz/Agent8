using System;
using System.Threading.Tasks;
using Agent8.Models;
using Agent8.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace Agent8.Tests.Pages
{
    public class EditPageTests
    {
        [Fact]
        public async Task OnPostAsync_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var todo = new TodoItem { Id = 1, Title = "Updated Todo" };
            
            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            mockTodoService.Setup(service => service.UpdateTodoAsync(todo))
                .ReturnsAsync(new TodoItem { Id = 1, Title = "Updated Todo" });

            var pageModel = new EditModel(mockTodoService.Object);

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("./Index", redirectToPageResult.PageName);
        }

        [Fact]
        public async Task OnPostAsync_WithInvalidModel_ReturnsPage()
        {
            // Arrange
            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            var pageModel = new EditModel(mockTodoService.Object);
            pageModel.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_WithNonExistentTodo_ReturnsNotFound()
        {
            // Arrange
            var todo = new TodoItem { Id = 999, Title = "Non-existent Todo" };
            
            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            mockTodoService.Setup(service => service.UpdateTodoAsync(todo))
                .ReturnsAsync((TodoItem)null);

            var pageModel = new EditModel(mockTodoService.Object);

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}