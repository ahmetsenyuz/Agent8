using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agent8.Models;
using Agent8.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace Agent8.Tests.Pages
{
    public class IndexPageTests
    {
        [Fact]
        public async Task OnGetAsync_WithTodos_ReturnsTodos()
        {
            // Arrange
            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Test Todo 1" },
                new TodoItem { Id = 2, Title = "Test Todo 2" }
            };

            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            mockTodoService.Setup(service => service.GetAllTodosAsync())
                .ReturnsAsync(todos);

            var pageModel = new IndexModel(mockTodoService.Object);

            // Act
            await pageModel.OnGetAsync();

            // Assert
            Assert.NotNull(pageModel.Todos);
            Assert.Equal(2, pageModel.Todos.Count);
        }

        [Fact]
        public async Task OnGetAsync_WithoutTodos_ReturnsEmptyList()
        {
            // Arrange
            var mockTodoService = new Mock<Agent8.Services.ITodoService>();
            mockTodoService.Setup(service => service.GetAllTodosAsync())
                .ReturnsAsync(new List<TodoItem>());

            var pageModel = new IndexModel(mockTodoService.Object);

            // Act
            await pageModel.OnGetAsync();

            // Assert
            Assert.NotNull(pageModel.Todos);
            Assert.Empty(pageModel.Todos);
        }
    }
}