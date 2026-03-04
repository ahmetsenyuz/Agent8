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
    public class CreatePageTests
    {
        [Fact]
        public async Task OnPostAsync_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var todo = new TodoItem { Title = "New Todo" };
            
            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            mockTodoService.Setup(service => service.CreateTodoAsync(todo))
                .ReturnsAsync(new TodoItem { Id = 1, Title = "New Todo" });

            var pageModel = new CreateModel(mockTodoService.Object);

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
            var pageModel = new CreateModel(mockTodoService.Object);
            pageModel.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }
    }
}